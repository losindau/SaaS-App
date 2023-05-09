using InventoryManagementApp.Data.Interfaces;
using InventoryManagementApp.Data.Models;
using InventoryManagementApp.Data.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace InventoryManagementApp.Data.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountRepository(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._configuration = configuration;
            this._httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> SignInAsync(SignInVM signInVM)
        {
            var result = await _signInManager.PasswordSignInAsync(signInVM.Email, signInVM.Password, false, false);

            if (!result.Succeeded)
            {
                return string.Empty;
            }

            var user = await _userManager.FindByEmailAsync(signInVM.Email);
            var role = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, role[0]),
                new Claim(ClaimTypes.Name, user.FirstName + " " + user.LastName),
                new Claim("FirstName", user.FirstName),
                new Claim("LastName", user.LastName),
                new Claim("CompanyID", user.CompanyID.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var authenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(20),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authenKey, SecurityAlgorithms.HmacSha512Signature)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<IdentityResult> SignUpAsync(AppUser appUser, string password)
        {
            return await _userManager.CreateAsync(appUser, password);
        }

        public ICollection<AppUser> GetUsers()
        {
            var authHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
            var token = authHeader[0].Substring("Bearer ".Length).Trim();
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
            var tenantId = securityToken.Claims.First(claim => claim.Type == "CompanyID").Value;

            return _userManager.Users.Include(a => a.Truck).Where(a => a.CompanyID == int.Parse(tenantId)).ToList();
        }
    }
}

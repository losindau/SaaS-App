using AutoMapper;
using InventoryManagementApp.Data;
using InventoryManagementApp.Data.Interfaces;
using InventoryManagementApp.Data.Models;
using InventoryManagementApp.Data.Repository;
using InventoryManagementApp.Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace InventoryManagementApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

        public AccountController(IAccountRepository accountRepository, IMapper mapper, UserManager<AppUser> userManager)
        {
            this._accountRepository = accountRepository;
            this._mapper = mapper;
            this._userManager = userManager;
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn([FromBody] SignInVM signInVM)
        {
            var result = await _accountRepository.SignInAsync(signInVM);

            if (string.IsNullOrEmpty(result))
            {
                return Unauthorized();
            }

            return Ok(result);
        }


        [HttpGet("{page}")]
        [Authorize(Roles = "Manager")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AppUser>))]
        public async Task<IActionResult> GetUsers(int page)
        {
            List<AppUser> users = (List<AppUser>)_accountRepository.GetUsers();

            var pageResults = 5f;
            var pageCount = Math.Ceiling(users.Count() / pageResults);


            List<AppUserVM> usersMap = _mapper.Map<List<AppUserVM>>(users.Skip((page - 1) * (int)pageResults).Take((int)pageResults));

            for (int i = 0; i < usersMap.Count(); i++)
            {
                var role = await _userManager.GetRolesAsync(users[i]);
                usersMap[i].Role = role[0].ToString();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = new ResponsePagination()
            {
                Entities = new List<object>(usersMap),
                CurrentPage = page,
                Pages = (int)pageCount
            };

            return Ok(response);
        }
    }
}

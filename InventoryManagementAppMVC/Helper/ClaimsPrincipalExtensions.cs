using System.Security.Claims;

namespace InventoryManagementAppMVC.Helper
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetUserId(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.NameIdentifier).Value;
        }

        public static string GetUserEmail(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.Email).Value;
        }

        public static string GetUserRole(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.Role).Value;
        }

        public static string GetUserName(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.Name).Value;
        }

        public static string GetUserFirstName(this ClaimsPrincipal user)
        {
            return user.FindFirst("FirstName").Value;
        }

        public static string GetUserLastName(this ClaimsPrincipal user)
        {
            return user.FindFirst("LastName").Value;
        }

        public static string GetUserCompanyID(this ClaimsPrincipal user)
        {
            return user.FindFirst("CompanyID").Value;
        }
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MiniAccountManagementSystem.Services;

namespace MiniAccountManagementSystem.Pages
{
    public class BasePageModel : PageModel
    {
        private readonly PageAccessService _accessService;
        private readonly UserManager<IdentityUser> _userManager;

        public BasePageModel(PageAccessService accessService, UserManager<IdentityUser> userManager)
        {
            _accessService = accessService;
            _userManager = userManager;
        }

        public async Task<bool> CheckPageAccessAsync(string pageName)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
                return false; // ইউজার লগইন না করলে AccessDenied দেখাবে

            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault();

            if (string.IsNullOrEmpty(role))
                return false; // রোল না থাকলেও Denied

            return _accessService.HasAccess(role, pageName);
        }
    }
}

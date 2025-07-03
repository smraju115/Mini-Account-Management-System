//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using MiniAccountManagementSystem.Services;

//namespace MiniAccountManagementSystem.Pages.Admin
//{
//    public class RolePermissionModel : PageModel
//    {
//        private readonly RoleManager<IdentityRole> _roleManager;
//        private readonly RolePagePermissionService _permissionService;

//        public RolePermissionModel(RoleManager<IdentityRole> roleManager, RolePagePermissionService permissionService)
//        {
//            _roleManager = roleManager;
//            _permissionService = permissionService;
//        }

//        [BindProperty]
//        public string SelectedRole { get; set; }

//        [BindProperty]
//        public List<string> SelectedPages { get; set; } = new();

//        public List<string> PageList { get; set; } = new();

//        public SelectList RoleList { get; set; }

//        public void OnGet()
//        {
//            RoleList = new SelectList(_roleManager.Roles.ToList(), "Name", "Name");
//        }

//        public void OnPost()
//        {
//            RoleList = new SelectList(_roleManager.Roles.ToList(), "Name", "Name");

//            if (!string.IsNullOrEmpty(SelectedRole))
//            {
//                PageList = _permissionService.GetAllPages();
//                SelectedPages = _permissionService.GetPagesByRole(SelectedRole);
//            }
//        }

//        public IActionResult OnPostSave()
//        {
//            if (!string.IsNullOrEmpty(SelectedRole))
//            {
//                _permissionService.UpdateRolePermissions(SelectedRole, SelectedPages);
//                TempData["Success"] = "Permission updated!";
//            }

//            return RedirectToPage();
//        }
//    }
//}

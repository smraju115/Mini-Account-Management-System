using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace MiniAccountManagementSystem.Controllers
{
    public class PageAccessController : Controller
    {
        private readonly IConfiguration _config;
        private readonly RoleManager<IdentityRole> _roleManager;

        public PageAccessController(IConfiguration config, RoleManager<IdentityRole> roleManager)
        {
            _config = config;
            _roleManager = roleManager;
        }

        //public IActionResult Index()
        //{
        //    var roles = _roleManager.Roles.ToList();
        //    return View(roles);
        //}

        public IActionResult Manage(string roleName)
        {
            var allPages = new List<string>
            {
                
                
                "UserRole.Index",
                "UserRole.ManageRole",
                "Role.Index",
                "PageAccess.Manage",
                "ChartOfAccounts.Index",
                "Voucher.Index",
                "Voucher.Create",
                "Voucher.View"
                


            };
            var assignedPages = GetAssignedPages(roleName);
            ViewBag.RoleName = roleName;
            ViewBag.AllPages = allPages;
            ViewBag.AssignedPages = assignedPages;
            return View();
        }

        [HttpPost]
        public IActionResult Save(string roleName, List<string> selectedPages)
        {
            using (var conn = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                conn.Open();

                // Delete previous permissions
                var deleteCmd = new SqlCommand("DELETE FROM RoleAccess WHERE RoleName = @RoleName", conn);
                deleteCmd.Parameters.AddWithValue("@RoleName", roleName);
                deleteCmd.ExecuteNonQuery();

                // Insert new permissions
                foreach (var page in selectedPages)
                {
                    var insertCmd = new SqlCommand("INSERT INTO RoleAccess (RoleName, PageName) VALUES (@RoleName, @PageName)", conn);
                    insertCmd.Parameters.AddWithValue("@RoleName", roleName);
                    insertCmd.Parameters.AddWithValue("@PageName", page);
                    insertCmd.ExecuteNonQuery();
                }
            }
            return RedirectToAction("Index", "Role");
        }

        private List<string> GetAssignedPages(string roleName)
        {
            var pages = new List<string>();
            using (var conn = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                var cmd = new SqlCommand("SELECT PageName FROM RoleAccess WHERE RoleName = @RoleName", conn);
                cmd.Parameters.AddWithValue("@RoleName", roleName);
                conn.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    pages.Add(reader.GetString(0));
                }
            }
            return pages;
        }
    }
}

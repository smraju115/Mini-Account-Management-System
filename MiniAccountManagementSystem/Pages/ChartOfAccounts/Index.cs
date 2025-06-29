using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MiniAccountManagementSystem.DataAccrss;
using MiniAccountManagementSystem.Models;

namespace MiniAccountManagementSystem.Pages.ChartOfAccounts
{
    public class IndexModel : PageModel
    {
        private readonly DatabaseHelper _db;

        public IndexModel(IConfiguration config)
        {
            _db = new DatabaseHelper(config);
        }

        [BindProperty]
        public ChartOfAccountModel Account { get; set; }

        public List<ChartOfAccountModel> AccountsList { get; set; }

        public void OnGet()
        {
            AccountsList = _db.GetChartOfAccounts();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            _db.AddChartAccount(Account);
            return RedirectToPage();
        }
    }
}

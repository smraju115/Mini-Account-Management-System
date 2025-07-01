using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MiniAccountManagementSystem.DataAccrss;
using MiniAccountManagementSystem.Models;
using MiniAccountManagementSystem.Services;

namespace MiniAccountManagementSystem.Pages.ChartOfAccounts
{
    public class IndexModel : BasePageModel
    {
        private readonly DatabaseHelper _db;

        public IndexModel(IConfiguration config, PageAccessService accessService, UserManager<IdentityUser> userManager) : base(accessService, userManager)
        {
            _db = new DatabaseHelper(config);
        }

        [BindProperty]
        public ChartOfAccountModel Account { get; set; } = new(); // instence make null problem solve

        public List<ChartOfAccountModel> AccountsList { get; set; }



        public async Task<IActionResult> OnGetAsync()
        {
            if (!await CheckPageAccessAsync("ChartOfAccounts.Index"))
                return RedirectToPage("/AccessDenied");

            Account = new();
            AccountsList = _db.GetChartOfAccounts();

            return Page();
        }
        //public void OnGet()
        //{

        //    Account = new(); // optional fallback
        //    AccountsList = _db.GetChartOfAccounts();
        //}

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // যদি ID = 0, তাহলে new insert
            if (Account.AccountID == 0)
            {
                _db.AddChartAccount(Account);
            }

            return RedirectToPage();
        }


        public IActionResult OnPostUpdate()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _db.UpdateChartAccount(Account);
            return RedirectToPage();

        }


        public IActionResult OnGetEdit(int id)
        {
            Account = _db.GetById(id);
            AccountsList = _db.GetChartOfAccounts();
            return Page();
        }

       

        public IActionResult OnDelete(int id)
        {
            _db.DeleteChartAccount(id);
            return RedirectToPage();

        }


    }
}

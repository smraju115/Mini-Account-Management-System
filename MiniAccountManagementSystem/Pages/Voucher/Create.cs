using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using MiniAccountManagementSystem.Models;
using System.Data;

namespace MiniAccountManagementSystem.Pages.Voucher
{
    public class CreateModel: PageModel
    {
        private readonly IConfiguration _config;
        private readonly string _connectionString;

        public CreateModel(IConfiguration config)
        {
            _config = config;
            _connectionString = _config.GetConnectionString("DefaultConnection");
        }

        [BindProperty]
        public VoucherModel Voucher { get; set; } = new();

        public List<SelectListItem> AccountList { get; set; }

        public void OnGet()
        {
            LoadAccounts();
            // Initial 2 rows
            Voucher.Entries.Add(new VoucherEntryModel());
            Voucher.Entries.Add(new VoucherEntryModel());
        }

        private void LoadAccounts()
        {
            AccountList = new List<SelectListItem>();

            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("sp_ManageChartOfAccounts", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "GET");

            conn.Open();
            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                AccountList.Add(new SelectListItem
                {
                    Value = reader["AccountID"].ToString(),
                    Text = reader["AccountName"].ToString()
                });
            }
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                LoadAccounts();
                return Page();
            }

            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("sp_SaveVoucher", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@VoucherDate", Voucher.VoucherDate);
            cmd.Parameters.AddWithValue("@ReferenceNo", Voucher.ReferenceNo ?? "");
            cmd.Parameters.AddWithValue("@VoucherType", Voucher.VoucherType ?? "");

            // Table-valued parameter
            var table = new DataTable();
            table.Columns.Add("AccountID", typeof(int));
            table.Columns.Add("DebitAmount", typeof(decimal));
            table.Columns.Add("CreditAmount", typeof(decimal));
            table.Columns.Add("Remarks", typeof(string));

            foreach (var entry in Voucher.Entries)
            {
                table.Rows.Add(entry.AccountID, entry.DebitAmount, entry.CreditAmount, entry.Remarks ?? "");
            }

            var param = cmd.Parameters.AddWithValue("@VoucherEntries", table);
            param.SqlDbType = SqlDbType.Structured;
            param.TypeName = "VoucherEntryType";

            conn.Open();
            cmd.ExecuteNonQuery();

            TempData["Success"] = "Voucher saved successfully!";
            return RedirectToPage("/Voucher/Create");
        }
    }
}

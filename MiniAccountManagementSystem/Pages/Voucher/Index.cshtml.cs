using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Data;

namespace MiniAccountManagementSystem.Pages.Voucher
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration _config;
        private readonly string _connectionString;

        public IndexModel(IConfiguration config)
        {
            _config = config;
            _connectionString = _config.GetConnectionString("DefaultConnection");
        }

        public List<VoucherListItem> VoucherList { get; set; } = new();

        [BindProperty]
        public DateTime? FromDate { get; set; }

        [BindProperty]
        public DateTime? ToDate { get; set; }

        [BindProperty]
        public string? VoucherType { get; set; }

        public void OnGet()
        {
            LoadVouchers();
        }

        public void OnPost()
        {
            LoadVouchers();
        }

        private void LoadVouchers()
        {
            VoucherList.Clear();

            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("sp_GetVouchersByFilter", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@FromDate", (object?)FromDate ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@ToDate", (object?)ToDate ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@VoucherType", (object?)VoucherType ?? DBNull.Value);

            conn.Open();
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                VoucherList.Add(new VoucherListItem
                {
                    VoucherID = Convert.ToInt32(reader["VoucherID"]),
                    VoucherDate = Convert.ToDateTime(reader["VoucherDate"]),
                    ReferenceNo = reader["ReferenceNo"].ToString(),
                    VoucherType = reader["VoucherType"].ToString()
                });
            }
        }

        public class VoucherListItem
        {
            public int VoucherID { get; set; }
            public DateTime VoucherDate { get; set; }
            public string? ReferenceNo { get; set; }
            public string? VoucherType { get; set; }
        }
    }
}

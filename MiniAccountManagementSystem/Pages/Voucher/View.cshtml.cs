using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Data;

namespace MiniAccountManagementSystem.Pages.Voucher
{
    public class ViewModel : PageModel
    {
        private readonly IConfiguration _config;
        private readonly string _conn;

        public ViewModel(IConfiguration config)
        {
            _config = config;
            _conn = _config.GetConnectionString("DefaultConnection");
        }

        public VoucherHeader Header { get; set; } = new();
        public List<VoucherLine> Lines { get; set; } = new();

        public void OnGet(int id)
        {
            using var conn = new SqlConnection(_conn);
            using var cmd = new SqlCommand("sp_GetVoucherDetails", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@VoucherID", id);

            conn.Open();
            using var reader = cmd.ExecuteReader();

            // Header
            if (reader.Read())
            {
                Header = new VoucherHeader
                {
                    VoucherID = Convert.ToInt32(reader["VoucherID"]),
                    VoucherDate = Convert.ToDateTime(reader["VoucherDate"]),
                    ReferenceNo = reader["ReferenceNo"].ToString(),
                    VoucherType = reader["VoucherType"].ToString()
                };
            }

            // Entries
            if (reader.NextResult())
            {
                while (reader.Read())
                {
                    Lines.Add(new VoucherLine
                    {
                        AccountName = reader["AccountName"].ToString(),
                        DebitAmount = Convert.ToDecimal(reader["DebitAmount"]),
                        CreditAmount = Convert.ToDecimal(reader["CreditAmount"]),
                        Remarks = reader["Remarks"].ToString()
                    });
                }
            }
        }

        public class VoucherHeader
        {
            public int VoucherID { get; set; }
            public DateTime VoucherDate { get; set; }
            public string? ReferenceNo { get; set; }
            public string? VoucherType { get; set; }
        }

        public class VoucherLine
        {
            public string? AccountName { get; set; }
            public decimal DebitAmount { get; set; }
            public decimal CreditAmount { get; set; }
            public string? Remarks { get; set; }
        }
    }
}

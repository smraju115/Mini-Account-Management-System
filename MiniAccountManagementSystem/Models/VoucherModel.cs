using System.ComponentModel.DataAnnotations;

namespace MiniAccountManagementSystem.Models
{
    public class VoucherEntryModel
    {
        public int AccountID { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Debit amount cannot be negative")]
        public decimal DebitAmount { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Credit amount cannot be negative")]
        public decimal CreditAmount { get; set; }
        public string? Remarks { get; set; }
    }

    public class VoucherModel
    {
        public DateTime VoucherDate { get; set; } = DateTime.UtcNow;
        public string? ReferenceNo { get; set; }
        public string? VoucherType { get; set; }
        public List<VoucherEntryModel> Entries { get; set; } = new();
    }
}

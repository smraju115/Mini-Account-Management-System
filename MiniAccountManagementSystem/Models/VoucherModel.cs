namespace MiniAccountManagementSystem.Models
{
    public class VoucherEntryModel
    {
        public int AccountID { get; set; }
        public decimal DebitAmount { get; set; }
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

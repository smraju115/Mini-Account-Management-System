﻿namespace MiniAccountManagementSystem.Models
{
    public class ChartOfAccountModel
    {
        public int AccountID { get; set; }
        public string AccountName { get; set; }
        public int? ParentAccountID { get; set; }
        public string AccountType { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}

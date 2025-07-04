using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniAccountManagementSystem.Models
{
    public class ChartOfAccountModel
    {
        public int AccountID { get; set; }
        [Required(ErrorMessage = "Account name is required")]
        public string AccountName { get; set; }
        [Required(ErrorMessage = "ParrentID is required")]
        public int? ParentAccountID { get; set; }
        [Required(ErrorMessage = "Account type is required")]
        public string AccountType { get; set; }
        public bool IsActive { get; set; }
        [Required, Column(TypeName ="Date")]
        public DateTime CreatedDate { get; set; }
    }
}

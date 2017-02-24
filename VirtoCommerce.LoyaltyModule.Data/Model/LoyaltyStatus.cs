using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VirtoCommerce.LoyaltyModule.Data.Model
{
    public class LoyaltyStatus
    {
        [Required]
        [StringLength(64)]
        public string Id { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        [Column(TypeName = "Money")]
        public decimal Threshold { get; set; }
    }
}

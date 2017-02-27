using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VirtoCommerce.Platform.Core.Common;

namespace VirtoCommerce.LoyaltyModule.Data.Model
{
    public class LoyaltyStatus : AuditableEntity
    {
        [StringLength(255)]
        public string Name { get; set; }

        [Column(TypeName = "Money")]
        public decimal Threshold { get; set; }
    }
}

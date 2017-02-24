using System.Linq;
using VirtoCommerce.LoyaltyModule.Data.Model;
using VirtoCommerce.Platform.Core.Common;

namespace VirtoCommerce.LoyaltyModule.Data.Repositories
{
    public interface ICustomerLoyalityRepository : IRepository
    {
        IQueryable<Contact2DataEntity> Contacts2 { get; }
        IQueryable<LoyaltyStatus> LoyaltyStatuses { get; }

        void RemoveLoyalityStatusesByIds(string[] ids);
    }
}

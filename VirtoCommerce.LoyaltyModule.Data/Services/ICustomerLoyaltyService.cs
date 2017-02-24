using VirtoCommerce.LoyaltyModule.Data.Model;

namespace VirtoCommerce.LoyaltyModule.Data.Services
{
    public interface ICustomerLoyaltyService
    {
        LoyaltyStatus[] GetAll();
        void UpdateStatuses(LoyaltyStatus[] statuses);
        void DeleteStatuses(string[] ids);
        Contact2 UpdateCustomerLoyalty(Contact2 contact);
    }
}

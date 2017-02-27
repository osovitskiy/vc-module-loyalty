using VirtoCommerce.LoyaltyModule.Data.Model;

namespace VirtoCommerce.LoyaltyModule.Data.Services
{
    public interface ICustomerLoyaltyService
    {
        LoyaltyStatus[] GetAll();
        LoyaltyStatus GetById(string id);
        LoyaltyStatus CreateStatus(LoyaltyStatus status);
        void UpdateStatuses(LoyaltyStatus[] statuses);
        void DeleteStatuses(string[] ids);
        Contact2 UpdateCustomerLoyalty(Contact2 contact);
    }
}

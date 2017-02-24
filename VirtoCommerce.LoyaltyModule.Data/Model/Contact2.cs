using VirtoCommerce.Domain.Customer.Model;

namespace VirtoCommerce.LoyaltyModule.Data.Model
{
    public class Contact2 : Contact
    {
        public Contact2()
        {
            base.MemberType = typeof(Contact).Name;
        }

        public virtual LoyaltyStatus LoyaltyStatus { get; set; }
    }
}

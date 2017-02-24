using VirtoCommerce.CustomerModule.Data.Model;
using VirtoCommerce.Domain.Customer.Model;
using VirtoCommerce.Platform.Core.Common;

namespace VirtoCommerce.LoyaltyModule.Data.Model
{
    public class Contact2DataEntity : ContactDataEntity
    {
        public string LoyaltyStatusId { get; set; }
        public virtual LoyaltyStatus LoyaltyStatus { get; set; }

        public override Member ToModel(Member member)
        {
            var result = base.ToModel(member);

            var contact2 = result as Contact2;
            contact2.LoyaltyStatus = LoyaltyStatus;

            return contact2;
        }

        public override MemberDataEntity FromModel(Member member, PrimaryKeyResolvingMap pkMap)
        {
            base.FromModel(member, pkMap);

            var contact2 = member as Contact2;
            contact2.LoyaltyStatus = LoyaltyStatus;

            return this;
        }

        public override void Patch(MemberDataEntity memberDataEntity)
        {
            base.Patch(memberDataEntity);

            var contact2 = memberDataEntity as Contact2DataEntity;
            contact2.LoyaltyStatus = LoyaltyStatus;
        }
    }
}

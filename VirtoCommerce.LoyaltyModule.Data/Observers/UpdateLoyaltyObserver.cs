using System;
using VirtoCommerce.Domain.Customer.Model;
using VirtoCommerce.Domain.Customer.Services;
using VirtoCommerce.Domain.Order.Events;
using VirtoCommerce.LoyaltyModule.Data.Model;
using VirtoCommerce.LoyaltyModule.Data.Services;

namespace VirtoCommerce.LoyaltyModule.Data.Observers
{
    public class UpdateLoyaltyObserver : IObserver<OrderChangeEvent>
    {
        private readonly ICustomerLoyaltyService loyaltyService;
        private readonly IMemberService memberService;

        public UpdateLoyaltyObserver(ICustomerLoyaltyService loyaltyService, IMemberService memberService)
        {
            this.loyaltyService = loyaltyService;
            this.memberService = memberService;
        }

        public void OnError(Exception error)
        {
        }

        public void OnCompleted()
        {
        }

        public void OnNext(OrderChangeEvent changeEvent)
        {
            if (changeEvent.ModifiedOrder.Status == "Completed")
            {
                var contact = memberService.GetByIds(new[] {changeEvent.ModifiedOrder.CustomerId})[0] as Contact2;

                loyaltyService.UpdateCustomerLoyalty(contact);
                memberService.SaveChanges(new Member[] {contact});
            }
        }
    }
}

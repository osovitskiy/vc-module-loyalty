using System.Linq;
using VirtoCommerce.Domain.Order.Model;
using VirtoCommerce.Domain.Order.Services;
using VirtoCommerce.LoyaltyModule.Data.Model;
using VirtoCommerce.LoyaltyModule.Data.Repositories;
using VirtoCommerce.Platform.Data.Infrastructure;

namespace VirtoCommerce.LoyaltyModule.Data.Services
{
    public class CustomerLoyaltySerrviceImpl : ServiceBase, ICustomerLoyaltyService
    {
        private readonly ICustomerLoyalityRepository loyaltyRepository;
        private readonly ICustomerOrderSearchService orderSearchService;

        public CustomerLoyaltySerrviceImpl(ICustomerLoyalityRepository loyaltyRepository, ICustomerOrderSearchService orderSearchService)
        {
            this.loyaltyRepository = loyaltyRepository;
            this.orderSearchService = orderSearchService;
        }

        public LoyaltyStatus[] GetAll()
        {
            return loyaltyRepository.LoyaltyStatuses.ToArray();
        }

        public void UpdateStatuses(LoyaltyStatus[] statuses)
        {
            foreach (var status in statuses)
            {
                var existing = loyaltyRepository.LoyaltyStatuses.FirstOrDefault(x => x.Id == status.Id);

                if (existing == null)
                {
                    loyaltyRepository.Add(status);
                }
                else
                {
                    existing.Name = status.Name;
                    existing.Threshold = status.Threshold;
                }
            }

            CommitChanges(loyaltyRepository);
        }

        public void DeleteStatuses(string[] ids)
        {
            loyaltyRepository.RemoveLoyalityStatusesByIds(ids);

            CommitChanges(loyaltyRepository);
        }

        public Contact2 UpdateCustomerLoyalty(Contact2 contact)
        {
            var completedOrders = orderSearchService.SearchCustomerOrders(new CustomerOrderSearchCriteria()
            {
                CustomerId = contact.Id,
                Status = "Completed"
            });

            var total = completedOrders.Results.Sum(x => x.Total);
            var loyalty = loyaltyRepository.LoyaltyStatuses.OrderByDescending(x => x.Threshold).FirstOrDefault(x => x.Threshold <= total);

            contact.LoyaltyStatus = loyalty;

            return contact;
        }
    }
}

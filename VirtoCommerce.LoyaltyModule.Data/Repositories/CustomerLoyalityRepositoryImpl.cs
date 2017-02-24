using System.Data.Entity;
using System.Linq;
using VirtoCommerce.CustomerModule.Data.Repositories;
using VirtoCommerce.LoyaltyModule.Data.Model;
using VirtoCommerce.Platform.Data.Infrastructure.Interceptors;

namespace VirtoCommerce.LoyaltyModule.Data.Repositories
{
    public class CustomerLoyalityRepositoryImpl : CustomerRepositoryImpl, ICustomerLoyalityRepository
    {
        public CustomerLoyalityRepositoryImpl()
        {
        }

        public CustomerLoyalityRepositoryImpl(string nameOrConnectionString, params IInterceptor[] interceptors)
            : base(nameOrConnectionString, interceptors)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            #region Contact2

            modelBuilder.Entity<Contact2DataEntity>().HasKey(x => x.Id).Property(x => x.Id);
            modelBuilder.Entity<Contact2DataEntity>().HasOptional(x => x.LoyaltyStatus).WithMany().HasForeignKey(x => x.LoyaltyStatusId);
            modelBuilder.Entity<Contact2DataEntity>().ToTable("Contact2");

            #endregion

            #region LoyaltyStatus

            modelBuilder.Entity<LoyaltyStatus>().HasKey(x => x.Id).Property(x => x.Id);

            #endregion

            base.OnModelCreating(modelBuilder);
        }

        public IQueryable<Contact2DataEntity> Contacts2
        {
            get { return GetAsQueryable<Contact2DataEntity>(); }
        }

        public IQueryable<LoyaltyStatus> LoyaltyStatuses
        {
            get { return GetAsQueryable<LoyaltyStatus>(); }
        }

        public void RemoveLoyalityStatusesByIds(string[] ids)
        {
            var statuses = LoyaltyStatuses.Where(x => ids.Contains(x.Id));

            foreach (var status in statuses)
            {
                Remove(status);
            }
        }
    }
}

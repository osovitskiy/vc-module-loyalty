using System;
using Microsoft.Practices.Unity;
using VirtoCommerce.CustomerModule.Data.Model;
using VirtoCommerce.CustomerModule.Data.Repositories;
using VirtoCommerce.Domain.Customer.Model;
using VirtoCommerce.Domain.Order.Events;
using VirtoCommerce.LoyaltyModule.Data.Model;
using VirtoCommerce.LoyaltyModule.Data.Observers;
using VirtoCommerce.LoyaltyModule.Data.Repositories;
using VirtoCommerce.LoyaltyModule.Data.Services;
using VirtoCommerce.Platform.Core.Common;
using VirtoCommerce.Platform.Core.Modularity;
using VirtoCommerce.Platform.Data.Infrastructure;
using VirtoCommerce.Platform.Data.Infrastructure.Interceptors;

namespace VirtoCommerce.LoyaltyModule.Web
{
    public class Module : ModuleBase
    {
        private const string ConnectionStringName = "VirtoCommerce";
        private readonly IUnityContainer container;

        public Module(IUnityContainer container)
        {
            this.container = container;
        }

        public override void SetupDatabase()
        {
            using (var db = new CustomerLoyalityRepositoryImpl(ConnectionStringName, container.Resolve<AuditableInterceptor>()))
            {
                var initializer = new SetupDatabaseInitializer<CustomerLoyalityRepositoryImpl, Data.Migrations.Configuration>();
                initializer.InitializeDatabase(db);
            }
        }

        public override void Initialize()
        {
            base.Initialize();
            //container.RegisterType<IObserver<OrderChangeEvent>, UpdateLoyaltyObserver>("UpdateLoyaltyObserver");
            container.RegisterType<ICustomerLoyaltyService, CustomerLoyaltySerrviceImpl>();
            //container.RegisterType<ICustomerRepository>(new InjectionFactory(c => new CustomerLoyalityRepositoryImpl(ConnectionStringName, container.Resolve<AuditableInterceptor>(), new EntityPrimaryKeyGeneratorInterceptor())));
            container.RegisterType<ICustomerLoyalityRepository>(new InjectionFactory(c => new CustomerLoyalityRepositoryImpl(ConnectionStringName, container.Resolve<AuditableInterceptor>(), new EntityPrimaryKeyGeneratorInterceptor())));
        }

        public override void PostInitialize()
        {
            base.PostInitialize();
            AbstractTypeFactory<Member>.OverrideType<Contact, Contact2>().MapToType<Contact2DataEntity>();
            AbstractTypeFactory<MemberDataEntity>.OverrideType<ContactDataEntity, Contact2DataEntity>();
        }
    }
}

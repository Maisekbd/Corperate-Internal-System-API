using System;
using System.Data.Entity;
using DocumentsExplorer.BLL;
using DocumentsExplorer.BLL.Interface;
using DocumentsExplorer.Model.Models;
using DocumentsExplorer.Website.Controllers;
using DocumentsExplorer.Website.Models;
using IdentityManagement.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Repository.Pattern.Ef6;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;

namespace DocumentsExplorer.Website.App_Start
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your types here
            // container.RegisterType<IProductRepository, ProductRepository>();

            container
           // Register DbContext instead of IDataDataContext, which is now obsolete.
           //.RegisterType<IDataContextAsync, NorthwindContext>(new PerRequestLifetimeManager())
           .RegisterType<DbContext, DocumentsExplorerContext>(new PerRequestLifetimeManager())
           .RegisterType<IUnitOfWorkAsync, UnitOfWork>(new PerRequestLifetimeManager())
           .RegisterType<AccountController>(new InjectionConstructor())
            .RegisterType<ManageController>(new InjectionConstructor())
           .RegisterType(typeof(IRepositoryAsync<>), typeof(Repository<>))
           .RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>()
           .RegisterType<IMainCategoryBLL, MainCategoryBLL>()
           .RegisterType<ICouncilTypeBLL, CouncilTypeBLL>()
           .RegisterType<ISubCategoryBLL, SubCategoryBLL>()
           .RegisterType<ICountryBLL, CountryBLL>()
           .RegisterType<ICouncilMemberBLL, CouncilMemberBLL>()
           .RegisterType<IRoundBLL, RoundBLL>()
           .RegisterType<IMinutesOfMeetingBLL, MinutesOfMeetingBLL>()
           .RegisterType<IAttachmentBLL, AttachmentBLL>()
           .RegisterType<ICompanyBLL, CompanyBLL>()
           .RegisterType<IActivitySectorBLL, ActivitySectorBLL>()
           .RegisterType<IDecisionTypeBLL, DecisionTypeBLL>()
           .RegisterType<IDepartmentBLL, DepartmentBLL>()
           .RegisterType<IDecisionExecutionBLL, DecisionExecutionBLL>()
            .RegisterType<IDepartmentResponsibleBLL, DepartmentResponsibleBLL>()
           .RegisterType<IDecisionBLL, DecisionBLL>();

        }
    }
}

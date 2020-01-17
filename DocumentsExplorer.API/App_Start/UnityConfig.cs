using AAAID.Common;
using AAAID.HR.Entities;
using AAAID.HR.Service;
using AAAID.HR.ServiceInterface;
using DocumentsExplorer.API.App_Start;
using DocumentsExplorer.BLL;
using DocumentsExplorer.BLL.Interface;
using DocumentsExplorer.Model.Models;
using Repository.Pattern.Ef6;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using System;
using System.Data.Entity;
using Unity;
using Unity.AspNet.Mvc;
using Unity.Injection;

namespace DocumentsExplorer.API
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below.
            // Make sure to add a Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your type's mappings here.
            // container.RegisterType<IProductRepository, ProductRepository>();
            HRContext hr = new HRContext();
            hr.Database.Initialize(force: false);
            var unitOfWorkHr = new UnitOfWork(hr);
            container
                .RegisterType<HRContext>("HRContext", new PerRequestLifetimeManager())
                .RegisterType<DbContext, DocumentsExplorerContext>(new PerRequestLifetimeManager())
                .RegisterType<IApplicationContext, ApplicationContext>()
                .RegisterType(typeof(IRepositoryAsync<>), typeof(Repository<>))
                .RegisterType<IUnitOfWorkAsync, UnitOfWork>(new PerRequestLifetimeManager())
                .RegisterType<IMainCategoryBLL, MainCategoryBLL>()
                .RegisterType<ICouncilTypeBLL, CouncilTypeBLL>()
                .RegisterType<ISubCategoryBLL, SubCategoryBLL>()
                .RegisterType<ICountryBLL, CountryBLL>()
                .RegisterType<ICouncilMemberBLL, CouncilMemberBLL>()
                .RegisterType<IRoundBLL, RoundBLL>()
                .RegisterType<IMinutesOfMeetingBLL, MinutesOfMeetingBLL>()
                .RegisterType<IMeetingBLL, MeetingBLL>()
                .RegisterType<IAttachmentBLL, AttachmentBLL>()
                .RegisterType<ICompanyBLL, CompanyBLL>()
                .RegisterType<IActivitySectorBLL, ActivitySectorBLL>()
                .RegisterType<IDecisionTypeBLL, DecisionTypeBLL>()
                .RegisterType<IReferenceTypeBLL, ReferenceTypeBLL>()
                .RegisterType<IDepartmentBLL, DepartmentBLL>()
                .RegisterType<IDecisionExecutionBLL, DecisionExecutionBLL>()
                .RegisterType<IDepartmentResponsibleBLL, DepartmentResponsibleBLL>()
                .RegisterType<IDecisionBLL, DecisionBLL>()
                .RegisterType<INotificationBLL, NotificationBLL>()
                .RegisterType<IEmployeeService, EmployeeService>(new PerRequestLifetimeManager(), new InjectionConstructor(new Repository.Pattern.Ef6.Repository<Employee>(hr, unitOfWorkHr, new ApplicationContext()), unitOfWorkHr))
                .RegisterType<IDepartmentService, DepartmentService>(new PerRequestLifetimeManager(), new InjectionConstructor(new Repository.Pattern.Ef6.Repository<AAAID.HR.Entities.Department>(hr, unitOfWorkHr, new ApplicationContext()), unitOfWorkHr));
        }
    }
}
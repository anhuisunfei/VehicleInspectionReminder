using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Autofac;
using Autofac.Extras.Quartz;
using Autofac.Integration.Mvc;
using Quartz;
using Quartz.Impl;
using VehicleInspectionReminder.Data.Infrastructure;
using VehicleInspectionReminder.Data.Repository;
using VehicleInspectionReminder.Service;
using VehicleInspectionReminder.Web.Jobs;

namespace VehicleInspectionReminder.Web.App_Start
{
	public class Bootstarpper
	{
		public static void Run()
		{
			SetAutofacContiner();
		}

		private static void SetAutofacContiner()
		{
			var builder = new ContainerBuilder();
			builder.RegisterControllers(typeof(MvcApplication).Assembly);
			builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
			builder.RegisterType<DataBaseFactory>().As<IDataBaseFactory>().InstancePerRequest();
			builder.RegisterAssemblyTypes(typeof(UserRepository).Assembly)
			.Where(t => t.Name.EndsWith("Repository"))
			.AsImplementedInterfaces().InstancePerRequest();
			builder.RegisterAssemblyTypes(typeof(UserService).Assembly)
		   .Where(t => t.Name.EndsWith("Service"))
		   .AsImplementedInterfaces().InstancePerRequest();
			builder.RegisterFilterProvider();


			// Quartz.net Injection
			builder.RegisterModule(new QuartzAutofacFactoryModule());
			builder.RegisterModule(new QuartzAutofacJobsModule(typeof(EmailingJob).Assembly));
			builder.RegisterType<EmailingJob>().As<IJob>().InstancePerLifetimeScope();
			builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
			builder.RegisterType<DataBaseFactory>().As<IDataBaseFactory>().InstancePerLifetimeScope();
			builder.RegisterType<NotificationMessageRepository>().As<INotificationMessageRepository>().InstancePerLifetimeScope();
			builder.RegisterType<NotificationMessageService>().As<INotificationMessageService>().InstancePerLifetimeScope();
			builder.RegisterType<OwnerInfoRepository>().As<IOwnerInfoRepository>().InstancePerLifetimeScope();
			builder.RegisterType<OwnerInfoService>().As<IOwnerInfoService>().InstancePerLifetimeScope();
            builder.RegisterType<VehicleInfoRepository>().As<IVehicleInfoRepository>().InstancePerLifetimeScope();
            builder.RegisterType<VehicleInfoService>().As<IVehicleInfoService>().InstancePerLifetimeScope();
			IContainer container = builder.Build(); 
			
			DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
		}
	}
}
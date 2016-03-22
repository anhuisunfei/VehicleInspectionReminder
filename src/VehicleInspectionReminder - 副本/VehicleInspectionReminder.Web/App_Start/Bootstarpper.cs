using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using VehicleInspectionReminder.Data.Infrastructure;
using VehicleInspectionReminder.Data.Repository;
using VehicleInspectionReminder.Service;

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
			IContainer container = builder.Build();
			DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
		}
	}
}
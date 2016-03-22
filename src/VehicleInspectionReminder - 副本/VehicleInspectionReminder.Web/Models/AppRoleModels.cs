using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;

namespace VehicleInspectionReminder.Web.Models
{
	public class AppRole : IdentityRole
	{
		public AppRole() : base() { }
		public AppRole(string name) : base(name) { }
	}

	public class AppRoleManager : RoleManager<AppRole>
	{
		public AppRoleManager(RoleStore<AppRole> store)
			: base(store)
		{
		}

	 
	}

}
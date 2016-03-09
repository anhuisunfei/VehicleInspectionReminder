using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using VehicleInspectionReminder.Web.Models;

namespace VehicleInspectionReminder.Web.Migrations
{
	using System;
	using System.Data.Entity;
	using System.Data.Entity.Migrations;
	using System.Linq;

	internal sealed class Configuration : DbMigrationsConfiguration<VehicleInspectionReminder.Web.Models.ApplicationDbContext>
	{
		public Configuration()
		{
			AutomaticMigrationsEnabled = false;
		}

		protected override void Seed(VehicleInspectionReminder.Web.Models.ApplicationDbContext context)
		{
			//  This method will be called after migrating to the latest version.

			//  You can use the DbSet<T>.AddOrUpdate() helper extension method 
			//  to avoid creating duplicate seed data. E.g.
			//
			//    context.People.AddOrUpdate(
			//      p => p.FullName,
			//      new Person { FullName = "Andrew Peters" },
			//      new Person { FullName = "Brice Lambson" },
			//      new Person { FullName = "Rowan Miller" }
			//    );
			//
			var passwordHash = new PasswordHasher();
			string password = passwordHash.HashPassword("sunf");
			context.Users.AddOrUpdate(u => u.UserName,
				new ApplicationUser
				{
					UserName = "sunf",
					PasswordHash = password,
					Email = "anhuisunfei@gmail.com"
				},new ApplicationUser
				{
					UserName="baiy",
					PasswordHash = password,
					Email = "baiy@gmail.com" 
				});
			context.Roles.AddOrUpdate(r => r.Name, new AppRole
			{
				Name = "Admin" // 管理员
			},new AppRole
			{
				Name="CarOwner" // 车主
			});
			
		}
	}
}

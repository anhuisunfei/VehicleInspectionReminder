using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace VehicleInspectionReminder.Web.Controllers
{
	
	public class HomeController : Controller
	{
		[Authorize(Roles = "Admin")]
		public ActionResult Index()
		{
			return View();
		}

		[Authorize(Roles = "CarOwner")]
		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";
			var userIdentity = (ClaimsIdentity)User.Identity;
			var claims = userIdentity.Claims;
			var roleClaimType = userIdentity.RoleClaimType;
			var roles = claims.Where(c => c.Type == ClaimTypes.Role).ToList();
			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}
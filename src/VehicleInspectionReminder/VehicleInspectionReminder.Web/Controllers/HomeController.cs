using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using VehicleInspectionReminder.Model;
using VehicleInspectionReminder.Service;

namespace VehicleInspectionReminder.Web.Controllers
{

	public class HomeController : Controller
	{
		private readonly IBrandService _brandService;

		public HomeController(IBrandService brandService)
		{
			_brandService = brandService;
		}

		[Authorize]
		public ActionResult Index()
		{
			//_brandService.AddBrand(new Brand()
			//{
			//	 BrandName = "奥迪"
			//});
			var list = _brandService.GetAll();
			ViewBag.BrandList = list;
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

		[Authorize(Roles = "CarOwner")]
		public ActionResult GetCheckNotification()
		{
			string userId = User.Identity.GetUserId();
			Random random = new Random(50);

			var list = Enumerable.Range(0, 2).Select(p =>
				new
				{
					Plate = "皖A123" + random.Next(10, 99),
					RemainDay = random.Next(0, 20)
				}).ToList();
			return Json(list, JsonRequestBehavior.AllowGet);
		}
	}
}
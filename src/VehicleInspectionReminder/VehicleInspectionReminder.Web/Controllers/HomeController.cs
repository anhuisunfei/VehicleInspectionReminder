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
        private readonly IVehicleInfoService _vehicleInfoService;
        private readonly IOwnerInfoService _ownerInfoService;
        public HomeController(IBrandService brandService, IVehicleInfoService vehicleInfoService, IOwnerInfoService ownerInfoService)
        {
            _brandService = brandService;
            _vehicleInfoService = vehicleInfoService;
            _ownerInfoService = ownerInfoService;
        }

        [Authorize]
        public ActionResult Welcome()
        {
            return View();
        }

        [Authorize(Roles = "CarOwner")]
        public ActionResult Index()
        {
            //_brandService.AddBrand(new Brand()
            //{
            //	 BrandName = "奥迪"
            ////});
            //var list = _brandService.GetAll();
            //ViewBag.BrandList = list;
            var list = _vehicleInfoService.GetUserCars(new Guid(User.Identity.GetUserId()));

            ViewBag.CarList = list;
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

        /// <summary>
        /// 车主列表
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Admin,CarCheck,TrafficPolice")]
        public ActionResult CarOwnerList()
        {
            var list = _ownerInfoService.GetAll();
            ViewBag.OwnerList = list;
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
            var carList = _vehicleInfoService.GetUserCars(new Guid(userId));

            var list = carList.Select(p => new
           {
               Plate = p.Plate,
               RemainDay = _vehicleInfoService.GetNextRemainDay(p.Plate)
           });
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}
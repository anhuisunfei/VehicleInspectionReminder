using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VehicleInspectionReminder.Model;
using VehicleInspectionReminder.Service;

namespace VehicleInspectionReminder.Web.Controllers
{
    public class VehicleTypeController : Controller
    {
        private IVehicleTypeService _vehicleTypeService;

        public VehicleTypeController(IVehicleTypeService vehicleTypeService)
        {
            _vehicleTypeService = vehicleTypeService;
        }

       [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var _list = _vehicleTypeService.GetAll();
            ViewBag.List = _list;
            return View();
        }

       [HttpPost]
       [Authorize(Roles = "Admin")]
       public ActionResult AddVehicleType(string name)
       {

           if (string.IsNullOrWhiteSpace(name))
           {
               ViewBag.Message = "请输入违章类型名称";

               var _list = _vehicleTypeService.GetAll();
               ViewBag.List = _list;
               return View("Index");
           }
           _vehicleTypeService.AddVehicleType(new VehicleType
           {
               VehicleTypeName = name
           });

           return RedirectToAction("Index");

       }
       [Authorize(Roles = "Admin")]
       [HttpPost]
       public ActionResult Delete(int id)
       {
           _vehicleTypeService.Delete(id);
           return Json(id);
       }
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VehicleInspectionReminder.Model;
using VehicleInspectionReminder.Service;

namespace VehicleInspectionReminder.Web.Controllers
{
    public class BrandController:Controller
    {

        private IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }
		[Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var _list = _brandService.GetAll();
            ViewBag.List = _list;
            return View();
        }

        [HttpPost]
        [Authorize(Roles="Admin")]
        public ActionResult AddBrand(string name)
        {
            
            if (string.IsNullOrWhiteSpace(name))
            {
                ViewBag.Message = "请输入品牌名称";

                var _list = _brandService.GetAll();
                ViewBag.List = _list;
                return View("Index");
            }
            _brandService.AddBrand(new Brand
            {
                 BrandName=name
            });

            return RedirectToAction("Index");


        }
        [Authorize(Roles="Admin")]
        [HttpPost]
        public ActionResult Delete(int id)
        {
            _brandService.Delete(id);
            return Json(id);
        }
    }
}
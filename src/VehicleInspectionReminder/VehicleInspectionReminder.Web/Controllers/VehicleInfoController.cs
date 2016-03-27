using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using VehicleInspectionReminder.Model;
using VehicleInspectionReminder.Service;
using VehicleInspectionReminder.Web.Models;

namespace VehicleInspectionReminder.Web.Controllers
{
    public class VehicleInfoController : Controller
    {
        private IVehicleInfoService _vehicleInfoService;
        private IVehicleTypeService _vehicleTypeService;
        private IBrandService _brandService;
        public VehicleInfoController(IVehicleInfoService vehicleInfoService, IVehicleTypeService vehicleTypeService,IBrandService brandService)
        {
            _vehicleInfoService = vehicleInfoService;
            _vehicleTypeService = vehicleTypeService;
            _brandService = brandService;
        }


        //
        // GET: /VehicleInfo/
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult InsertNew()
        {
            var _list = _vehicleInfoService.GetAll();
            var typeList = _vehicleTypeService.GetAll();
            var brandList = _brandService.GetAll();
            ICollection<SelectListItem> dropTypeList = new List<SelectListItem>
            {
                 new SelectListItem{
                Text="请选择车辆类型",
                 Value="0"
                }
            };

            if (typeList != null)
            {
                foreach (var item in typeList)
                {
                    dropTypeList.Add(new SelectListItem
                    {
                        Text = item.VehicleTypeName,
                        Value = item.Id.ToString()
                    });
                }

            }
            ICollection<SelectListItem> dropBrandList = new List<SelectListItem>
            {
                 new SelectListItem{
                Text="请选择车辆品牌",
                 Value="0"
                }
            };

            if (brandList != null)
            {
                foreach (var item in brandList)
                {
                    dropBrandList.Add(new SelectListItem
                    {
                        Text = item.BrandName,
                        Value = item.Id.ToString()
                    });
                }

            }


            ViewBag.typeList = dropTypeList;
            ViewBag.brandList = dropBrandList;
            ViewBag.List = _list;
            //  this.GetVehicleTypeList();

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AddVehicleInfo(VehicleInfoModel model)
        {

            if (string.IsNullOrWhiteSpace(model.Plate))
            {
                ViewBag.Message = "请输入汽车牌号 ";

                var _list = _vehicleInfoService.GetAll();
                ViewBag.List = _list;
                return View("Index");
            }
            _vehicleInfoService.AddVehicleInfo(new VehicleInfo
            {
                VehicleTypeId = model.VehicleTypeId,
                Plate = model.Plate,
                DeliveryTtime = model.DeliveryTtime,
                PurchaseDate = model.PurchaseDate,
                InsuranceStatus = Boolean.Parse(model.InsuranceStatus.ToString()),
                FireCar = model.FireCar,
                LightCondition = model.LightCondition,
                PlateIsIntact = model.PlateIsIntact,
                LastInspectionTime = DateTime.Now.AddDays(7),//车辆检测时间加7天为这次检测时间
                NextInspectionTime = _vehicleInfoService.GetNextInspectionTime(model.VehicleTypeId, model.Plate, 1, model.PurchaseDate)
            });

            _vehicleInfoService.GetNextRemainDay(model.Plate);
            return RedirectToAction("Index");

        }

        //public void GetVehicleTypeList()
        //{
        //    var list = _vehicletypeService.GetAll();
        //    ViewData["VehicleTypeList"] = new SelectList(list, "Id", "VehicleTypeName");

        //}


    }
}

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
        private IOwnerInfoService _ownerInfoService;


        public VehicleInfoController(IVehicleInfoService vehicleInfoService, IVehicleTypeService vehicleTypeService, IBrandService brandService, IOwnerInfoService ownerInfoService)
        {
            _vehicleInfoService = vehicleInfoService;
            _vehicleTypeService = vehicleTypeService;
            _brandService = brandService;
            _ownerInfoService = ownerInfoService;
        }


        //
        // GET: /VehicleInfo/
        public ActionResult Index()
        {
            return View();
        }

        #region 新车登记
        [Authorize(Roles = "Admin")]
        public ActionResult InsertNew()
        {
            var _list = _vehicleInfoService.GetAll();
            var typeList = _vehicleTypeService.GetAll();
            var brandList = _brandService.GetAll();
            var ownerList = _ownerInfoService.GetAll();

            ICollection<SelectListItem> dropOwnerList = new List<SelectListItem>
            {
                 new SelectListItem{
                Text="请选择车主",
                 Value="0"
                }
            };

            if (typeList != null)
            {
                foreach (var item in ownerList)
                {
                    dropOwnerList.Add(new SelectListItem
                    {
                        Text = item.UserName,
                        Value = item.Id.ToString()
                    });
                }

            }


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
            ViewBag.ownerList = dropOwnerList;
            //  this.GetVehicleTypeList();

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AddVehicleInfo(VehicleInfoModel model, string insuranceStatus, string fireCar, string lightCondition, string plateIsIntact)
        {

            if (string.IsNullOrWhiteSpace(model.Plate))
            {
                ViewBag.Message = "请输入汽车牌号 ";

                var _list = _vehicleInfoService.GetAll();
                ViewBag.List = _list;
                return View("InsertNew");
            }
            _vehicleInfoService.AddVehicleInfo(new VehicleInfo
            {
                OwnerId = model.OwnerId,
                BrandId = model.BrandId,
                VehicleTypeId = model.VehicleTypeId,
                Plate = model.Plate,
                DeliveryTtime = model.DeliveryTtime,
                PurchaseDate = model.PurchaseDate,
                InsuranceStatus = insuranceStatus == "1" ? true : false,
                FireCar = (FireCarType)Enum.Parse(typeof(FireCarType), fireCar),
                LightCondition = (LightConditionType)Enum.Parse(typeof(LightConditionType), lightCondition),
                PlateIsIntact = (PlateType)Enum.Parse(typeof(PlateType), plateIsIntact),
                LastInspectionTime = DateTime.Now.AddDays(7),//车辆检测时间加7天为这次检测时间
                NextInspectionTime = _vehicleInfoService.GetNextInspectionTime(model.VehicleTypeId, model.Plate, 1, model.PurchaseDate)
            });

            int xx = _vehicleInfoService.GetNextRemainDay(model.Plate);
            return RedirectToAction("InsertNew");

        }

        #endregion

        #region 车辆保养

        /// <summary>
        /// 根据车牌号码，获取要保养的车辆信息
        /// </summary>
        /// <param name="plate"></param>
        /// <returns></returns>
        public ActionResult Tenance(string plate)
        {
             
            VehicleInfoModel model = new VehicleInfoModel();
            try
            {

                var _list = _vehicleInfoService.GetAll();
                ViewBag.List = _list;
                VehicleInfo _info = _list.Where(m => m.Plate == plate).SingleOrDefault();

                if (_info != null)
                {


                    //model.Owner.UserName = _ownerInfoService.GetAll().Where(o => o.Id == _info.OwnerId).SingleOrDefault().UserName;
                    //model.VehicleBrand.Id = _info.VehicleBrand.Id;
                    //model.VehicleBrand.BrandName = _info.VehicleBrand.BrandName;
                    //model.VehicleTypeId = _info.VehicleTypeId;
                    //model.VehicleType.VehicleTypeName = _info.VehicleType.VehicleTypeName;
                    model.OwnerId = _info.OwnerId;
                    model.Plate = _info.Plate;
                    model.DeliveryTtime = _info.DeliveryTtime;
                    model.PurchaseDate = _info.PurchaseDate;

                }


            }
            catch (Exception)
            {
            }

            return View(model);
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Tenance(VehicleInfoModel model, string insuranceStatus, string fireCar, string lightCondition, string plateIsIntact)
        {
            _vehicleInfoService.AddVehicleInfo(new VehicleInfo
            {
                OwnerId = model.OwnerId,
                BrandId = model.BrandId,
                VehicleTypeId = model.VehicleTypeId,
                Plate = model.Plate,
                DeliveryTtime = model.DeliveryTtime,
                PurchaseDate = model.PurchaseDate,
                InsuranceStatus = insuranceStatus == "1" ? true : false,
                FireCar = (FireCarType)Enum.Parse(typeof(FireCarType), fireCar),
                LightCondition = (LightConditionType)Enum.Parse(typeof(LightConditionType), lightCondition),
                PlateIsIntact = (PlateType)Enum.Parse(typeof(PlateType), plateIsIntact),
                LastInspectionTime = DateTime.Now.AddDays(7),//车辆检测时间加7天为这次检测时间
                NextInspectionTime = _vehicleInfoService.GetNextInspectionTime(model.VehicleTypeId, model.Plate, 2, model.PurchaseDate)
            });

            int xx = _vehicleInfoService.GetNextRemainDay(model.Plate);
            return RedirectToAction("InsertNew");
        }


        #endregion

    }
}

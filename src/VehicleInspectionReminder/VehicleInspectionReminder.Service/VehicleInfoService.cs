using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleInspectionReminder.Data.Infrastructure;
using VehicleInspectionReminder.Data.Repository;
using VehicleInspectionReminder.Model;

namespace VehicleInspectionReminder.Service
{
    public class VehicleInfoService : IVehicleInfoService
    {
        private IUnitOfWork _unitofwork;
        private IVehicleInfoRepository _vehicleInfoRepository;
        private IVehicleTypeRepository _vehicleTypeRepository;

        public VehicleInfoService(IUnitOfWork unitofwork, IVehicleInfoRepository vehicleInfoRepository, IVehicleTypeRepository vehicleTypeRepository)
        {
            _unitofwork = unitofwork;
            _vehicleInfoRepository = vehicleInfoRepository;
            _vehicleTypeRepository = vehicleTypeRepository;
        }

        public void AddVehicleInfo(VehicleInfo model)
        {
            //在规定检验期限内， 同一车牌的车辆 不用重复插入检验信息

            if (_vehicleInfoRepository.GetManay(p => p.Plate == model.Plate).Any())
            {
                throw new ArgumentException("同一车牌的车辆已存在");
            }

            _vehicleInfoRepository.Insert(model);
            _unitofwork.Commit();
        }

        public void UpdateVehicleInfo(VehicleInfo model)
        {
            _vehicleInfoRepository.Update(model);
            _unitofwork.Commit();
        }

        public IEnumerable<VehicleInfo> GetAll()
        {
            return _vehicleInfoRepository.GetAll();
        }

        public void Delete(int id)
        {
            var entity = _vehicleInfoRepository.GetById(id);
            _vehicleInfoRepository.Delete(entity);
            _unitofwork.Commit();
        }


        public int GetNextRemainDay(string plate)
        {
            int day = 0;
            DateTime nextTime = DateTime.Now;
            try
            {
                //下一次车检日期也就是下一次车检时间
                //  DateTime nextTime = DateTime.Parse(_vehicleInfoRepository.GetManay(o => o.Plate == plate).First().NextInspectionTime.ToString());
                if (_vehicleInfoRepository.GetManay(o => o.Plate == plate).Any())
                {
                    nextTime = DateTime.Parse(_vehicleInfoRepository.GetManay(o => o.Plate == plate).First().NextInspectionTime.ToString());
                }

                // 下一次车检日期 - 当前系统时间  = 剩余天数
                TimeSpan ts = DateTime.Parse(nextTime.ToShortDateString()) - DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
                day = ts.Days;
            }
            catch (Exception)
            {
                day = 0;
            }

            return day;
        }

        public IEnumerable<VehicleInfo> GetUserCars(Guid aspnetUserID)
        {
            return _vehicleInfoRepository.GetManay(p => p.Owner.AspNetUserId == aspnetUserID).ToList();
        }

        public VehicleInfo GetCarInfo(string plate)
        {
            return _vehicleInfoRepository.GetManay(p => p.Plate==plate).SingleOrDefault();
        }

        public DateTime GetNextInspectionTime(int VehicleTypeId, string plate, int typeId, DateTime PurchaseDate)
        {
            DateTime nextInspectionTime = DateTime.Now;
            //获取该类型车辆车检周期
            VehicleType vehicleType = _vehicleTypeRepository.GetById(VehicleTypeId);

            string nowDate = DateTime.Now.ToShortDateString();//此次车检日期， 默认为当前日期

            //如果typeId =1   则表明该车是第一次车检 ， 则用当前日期+检测周期作为下一次车检日期
            if (typeId == 1)
            {
                //购车日期+第一阶段周期 > 当前时间，说明还在第一阶段检车期
                if (PurchaseDate.AddYears(vehicleType.FirstJY_Year) > DateTime.Parse(nowDate))
                {
                    //购车日期+周期内时间=下一次车检日期
                    nextInspectionTime = PurchaseDate.AddMonths(vehicleType.FirstJY_Cycle);
                }//否则判断是否存在第三检测期， 若不存在  则按第二阶段计算下次车检日期
                else if (vehicleType.LastJY_Year == 0)
                {
                    nextInspectionTime = PurchaseDate.AddMonths(vehicleType.SecondJY_Cycle);
                }
                else
                {
                    //购车日期+第三阶段<当前日期 ， 说明处于第二阶段 
                    if (PurchaseDate.AddYears(vehicleType.LastJY_Year) > DateTime.Parse(nowDate))
                    {
                        nextInspectionTime = PurchaseDate.AddMonths(vehicleType.SecondJY_Cycle);
                    }
                    else
                    {
                        nextInspectionTime = PurchaseDate.AddMonths(vehicleType.LastJY_Cycle);
                    }

                }

                return nextInspectionTime;
            }
            else
            {
                //获取该车信息
                VehicleInfo vehicleInfo = _vehicleInfoRepository.GetManay(o => o.Plate == plate).First();

                //购车日期+第一阶段周期 > 当前时间，说明还在第一阶段检车期
                if (PurchaseDate.AddYears(vehicleType.FirstJY_Year) > DateTime.Parse(nowDate))
                {
                    //上一次的车检时间 加上周期内时间 就是下一次车检日期 
                    nextInspectionTime = DateTime.Parse(vehicleInfo.LastInspectionTime.ToString()).AddMonths(vehicleType.FirstJY_Cycle);
                }//否则判断是否存在第三检测期， 若不存在  则按第二阶段计算下次车检日期
                else if (vehicleType.LastJY_Year == 0)
                {
                    nextInspectionTime = DateTime.Parse(vehicleInfo.LastInspectionTime.ToString()).AddMonths(vehicleType.SecondJY_Cycle);
                }
                else
                {
                    //购车日期+第三阶段<当前日期 ， 说明处于第二阶段 
                    if (PurchaseDate.AddYears(vehicleType.LastJY_Year) > DateTime.Parse(nowDate))
                    {
                        nextInspectionTime = DateTime.Parse(vehicleInfo.LastInspectionTime.ToString()).AddMonths(vehicleType.SecondJY_Cycle);
                    }
                    else
                    {
                        nextInspectionTime = DateTime.Parse(vehicleInfo.LastInspectionTime.ToString()).AddMonths(vehicleType.LastJY_Cycle);
                    }

                }

            }

            return nextInspectionTime;

        }

    }
}



public interface IVehicleInfoService
{
    void AddVehicleInfo(VehicleInfo model);

    IEnumerable<VehicleInfo> GetAll();

    void Delete(int id);

    /// <summary>
    /// 根据车牌号 计算出距离下次车检剩余天数
    /// 用在车主进入系统时提醒
    /// </summary>
    /// <param name="plate">车牌号</param>
    /// <returns></returns>
    int GetNextRemainDay(string plate);

    /// <summary>
    /// 获取车主所有车辆信息
    /// </summary>
    /// <param name="aspnetUserID"></param>
    /// <returns></returns>
    IEnumerable<VehicleInfo> GetUserCars(Guid aspnetUserID);

    /// <summary>
    /// 根据车辆类型， 车牌号 和 本次车检日期 计算出下次车检日期
    /// </summary>
    /// <param name="VehicleTypeId">车辆类型ID</param>
    /// <param name="plate">车牌号ID</param>
    /// <param name="typeId">新车录入：1， 老车保养：2</param>
    /// <param name="PurchaseDate">购车日期</param>
    /// <returns></returns>
    DateTime GetNextInspectionTime(int VehicleTypeId, string plate, int typeId, DateTime PurchaseDate);

    /// <summary>
    /// 获取指定车辆信息
    /// </summary>
    /// <param name="plate"></param>
    /// <returns></returns>
    VehicleInfo GetCarInfo(string plate);

    void UpdateVehicleInfo(VehicleInfo model);
}



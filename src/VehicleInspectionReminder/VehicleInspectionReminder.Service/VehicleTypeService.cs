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
    public class VehicleTypeService : IVehicleTypeService
    {
        private IUnitOfWork _unitofwork;
        private IVehicleTypeRepository _vehicleTypeRepository;

        public VehicleTypeService(IUnitOfWork unitofwork, IVehicleTypeRepository vehicleTypeRepository)
        {
            _unitofwork = unitofwork;
            _vehicleTypeRepository = vehicleTypeRepository;
        }

        public void AddVehicleType(VehicleType model)
        {
            if (_vehicleTypeRepository.GetManay(p => p.VehicleTypeName == model.VehicleTypeName).Any())
            {
                throw new ArgumentException("违章类型已存在");
            }

            _vehicleTypeRepository.Insert(model);
            _unitofwork.Commit();
        }

        public IEnumerable<VehicleType> GetAll()
        {
            return _vehicleTypeRepository.GetAll();
        }

        public void Delete(int id)
        {
            var entity = _vehicleTypeRepository.GetById(id);
            _vehicleTypeRepository.Delete(entity);
            _unitofwork.Commit();
        }
    }
}

public interface IVehicleTypeService
{
    void AddVehicleType(VehicleType model);

    IEnumerable<VehicleType> GetAll();

    void Delete(int id);

}

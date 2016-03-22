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
    public class BrandService : IBrandService
    {
        private IUnitOfWork _unitofwork;
        private IBrandRepository _brandRepository;

        public BrandService(IUnitOfWork unitofwork, IBrandRepository brandrepository)
        {
            _unitofwork = unitofwork;
            _brandRepository = brandrepository;
        }



        public void AddBrand(Brand model)
        {
            if (_brandRepository.GetManay(p => p.BrandName == model.BrandName).Any())
            {
                throw new ArgumentException("品牌已存在");
            }
            _brandRepository.Insert(model);
            _unitofwork.Commit();
        }

        public IEnumerable<Brand> GetAll()
        {
            return _brandRepository.GetAll();
        }

        public void Delete(int id)
        {
            var entity = _brandRepository.GetById(id);
            _brandRepository.Delete(entity);
            _unitofwork.Commit();
        }
    }

    public interface IBrandService
    {
        void AddBrand(Brand model);

        IEnumerable<Brand> GetAll();

        void Delete(int id);
    }
}
 
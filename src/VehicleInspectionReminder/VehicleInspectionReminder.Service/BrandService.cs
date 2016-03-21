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
		private readonly IBrandRepository _brandRepository;
		private readonly IUnitOfWork _unitOfWork;
		public BrandService(IBrandRepository brandRepository,IUnitOfWork unitOfWork)
		{
			_brandRepository = brandRepository;
			_unitOfWork = unitOfWork;
		}
		public void AddBrand(Brand brand)
		{
			_brandRepository.Insert(brand);
			_unitOfWork.Commit();
		}

		public IEnumerable<Brand> GetAll()
		{
			return _brandRepository.GetAll();
		}
	}

	public interface IBrandService
	{
		void AddBrand(Brand brand);

		IEnumerable<Brand> GetAll();
	}
}

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
	public class OwnerInfoService:IOwnerInfoService
	{

		private readonly IOwnerInfoRepository _ownerInfoRepository;
		private readonly IUnitOfWork _unitOfWork;

		public OwnerInfoService(IOwnerInfoRepository ownerInfoRepository, IUnitOfWork unitOfWork)
		{
			_ownerInfoRepository = ownerInfoRepository;
			_unitOfWork = unitOfWork;
		}
		public void AddOwnerInfo(OwnerInfo model)
		{
			 _ownerInfoRepository.Insert(model);
			_unitOfWork.Commit();
		}

		public OwnerInfo GetOwnerInfoByUserId(Guid id)
		{
			return _ownerInfoRepository.Get(p => p.AspNetUserId == id);
		}


		public void UpdateOwnerInfo(OwnerInfo model)
		{
			_ownerInfoRepository.Update(model);
			_unitOfWork.Commit();
		}

        public IEnumerable<OwnerInfo> GetAll()
        {
            return _ownerInfoRepository.GetAll().ToList();
        }
	}

	public interface IOwnerInfoService
	{
		void AddOwnerInfo(OwnerInfo model);
		void UpdateOwnerInfo(OwnerInfo model);

		OwnerInfo GetOwnerInfoByUserId(Guid id);

        IEnumerable<OwnerInfo> GetAll();
	}
}

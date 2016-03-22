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
	public class UserService:IUserService
	{
		private IUserRepository _userRepository;
		private IUnitOfWork _unitOfWork;

		public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork)
		{
			_userRepository = userRepository;
			_unitOfWork = unitOfWork;
		}
		public User Get(int id)
		{
			return _userRepository.GetById(id);
		}
	}

	public interface IUserService
	{
		User Get(int id);
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleInspectionReminder.Data.Infrastructure;
using VehicleInspectionReminder.Model;

namespace VehicleInspectionReminder.Data.Repository
{
	public class UserRepository : EfRepositoryBase<User>, IUserRepository
	{
		public UserRepository(IDataBaseFactory factory)
			: base(factory)
		{

		}
	}

	public interface IUserRepository : IRepository<User>
	{

	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleInspectionReminder.Data.Infrastructure;
using VehicleInspectionReminder.Model;

namespace VehicleInspectionReminder.Data.Repository
{
	public class OwnerInfoRepository : EfRepositoryBase<OwnerInfo>, IOwnerInfoRepository
	{
		public OwnerInfoRepository(IDataBaseFactory factory)
			: base(factory)
		{

		}
	}

	public interface IOwnerInfoRepository : IRepository<OwnerInfo>
	{

	}
}

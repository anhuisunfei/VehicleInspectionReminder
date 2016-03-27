using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleInspectionReminder.Data.Infrastructure;
using VehicleInspectionReminder.Model;

namespace VehicleInspectionReminder.Data.Repository
{
    public class VehicleInfoRepository : EfRepositoryBase<VehicleInfo>, IVehicleInfoRepository
    {
        public VehicleInfoRepository(IDataBaseFactory factory) : base(factory) { }
    }
   
    public interface IVehicleInfoRepository:IRepository<VehicleInfo>
    {

    }

}

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleInspectionReminder.Data.Infrastructure;
using VehicleInspectionReminder.Model;

namespace VehicleInspectionReminder.Data.Repository
{
    public class VehicleTypeRepository : EfRepositoryBase<VehicleType>, IVehicleTypeRepository
    {
        public VehicleTypeRepository(IDataBaseFactory factory) : base(factory) { }
    }

    public interface IVehicleTypeRepository : IRepository<VehicleType>
    {

    }

}

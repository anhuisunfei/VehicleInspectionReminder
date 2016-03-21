<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleInspectionReminder.Data.Infrastructure;
using VehicleInspectionReminder.Model;

namespace VehicleInspectionReminder.Data.Repository
{
	public class BrandRepository : EfRepositoryBase<Brand>, IBrandRepository
	{
		public BrandRepository(IDataBaseFactory factory)
			: base(factory)
		{

		}
	}

	public interface IBrandRepository:IRepository<Brand>
	{

	}
}
=======
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleInspectionReminder.Data.Infrastructure;
using VehicleInspectionReminder.Model;

namespace VehicleInspectionReminder.Data.Repository
{
    public class BrandRepository : EfRepositoryBase<Brand>, IBrandRepository
    {
        public BrandRepository(IDataBaseFactory factory) : base(factory) { }
    }

   public interface IBrandRepository : IRepository<Brand>
   {

   }
}
>>>>>>> 2af02b28cae4579fdda473347569a3a03ba43f87

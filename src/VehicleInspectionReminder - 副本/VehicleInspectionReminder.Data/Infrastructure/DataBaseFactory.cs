using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleInspectionReminder.Data.Infrastructure
{
	public class DataBaseFactory : Disposable, IDataBaseFactory
	{
		private MyDbContext _context;
		public MyDbContext Get()
		{
			return _context ?? (_context = new MyDbContext());
		}

		protected override void DisposeCore()
		{
			if (_context != null)
				_context.Dispose();
		}
	}
}

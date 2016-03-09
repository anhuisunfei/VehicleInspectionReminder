using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleInspectionReminder.Data.Infrastructure
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly IDataBaseFactory _factory;
		private MyDbContext _context;
		public UnitOfWork(IDataBaseFactory databaseFactory)
		{
			this._factory = databaseFactory;
		}

		protected MyDbContext DataContext
		{
			get { return _context ?? (_context = _factory.Get()); }
		}

		public int Commit()
		{
			try
			{
				return DataContext.Commit();
			}
			catch (DbEntityValidationException dbEx)
			{
				var msg = string.Empty;

				foreach (var validationErrors in dbEx.EntityValidationErrors)
					foreach (var validationError in validationErrors.ValidationErrors)
						msg += string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;

				var fail = new Exception(msg, dbEx);
				//Debug.WriteLine(fail.Message, fail);
				throw fail;
			}
		}
	}
}

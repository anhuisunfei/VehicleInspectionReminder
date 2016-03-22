using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleInspectionReminder.Model;

namespace VehicleInspectionReminder.Data.Infrastructure
{
	public interface IDbContext
	{
		IDbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity;
		int Commit();

		IEnumerable<TElement> SqlQuery<TElement>(string sql, params object[] parameters);
		IList<TEntity> ExecuteStoredProcedureList<TEntity>(string commandText, params object[] parameters)
		  where TEntity : BaseEntity, new();
		int ExecuteSqlCommand(string sql, bool doNotEnsureTransaction = false, int? timeout = null, params object[] parameters);
	}
}

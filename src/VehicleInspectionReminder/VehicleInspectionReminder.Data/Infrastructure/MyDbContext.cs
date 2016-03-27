using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using VehicleInspectionReminder.Model;

namespace VehicleInspectionReminder.Data.Infrastructure
{
	public class MyDbContext:DbContext,IDbContext
	{
		public MyDbContext()
			: base("MyEntities")
		{
            //this.Configuration.LazyLoadingEnabled = false;
		}

		#region Utilities

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
			.Where(type => !String.IsNullOrEmpty(type.Namespace))
			.Where(type => type.BaseType != null && type.BaseType.IsGenericType && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));
			foreach (var type in typesToRegister)
			{
				dynamic configurationInstance = Activator.CreateInstance(type);
				modelBuilder.Configurations.Add(configurationInstance);
			}



            base.Configuration.LazyLoadingEnabled = false;
			base.OnModelCreating(modelBuilder);
		}

		public virtual int Commit()
		{
			return base.SaveChanges();
		}


		/// <summary>
		/// 附加实体到 DbContext
		/// </summary>
		/// <typeparam name="TEntity"></typeparam>
		/// <param name="entity"></param>
		/// <returns></returns>
		protected virtual TEntity AttachEntityToContext<TEntity>(TEntity entity) where TEntity : BaseEntity, new()
		{
			var alreadyAttached = Set<TEntity>().Local.FirstOrDefault(x => x.Id == entity.Id);
			if (alreadyAttached == null)
			{
				Set<TEntity>().Attach(entity);
				return entity;
			}
			else
			{
				return alreadyAttached;
			}
		}

		#endregion

		#region Methods




		public new IDbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
		{
			return base.Set<TEntity>();
		}

		/// <summary>
		/// 执行存储过程获取实体列表
		/// </summary>
		/// <typeparam name="TEntity"></typeparam>
		/// <param name="commandText"></param>
		/// <param name="parameters"></param>
		/// <returns></returns>
		public IList<TEntity> ExecuteStoredProcedureList<TEntity>(string commandText, params object[] parameters) where TEntity : BaseEntity, new()
		{
			if (parameters != null && parameters.Length > 0)
			{
				for (int i = 0; i <= parameters.Length - 1; i++)
				{
					var p = parameters[i] as DbParameter;
					if (p == null)
						throw new Exception("Not support parameter type");

					commandText += i == 0 ? " " : ", ";

					commandText += "@" + p.ParameterName;
					if (p.Direction == ParameterDirection.InputOutput || p.Direction == ParameterDirection.Output)
					{
						//output parameter
						commandText += " output";
					}
				}
			}

			var result = this.Database.SqlQuery<TEntity>(commandText, parameters).ToList();

			bool acd = this.Configuration.AutoDetectChangesEnabled;
			try
			{
				this.Configuration.AutoDetectChangesEnabled = false;

				for (int i = 0; i < result.Count; i++)
					result[i] = AttachEntityToContext(result[i]);
			}
			finally
			{
				this.Configuration.AutoDetectChangesEnabled = acd;
			}

			return result;
		}

		/// <summary>
		/// 执行 SQL 查询
		/// </summary>
		/// <typeparam name="TElement"></typeparam>
		/// <param name="sql"></param>
		/// <param name="parameters"></param>
		/// <returns></returns>
		public IEnumerable<TElement> SqlQuery<TElement>(string sql, params object[] parameters)
		{
			return this.Database.SqlQuery<TElement>(sql, parameters);
		}

		/// <summary>
		/// 执行 SQL
		/// </summary>
		/// <param name="sql"></param>
		/// <param name="doNotEnsureTransaction"></param>
		/// <param name="timeout"></param>
		/// <param name="parameters"></param>
		/// <returns></returns>
		public int ExecuteSqlCommand(string sql, bool doNotEnsureTransaction = false, int? timeout = null, params object[] parameters)
		{
			int? previousTimeout = null;
			if (timeout.HasValue)
			{
				previousTimeout = ((IObjectContextAdapter)this).ObjectContext.CommandTimeout;
				((IObjectContextAdapter)this).ObjectContext.CommandTimeout = timeout;
			}

			var transactionalBehavior = doNotEnsureTransaction
				? TransactionalBehavior.DoNotEnsureTransaction
				: TransactionalBehavior.EnsureTransaction;
			var result = this.Database.ExecuteSqlCommand(transactionalBehavior, sql, parameters);

			if (timeout.HasValue)
			{
				((IObjectContextAdapter)this).ObjectContext.CommandTimeout = previousTimeout;
			}

			return result;
		}

		#endregion
	}
}

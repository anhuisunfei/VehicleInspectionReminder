using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VehicleInspectionReminder.Model;

namespace VehicleInspectionReminder.Data.Infrastructure
{
	public class EfRepositoryBase<T> :IRepository<T> where T:BaseEntity
	{
		private MyDbContext _context;
		private IDbSet<T> _entities;

		public EfRepositoryBase(IDataBaseFactory factory)
		{
			if (factory == null)
				throw new ArgumentNullException("factory");
			this.DatabaseFactory = factory;
			_context = _context ?? (_context = DatabaseFactory.Get());
		}

		protected IDataBaseFactory DatabaseFactory
		{
			get;
			private set;
		}



		public virtual T GetById(object id)
		{
			return this.Entities.Find(id);
		}


		public virtual T Get(Expression<Func<T, bool>> where)
		{
			return this.Entities.Where(where).FirstOrDefault();
		}


		public virtual IEnumerable<T> GetAll()
		{
			return this.Entities.ToList();
		}

		public virtual IEnumerable<T> GetManay(Expression<Func<T, bool>> where)
		{
			return this.Entities.Where(where).ToList();
		}


		public virtual IPagedList<T> GetPage<TOrder>(Page page, Expression<Func<T, bool>> where, Expression<Func<T, TOrder>> order)
		{
			var results = this.Entities.OrderBy(order).Where(where); 
			return new PagedList<T>(results, page);
		}

		public virtual void Insert(T entity)
		{

			if (entity == null)
				throw new ArgumentNullException("entity");

			this.Entities.Add(entity);
		}

		public virtual void Update(T entity)
		{

			if (entity == null)
				throw new ArgumentNullException("entity");

			this.Entities.Attach(entity);
			this._context.Entry(entity).State = EntityState.Modified;


		}

		public virtual void Delete(T entity)
		{

			if (entity == null)
				throw new ArgumentNullException("entity");

			this.Entities.Remove(entity);

		}

		public virtual IQueryable<T> Table
		{
			get
			{
				return this.Entities;
			}
		}

		protected virtual IDbSet<T> Entities
		{
			get
			{
				if (_entities == null)
					_entities = _context.Set<T>();
				return _entities;
			}
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VehicleInspectionReminder.Model;

namespace VehicleInspectionReminder.Data.Infrastructure
{
	public interface IRepository<T> where T : BaseEntity
	{
		/// <summary>
		/// 根据 ID 获取对象
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		T GetById(object id);

		/// <summary>
		/// 根据 Lambda 条件获取单条数据
		/// </summary>
		/// <param name="where"></param>
		/// <returns></returns>
		T Get(Expression<Func<T, bool>> where);

		/// <summary>
		/// 获取所有数据
		/// </summary>
		/// <returns></returns>
		IEnumerable<T> GetAll();

		/// <summary>
		/// 根据条件获取多条数据
		/// </summary>
		/// <param name="where"></param>
		/// <returns></returns>
		IEnumerable<T> GetManay(Expression<Func<T, bool>> where);

		/// <summary>
		/// 分页获取数据
		/// </summary>
		/// <typeparam name="TOrder"></typeparam>
		/// <param name="page"></param>
		/// <param name="where"></param>
		/// <param name="order"></param>
		/// <returns></returns>
		IPagedList<T> GetPage<TOrder>(Page page, Expression<Func<T, bool>> where, Expression<Func<T, TOrder>> order);

		/// <summary>
		/// 插入
		/// </summary>
		/// <param name="entity"></param>
		void Insert(T entity);

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="entity"></param>
		void Update(T entity);

		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="entity"></param>
		void Delete(T entity);

		/// <summary>
		/// 返回 IQueryable 
		/// </summary>
		IQueryable<T> Table { get; }

	}
}

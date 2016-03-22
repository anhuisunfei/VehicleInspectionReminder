using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleInspectionReminder.Data.Infrastructure
{
	public class PagedList<T> : IPagedList<T> where T : class
	{
		public PagedList(IQueryable<T> source, Page page)
		{
			int total = source.Count();
			this.records = total;
			this.total = (total / page.PageSize) + ((total % page.PageSize) > 0 ? 1 : 0);
			this.page = page.PageNumber;
			this.rows = source.Skip(page.Skip).Take(page.PageSize).ToList();
		}

		 

		public int page
		{
			get;
			private set;
		}
		public long records
		{
			get;
			private set;
		}
		public int total
		{
			get;
			private set;
		}


		public IEnumerable<T> rows
		{
			get;
			private set;
		}
	}

	public class Page
	{
		public int PageNumber { get; set; }
		public int PageSize { get; set; }

		public Page()
		{
			PageNumber = 1;
			PageSize = 10;
		}

		public Page(int pageNumber, int pageSize)
		{
			PageNumber = pageNumber;
			PageSize = pageSize;
		}

		public int Skip
		{
			get { return (PageNumber - 1) * PageSize; }
		}
	}
}

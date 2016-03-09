using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleInspectionReminder.Data.Infrastructure
{
	public interface IPagedList<T>
	{
		int page { get; }
		long records { get; }
		int total { get; }
		IEnumerable<T> rows { get; }
	}
}

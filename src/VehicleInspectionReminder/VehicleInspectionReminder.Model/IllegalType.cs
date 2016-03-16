using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleInspectionReminder.Model
{
	/// <summary>
	/// 违章类型
	/// </summary>
	public  class IllegalType:BaseEntity
	{
		public string IllegalTypeName { get; set; }
	}
}

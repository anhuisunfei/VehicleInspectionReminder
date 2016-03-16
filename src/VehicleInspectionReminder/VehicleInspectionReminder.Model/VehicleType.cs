using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleInspectionReminder.Model
{
	/// <summary>
	/// 车辆类型
	/// </summary>
	public class VehicleType:BaseEntity
	{
		public string VehicleTypeName { get; set; }
	}
}

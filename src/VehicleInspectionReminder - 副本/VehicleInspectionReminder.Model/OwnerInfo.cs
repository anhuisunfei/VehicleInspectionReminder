using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleInspectionReminder.Model
{
	/// <summary>
	/// 车辆信息
	/// </summary>
	public class OwnerInfo : BaseEntity
	{
		/// <summary>
		/// Identity 对应用户ID
		/// </summary>
		public Guid AspNetUserId { get; set; }
		/// <summary>
		/// 驾驶证编号
		/// </summary>
		public string LicenseNum { get; set; }

		/// <summary>
		/// 姓名
		/// </summary>
		public string UserName { get; set; }

		/// <summary>
		/// 出生日期
		/// </summary>
		public DateTime? BirthDay { get; set; }

		/// <summary>
		/// 邮箱
		/// </summary>
		public string Email { get; set; }


		/// <summary>
		/// 性别
		/// </summary>
		public Gender Gender { get; set; }

		/// <summary>
		/// 车辆信息
		/// </summary>
		public virtual ICollection<VehicleInfo> VehicleInfos { get; set; }
	}
}

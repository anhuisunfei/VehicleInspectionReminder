using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleInspectionReminder.Model
{
	/// <summary>
	/// 通知消息
	/// </summary>
	public class NotificationMessage:BaseEntity
	{
		/// <summary>
		/// 主题
		/// </summary>
		public string Subject { get; set; }
		/// <summary>
		/// 消息主体
		/// </summary>
		public string Body { get; set; }

		/// <summary>
		/// 是否发送
		/// </summary>
		public bool IsSending { get; set; }

		/// <summary>
		/// 发送时间
		/// </summary>
		public DateTime? SendingTime { get; set; }
 

		public int VehicleInfoId { get; set; }

        public string Email { get; set; }

		/// <summary>
		/// 车辆信息
		/// </summary>
		public virtual VehicleInfo VehicleInfo { get; set; }

		/// <summary>
		/// 车检时间
		/// </summary>
		public DateTime CheckTime { get; set; }
	}
}

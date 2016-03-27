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
	public class VehicleInfo:BaseEntity
	{
		/// <summary>
		/// 车辆品牌
		/// </summary>
		public int BrandId { get; set; }

		public virtual  Brand VehicleBrand { get; set; }

		/// <summary>
		/// 车辆类型
		/// </summary>
		public int VehicleTypeId { get; set; }
		public virtual  VehicleType VehicleType { get; set; }

		/// <summary>
		/// 车牌
		/// </summary>
		public string Plate { get; set; }

		/// <summary>
		/// 出厂时间
		/// </summary>
		public DateTime DeliveryTtime { get; set; }

		/// <summary>
		/// 购买日期
		/// </summary>
		public DateTime PurchaseDate { get; set; }

		/// <summary>
		/// 投保状态
		/// </summary>
		public bool InsuranceStatus { get; set; }

		/// <summary>
		/// 车内消防设施情况
		/// </summary>
		public FireCarType FireCar { get; set; }

		/// <summary>
		/// 照明设施情况
		/// </summary>
		public LightConditionType LightCondition { get; set; }

		/// <summary>
		/// 车牌是否完好
		/// </summary>
		public PlateType PlateIsIntact { get; set; }

		/// <summary>
		/// 最后一次车检时间
		/// </summary>
		public DateTime? LastInspectionTime { get; set; }

        /// <summary>
        /// 下一次车检日期
        /// </summary>
        public DateTime? NextInspectionTime { get; set; }

		/// <summary>
		/// 车主信息
		/// </summary>
		public int OwnerId { get; set; }

		public virtual OwnerInfo Owner { get; set; }
		 
	}
}

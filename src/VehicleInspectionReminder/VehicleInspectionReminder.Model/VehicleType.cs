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
        /// <summary>
        /// 类型名称
        /// </summary>
		public string VehicleTypeName { get; set; }

        /// <summary>
        /// 第一规定检验期， 单位： 年
        /// </summary>
        public int FirstJY_Year { get; set; }

        /// <summary>
        /// 第一规定检验期内的检验周期， 单位：月
        /// </summary>
        public int FirstJY_Cycle { get; set; }

        /// <summary>
        /// 第二规定检验期， 单位： 年 
        /// </summary>
        public int SecondJY_Year { get; set; }

        /// <summary>
        /// 第二规定检验期内的检验周期， 单位：月
        /// </summary>
        public int SecondJY_Cycle { get; set; }

        /// <summary>
        /// 第三规定检验期， 单位： 年
        /// </summary>
        public int LastJY_Year { get; set; }

        /// <summary>
        /// 第三规定检验期内的检验周期， 单位：月
        /// </summary>
        public int LastJY_Cycle { get; set; }

        

	}
}

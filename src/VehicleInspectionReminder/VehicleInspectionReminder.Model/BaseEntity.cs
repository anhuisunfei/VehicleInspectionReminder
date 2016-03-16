using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleInspectionReminder.Model
{
	public class BaseEntity
	{
		public int Id { get; set; }
		public RecordStatus Status { get; set; }

		public override bool Equals(object obj)
		{
			return Equals(obj as BaseEntity);
		}

		private static bool IsTransient(BaseEntity obj)
		{
			return obj != null && Equals(obj.Id, default(int));
		}

		private Type GetUnproxiedType()
		{
			return GetType();
		}

		public virtual bool Equals(BaseEntity other)
		{
			if (other == null)
				return false;

			if (ReferenceEquals(this, other))
				return true;

			if (!IsTransient(this) &&
				!IsTransient(other) &&
				Equals(Id, other.Id))
			{
				var otherType = other.GetUnproxiedType();
				var thisType = GetUnproxiedType();
				return thisType.IsAssignableFrom(otherType) ||
						otherType.IsAssignableFrom(thisType);
			}

			return false;
		}

		public override int GetHashCode()
		{
			if (Equals(Id, default(int)))
				return base.GetHashCode();
			return Id.GetHashCode();
		}

		public static bool operator ==(BaseEntity x, BaseEntity y)
		{
			return Equals(x, y);
		}

		public static bool operator !=(BaseEntity x, BaseEntity y)
		{
			return !(x == y);
		}
	}

	/// <summary>
	/// 记录状态
	/// </summary>
	public enum RecordStatus
	{
		Acvitiy = 0,
		Disabled = 1
	}

	/// <summary>
	/// 车内消防状况
	/// </summary>
	public enum FireCarType
	{
		/// <summary>
		/// 良好
		/// </summary>
		[Description("良好")]
		Good=1,
		/// <summary>
		/// 一般
		/// </summary>
		[Description("一般")]
		General=2,
		/// <summary>
		/// 损毁
		/// </summary>
		[Description("损毁")]
		Damage=3
		 
	}

	/// <summary>
	/// 照明设施情况
	/// </summary>
	public enum LightConditionType
	{
		/// <summary>
		/// 良好
		/// </summary>
		[Description("良好")]
		Good = 1,
		/// <summary>
		/// 一般
		/// </summary>
		[Description("一般")]
		General = 2,
		/// <summary>
		/// 损毁
		/// </summary>
		[Description("损毁")]
		Damage = 3
	}

	/// <summary>
	/// 车牌是否完好
	/// </summary>
	public enum PlateType
	{
		/// <summary>
		/// 完好
		/// </summary>
		[Description("完好")]
		Intact = 1,  
		/// <summary>
		/// 损毁
		/// </summary>
		[Description("损毁")]
		Damage = 2
	}


	public enum Gender
	{
		/// <summary>
		/// 男
		/// </summary>
		Male=0,
		/// <summary>
		/// 女
		/// </summary>
		Female=1
	}
}

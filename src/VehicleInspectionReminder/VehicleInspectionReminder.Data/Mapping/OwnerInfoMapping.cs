using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleInspectionReminder.Model;

namespace VehicleInspectionReminder.Data.Mapping
{
	public class OwnerInfoMapping:EntityTypeConfiguration<OwnerInfo>
	{
		public OwnerInfoMapping()
		{
			this.HasKey(p => p.Id);
			Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None); // 此处主键配置为空 我想象中的场景为 用户注册完成之后完善个人信息  此处ID取AspNetUsers中的ID
			HasMany(p => p.VehicleInfos).WithRequired(v => v.Owner).HasForeignKey(v=>v.OwnerId);


		}
	}
}

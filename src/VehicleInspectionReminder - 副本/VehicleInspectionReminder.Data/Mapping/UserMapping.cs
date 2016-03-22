using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using VehicleInspectionReminder.Model;

namespace VehicleInspectionReminder.Data.Mapping
{
	public class UserMapping:EntityTypeConfiguration<User>
	{
		public UserMapping()
		{
			this.ToTable("T_User");
			this.HasKey(t => t.Id);
			this.Property(p => p.UserName).HasMaxLength(200).IsRequired();
			this.Property(p => p.RealName).HasMaxLength(200).IsRequired();
			this.Property(p => p.Email).IsRequired();
		}
	}
}

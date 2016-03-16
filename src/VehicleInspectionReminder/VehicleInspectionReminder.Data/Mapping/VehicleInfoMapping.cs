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
	public class VehicleInfoMapping : EntityTypeConfiguration<VehicleInfo>
	{
		public VehicleInfoMapping()
		{
			this.HasKey(p => p.Id);
			Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
			// HasRequired(p => p.Owner).WithMany().HasForeignKey(p => p.Id);
		}
	}
}

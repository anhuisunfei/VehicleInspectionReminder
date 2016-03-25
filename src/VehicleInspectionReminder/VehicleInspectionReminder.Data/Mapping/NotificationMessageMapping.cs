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
	public class NotificationMessageMapping : EntityTypeConfiguration<NotificationMessage>
	{

		public NotificationMessageMapping()
		{
			this.HasKey(p => p.Id);
			this.Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity); 
		}
	}
}

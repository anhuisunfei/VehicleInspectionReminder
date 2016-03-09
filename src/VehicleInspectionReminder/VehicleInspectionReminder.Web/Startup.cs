using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VehicleInspectionReminder.Web.Startup))]
namespace VehicleInspectionReminder.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

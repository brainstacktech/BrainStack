using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProcurementManagementSystem.Startup))]
namespace ProcurementManagementSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

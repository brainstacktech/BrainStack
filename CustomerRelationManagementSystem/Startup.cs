using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CustomerRelationManagementSystem.Startup))]
namespace CustomerRelationManagementSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

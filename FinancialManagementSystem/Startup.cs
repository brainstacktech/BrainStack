using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FinancialManagementSystem.Startup))]
namespace FinancialManagementSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

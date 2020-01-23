using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ApparaisalSystem.Startup))]
namespace ApparaisalSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

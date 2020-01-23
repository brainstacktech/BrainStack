using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BrainstStackCollectionPortal.Startup))]
namespace BrainstStackCollectionPortal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

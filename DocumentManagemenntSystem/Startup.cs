using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DocumentManagemenntSystem.Startup))]
namespace DocumentManagemenntSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

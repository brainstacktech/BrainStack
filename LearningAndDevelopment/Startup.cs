using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LearningAndDevelopment.Startup))]
namespace LearningAndDevelopment
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

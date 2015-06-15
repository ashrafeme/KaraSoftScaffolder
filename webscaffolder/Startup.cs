using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(webscaffolder.Startup))]
namespace webscaffolder
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MillennialResortWebSite.Startup))]
namespace MillennialResortWebSite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

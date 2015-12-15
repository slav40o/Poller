using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Poller.Web.Startup))]
namespace Poller.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

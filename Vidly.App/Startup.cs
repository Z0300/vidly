using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Vidly.App.Startup))]
namespace Vidly.App
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

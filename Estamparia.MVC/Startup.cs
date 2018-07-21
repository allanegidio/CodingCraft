using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Estamparia.MVC.Startup))]
namespace Estamparia.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

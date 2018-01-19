using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Lojinha.MVC.Startup))]
namespace Lojinha.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

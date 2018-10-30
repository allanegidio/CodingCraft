using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Editora.Intranet.Startup))]
namespace Editora.Intranet
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

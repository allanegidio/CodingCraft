using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Editora.Extranet.Startup))]
namespace Editora.Extranet
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

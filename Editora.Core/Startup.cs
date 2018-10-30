using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Editora.Core.Startup))]
namespace Editora.Core
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SistemaFloricultura.Startup))]
namespace SistemaFloricultura
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

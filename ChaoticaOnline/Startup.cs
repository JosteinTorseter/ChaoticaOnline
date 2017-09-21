using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ChaoticaOnline.Startup))]
namespace ChaoticaOnline
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

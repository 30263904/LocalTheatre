using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LocalTheatre.Startup))]
namespace LocalTheatre
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
         
        }
    }
}

using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ParkingSys.Startup))]
namespace ParkingSys
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(projektMVC.Startup))]
namespace projektMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

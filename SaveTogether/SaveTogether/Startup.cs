using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SaveTogether.Startup))]
namespace SaveTogether
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

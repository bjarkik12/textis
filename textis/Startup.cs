using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(textis.Startup))]
namespace textis
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

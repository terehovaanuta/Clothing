using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Clothing_v2._2.Startup))]
namespace Clothing_v2._2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

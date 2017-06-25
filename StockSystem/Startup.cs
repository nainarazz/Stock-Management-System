using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StockSystem.Startup))]
namespace StockSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

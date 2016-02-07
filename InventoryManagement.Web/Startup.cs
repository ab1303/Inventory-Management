using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(InventoryManagement.Web.Startup))]
namespace InventoryManagement.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

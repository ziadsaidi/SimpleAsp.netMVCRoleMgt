using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RoleManagementApp.Startup))]
namespace RoleManagementApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

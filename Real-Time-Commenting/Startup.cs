using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Real_Time_Commenting.Startup))]
namespace Real_Time_Commenting
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

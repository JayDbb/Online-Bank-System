using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Online_Bank_System.Startup))]
namespace Online_Bank_System
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
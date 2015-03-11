using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TimetablingSys_T17.Startup))]
namespace TimetablingSys_T17
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

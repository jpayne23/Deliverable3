using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TimetableSys_T17.Startup))]
namespace TimetableSys_T17
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CorporateNAVReporting.Startup))]
namespace CorporateNAVReporting
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}

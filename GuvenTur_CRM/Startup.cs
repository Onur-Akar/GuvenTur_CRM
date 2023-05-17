using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GuvenTur_CRM.Startup))]
namespace GuvenTur_CRM
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

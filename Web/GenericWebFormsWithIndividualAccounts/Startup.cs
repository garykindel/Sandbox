using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GenericWebFormsWithIndividualAccounts.Startup))]
namespace GenericWebFormsWithIndividualAccounts
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}

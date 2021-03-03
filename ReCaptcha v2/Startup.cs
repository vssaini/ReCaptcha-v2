using Microsoft.Owin;
using Owin;
using ReCaptchaV2;

[assembly: OwinStartup(typeof(Startup))]
namespace ReCaptchaV2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

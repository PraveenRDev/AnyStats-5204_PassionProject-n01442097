using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AnyStats___5204_PassionProject_n01442097.Startup))]
namespace AnyStats___5204_PassionProject_n01442097
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

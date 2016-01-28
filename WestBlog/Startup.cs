using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WestBlog.Startup))]
namespace WestBlog
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DocStore.Startup))]
namespace DocStore
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

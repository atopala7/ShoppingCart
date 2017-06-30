using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AndreiTopala_ShoppingCart.Startup))]
namespace AndreiTopala_ShoppingCart
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

using Owin;
using System;
using System.Threading.Tasks;

namespace JavaJanitor
{
    public partial class Startup
    {
        public void ConfigureSignalR(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}
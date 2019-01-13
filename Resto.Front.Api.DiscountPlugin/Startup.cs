using System;
using System.Web.Http;
using System.Web.Http.SelfHost;

namespace Resto.Front.Api.DiscountPlugin
{
    public class Startup
    {
        private HttpSelfHostServer server = null;
        private HttpSelfHostConfiguration config = null;
        public Startup()
        {
            config = new HttpSelfHostConfiguration("http://localhost:9000");

            config.Routes.MapHttpRoute(
                "API Default", "api/{controller}/{id}",
                new { id = RouteParameter.Optional });

            server = new HttpSelfHostServer(config);
            //using (HttpSelfHostServer server = new HttpSelfHostServer(config))
            {
                try
                {
                    server.OpenAsync().Wait();
                }
                catch (Exception e)
                {
                    ;
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Web.Http;
using System.Web.Http.SelfHost;

namespace SelfHostExample
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using Resto.Front.Api.V6;
using Resto.Front.Api.V6.Attributes;
using Resto.Front.Api.V6.Attributes.JetBrains;

#if true
// NuGet: install Microsoft.AspNet.WebApi.SelfHost
//using System.Web.Http;
//using System.Web.Http.SelfHost;
#else
// NuGet: install Microsoft.AspNet.WebApi.OwinSelfHost
using Microsoft.Owin.Hosting;
using System.Net.Http;
#endif

namespace Resto.Front.Api.DiscountPlugin
{
    /// <summary>
    /// Тестовый плагин для демонстрации возможностей Api.
    /// Автоматически не публикуется, для использования скопировать Resto.Front.Api.DiscountPlugin.dll в Resto.Front.Main\bin\Debug\Plugins\Resto.Front.Api.DiscountPlugin\
    /// </summary>
    [UsedImplicitly]
    [PluginLicenseModuleId(21005108)]
    public sealed class PluginMain : IFrontPlugin
    {
        private readonly Stack<IDisposable> subscriptions = new Stack<IDisposable>();

#if true
        //private string baseAddress = "http://localhost:8080";
        //private HttpSelfHostServer server = null;
        //private HttpSelfHostConfiguration config = null;
        Startup startup = null;
#else
        private string baseAddress = "http://*:56988/";
#endif

        public PluginMain()
        {
            PluginContext.Log.Info("Initializing PluginMain");

#if true
            startup = new Startup();
#else
            // Start OWIN host
            WebApp.Start<Startup>(url: baseAddress);
#endif
            PluginContext.Operations.AddNotificationMessage("Discount plugin started", "DiscountPlugin", TimeSpan.FromSeconds(5));

            //if (Settings.Default.ExtendBillCheque)
                //subscriptions.Push(new BillChequeExtender());

            //subscriptions.Push(new ButtonsTester());

            //subscriptions.Push(new EditorTester());

            PluginContext.Log.Info("PluginMain started");
        }

        public void Dispose()
        {
            while (subscriptions.Any())
            {
                var subscription = subscriptions.Pop();
                try
                {
                    subscription.Dispose();
                }
                catch (RemotingException)
                {
                    // nothing to do with the lost connection
                }
            }

            PluginContext.Log.Info("PluginMain stopped");
        }
    }
}

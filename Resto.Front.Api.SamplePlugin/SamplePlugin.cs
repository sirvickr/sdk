using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using Resto.Front.Api.SamplePlugin.Properties;
using Resto.Front.Api.V6;
using Resto.Front.Api.V6.Attributes;
using Resto.Front.Api.V6.Attributes.JetBrains;

//using Resto.Front.Api.SamplePlugin.Controllers;
using Microsoft.Owin.Hosting;
using System.Net.Http;

// NuGet: install Microsoft.AspNet.WebApi.OwinSelfHost

namespace Resto.Front.Api.SamplePlugin
{
    /// <summary>
    /// Тестовый плагин для демонстрации возможностей Api.
    /// Автоматически не публикуется, для использования скопировать Resto.Front.Api.SamplePlugin.dll в Resto.Front.Main\bin\Debug\Plugins\Resto.Front.Api.SamplePlugin\
    /// </summary>
    [UsedImplicitly]
    [PluginLicenseModuleId(21005108)]
    public sealed class SamplePlugin : IFrontPlugin
    {
        private readonly Stack<IDisposable> subscriptions = new Stack<IDisposable>();

        //private string baseAddress = "http://localhost:9000/";//56988
        private string baseAddress = "http://192.168.42.150:9000/";//56988

        public SamplePlugin()
        {
            PluginContext.Log.Info("Initializing SamplePlugin");

#if true
            // Start OWIN host
            WebApp.Start<Startup>(url: baseAddress);
#else
            string baseAddress = "http://localhost:9000/";//56988
            // Start OWIN host
            using (WebApp.Start<Startup>(url: baseAddress))
            {
                // Create HttpClient and make a request to api/values
                HttpClient client = new HttpClient();
                var response = client.GetAsync(baseAddress + "api/values").Result;
                //PluginContext.Log.Info(response);
                PluginContext.Log.Info(response.Content.ReadAsStringAsync().Result);
                /*Console.WriteLine(response);
                Console.WriteLine(response.Content.ReadAsStringAsync().Result);
                Console.WriteLine("Press Enter to quit.");
                Console.ReadLine();*/
            }
#endif
            PluginContext.Operations.AddNotificationMessage("Plugin started", "SamplePlugin", TimeSpan.FromSeconds(3));

            if (Settings.Default.ExtendBillCheque)
                subscriptions.Push(new BillChequeExtender());

            subscriptions.Push(new ButtonsTester());

            subscriptions.Push(new EditorTester());
            //subscriptions.Push(new DiagnosticMessagesTester.MessagesTester());
            //subscriptions.Push(new Restaurant.RestaurantViewer());
            //subscriptions.Push(new Restaurant.MenuViewer());
            //subscriptions.Push(new DiagnosticMessagesTester.StopListChangeNotifier());
            //subscriptions.Push(new DiagnosticMessagesTester.FrontLockReminder());
            //subscriptions.Push(new Restaurant.StreetViewer());
            //subscriptions.Push(new Restaurant.ReservesViewer());
            //subscriptions.Push(new Restaurant.ClientViewer());
            //subscriptions.Push(new DiagnosticMessagesTester.OrderItemReadyChangeNotifier());
            //subscriptions.Push(new LicensingTester());
            //subscriptions.Push(new Kitchen.KitchenLoadMonitoringViewer());
            //subscriptions.Push(new Restaurant.BanquetAndReserveTester());
            //subscriptions.Push(new PreliminaryOrders.OrdersEditor());
            //subscriptions.Push(new Restaurant.SchemaViewer());
            // добавляйте сюда другие подписчики

            PluginContext.Log.Info("SamplePlugin started");
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

            PluginContext.Log.Info("SamplePlugin stopped");
        }
    }
}
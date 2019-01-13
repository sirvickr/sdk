using System;
using System.Collections.Generic;
using System.Linq;

using Resto.Front.Api.V6;
using Resto.Front.Api.V6.Data.Organization.Sections;
using Resto.Front.Api.V6.Data.Orders;

namespace Resto.Front.Api.SamplePlugin.Models
{
    public class OrdersRepos
    {
        public IEnumerable<Order> GetAll()
        {
            List<Order> result = new List<Order>();

            IReadOnlyList<IOrder> orders = PluginContext.Operations.GetOrders();

            foreach (IOrder order in orders)
            {
                List<TableEntry> orderTables = new List<TableEntry>();
                foreach (ITable table in order.Tables)
                {
                    orderTables.Add( new TableEntry { Id = table.Id.ToString(), Number = table.Number } );
                }
                result.Add( new Order{ Id = order.Id.ToString(), FullSum = order.FullSum, Tables = orderTables.ToArray<TableEntry>() } );
            }
            //PluginContext.Operations.AddNotificationMessage("GET orders", "SamplePlugin", TimeSpan.FromSeconds(5));

            return result;
        }
        /*public Table Get(int id)
        {
            return tables.Find(t => t.Id == id);
        }*/
    }
}

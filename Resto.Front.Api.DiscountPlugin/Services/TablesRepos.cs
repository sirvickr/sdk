using System;
using System.Collections.Generic;
using System.Linq;

using Resto.Front.Api.V6;
using Resto.Front.Api.V6.Data.Organization.Sections;
using Resto.Front.Api.V6.Data.Orders;

using Resto.Front.Api.DiscountPlugin.Models;

namespace Resto.Front.Api.DiscountPlugin.Services
{
    public class TablesRepos
    {
        public IEnumerable<Table> GetAll()
        {
            List<Table> result = new List<Table>();

            IReadOnlyList<IOrder> orders = PluginContext.Operations.GetOrders();
            IReadOnlyList<ITable> tables = PluginContext.Operations.GetTables();

            foreach (ITable table in tables)
            {
                decimal fullSum = 0;
                List<OrderEntry> tableOrders = new List<OrderEntry>();
                foreach (IOrder order in orders)
                {
                    //IReadOnlyList<ITable> orderTables = order.Tables;
                    foreach (ITable orderTable in order.Tables) // orderTables
                    {
                        if (orderTable.Id == table.Id)
                        {
                            fullSum += order.FullSum;
                            tableOrders.Add(new OrderEntry { Id = order.Id.ToString(), FullSum = order.FullSum });
                        }
                    }
                }
                result.Add(new Table { Id = table.Id.ToString(), Number = table.Number, FullSum = fullSum, Orders = tableOrders.ToArray<OrderEntry>() });
            }
            //PluginContext.Operations.AddNotificationMessage("GET tables", "SamplePlugin", TimeSpan.FromSeconds(5));

            return result;
        }
        /*public Table Get(int id)
        {
            return tables.Find(t => t.Id == id);
        }*/
    }
}

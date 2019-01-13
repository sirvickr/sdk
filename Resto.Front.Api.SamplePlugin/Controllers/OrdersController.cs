using System.Collections.Generic;
using System.Web.Http;
using Resto.Front.Api.SamplePlugin.Models;

namespace Resto.Front.Api.SamplePlugin
{
    public class OrdersController : ApiController
    {
        private OrdersRepos orders = new OrdersRepos();
        // GET api/orders
        public IEnumerable<Order> Get()
        {
            return orders.GetAll();
        }
        // GET api/orders/1
        // TODO ?
    }
}

using System.Collections.Generic;
using System.Web.Http;
using Resto.Front.Api.DiscountPlugin.Models;
using Resto.Front.Api.DiscountPlugin.Services;

namespace Resto.Front.Api.DiscountPlugin.Controllers
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

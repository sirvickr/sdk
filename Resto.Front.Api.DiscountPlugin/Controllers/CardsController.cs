using System.Collections.Generic;
using System.Web.Http;
using Resto.Front.Api.DiscountPlugin.Models;
using Resto.Front.Api.DiscountPlugin.Services;

namespace Resto.Front.Api.DiscountPlugin.Controllers
{
    public class CardsController : ApiController
    {
        private CardsRepos cards = new CardsRepos();
        // GET api/tables
        public IEnumerable<DiscountCard> Get()
        {
            return cards.GetAll();
        }
        // GET api/tables/1
        // TODO ?
    }
}

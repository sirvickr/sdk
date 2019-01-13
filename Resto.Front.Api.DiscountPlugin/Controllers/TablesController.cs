using System.Collections.Generic;
using System.Web.Http;
using Resto.Front.Api.DiscountPlugin.Models;
using Resto.Front.Api.DiscountPlugin.Services;

namespace Resto.Front.Api.DiscountPlugin.Controllers
{
    public class TablesController : ApiController
    {
        private TablesRepos tables = new TablesRepos();
        // GET api/tables
        public IEnumerable<Table> Get()
        {
            return tables.GetAll();
        }
        // GET api/tables/1
        // TODO ?
    }
}

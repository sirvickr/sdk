using System.Collections.Generic;
using System.Web.Http;
using Resto.Front.Api.SamplePlugin.Models;

namespace Resto.Front.Api.SamplePlugin
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

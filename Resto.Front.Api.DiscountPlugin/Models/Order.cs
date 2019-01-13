using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resto.Front.Api.DiscountPlugin.Models
{
    public class Order
    {
        // Order id
        public string Id { get; set; }
        // Total sum
        public decimal FullSum { get; set; }
        // Tables related to this order (if any; otherwise, empty array)
        public TableEntry[] Tables { get; set; }
    }
    // Order entry for the orders list on particular table
    public class OrderEntry
    {
        // Order id
        public string Id { get; set; }
        // Total sum
        public decimal FullSum { get; set; }
    }
}

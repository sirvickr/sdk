using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resto.Front.Api.SamplePlugin.Models
{
    public class Table
    {
        // Table id
        public string Id { get; set; }
        // Table number
        public int Number { get; set; }
        // Tolal order sum
        public decimal FullSum { get; set; }
        // Corresponding orders list (if any; otherwise, empty array)
        public OrderEntry[] Orders { get; set; }
    }
    // Table entry for the tables list in particular order
    public class TableEntry
    {
        // Table id
        public string Id { get; set; }
        // Table number
        public int Number { get; set; }
    }
}

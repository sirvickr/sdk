using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resto.Front.Api.DiscountPlugin.Models
{
    public class DiscountCard
    {
        // Card id
        public string Id { get; set; }
        // Card number
        public string Number { get; set; }
        // Discount type
        //public DiscountType Properties { get; set; }
        // Can discount be applied to order by card number
        public bool CanApplyByCardNumber { get; set; }
        // Can discount be applied to order manually
        public bool CanApplyManually { get; set; }
        // Is this item deleted
        public bool Deleted { get; set; }
        // Is this item active for current group. This property isn't associated with Deleted. I.e. discount type can be deleted and active at the same time.
        public bool Active { get; set; }
        // Name of discount type
        public string Name { get; set; }
    }
}

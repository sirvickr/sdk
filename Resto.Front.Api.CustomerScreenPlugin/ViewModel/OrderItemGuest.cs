using Resto.Front.Api.V5.Data.Orders;

namespace Resto.Front.Api.CustomerScreen.ViewModel
{
    internal sealed class OrderItemGuest : OrderItem
    {
        public OrderItemGuest(IOrderGuestItem source)
            : base(source, source.Name)
        {
        }
    }
}

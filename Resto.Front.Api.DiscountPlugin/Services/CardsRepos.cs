using System;
using System.Collections.Generic;
using System.Linq;

using Resto.Front.Api.V6;
using Resto.Front.Api.V6.Data.Orders;

using Resto.Front.Api.DiscountPlugin.Models;

namespace Resto.Front.Api.DiscountPlugin.Services
{
    public class CardsRepos
    {
        public IEnumerable<DiscountCard> GetAll()
        {
            List<DiscountCard> result = new List<DiscountCard>();

            IReadOnlyList<IDiscountCard> cards = PluginContext.Operations.GetDiscountCards();

            foreach (IDiscountCard card in cards)
            {
                result.Add(new DiscountCard {
                    Id = card.Id.ToString(),
                    Number = card.CardNumber,
                    //Type = card.DiscountType.Name
                    //Properties = new DiscountType {
                    Active = card.DiscountType.IsActive,
                    Deleted = card.DiscountType.Deleted,
                    CanApplyByCardNumber = card.DiscountType.CanApplyByCardNumber,
                    CanApplyManually = card.DiscountType.CanApplyManually,
                    Name = card.DiscountType.Name
                    //}
                });
            }

            return result;
        }
        /*public DiscountCard Get(int id)
        {
            return tables.Find(t => t.Id == id);
        }*/
    }
}

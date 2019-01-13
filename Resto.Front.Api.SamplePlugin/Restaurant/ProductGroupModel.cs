﻿using System;
using Resto.Front.Api.V6.Attributes.JetBrains;
using Resto.Front.Api.V6.Data.Assortment;

namespace Resto.Front.Api.SamplePlugin.Restaurant
{
    internal sealed class ProductGroupModel : MenuItemModel
    {
        public IProductGroup ProductGroup { get; private set; }

        public ProductGroupModel([NotNull] IProductGroup productGroup)
        {
            if (productGroup == null)
                throw new ArgumentNullException("productGroup");

            ProductGroup = productGroup;
        }
    }
}

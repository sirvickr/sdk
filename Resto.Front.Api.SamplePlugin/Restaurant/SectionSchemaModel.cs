using System;
using System.Collections.ObjectModel;
using System.Linq;
using Resto.Front.Api.V6.Attributes.JetBrains;
using Resto.Front.Api.V6.Data.Organization.Sections;
using Resto.Front.Api.V6.Data.View;

namespace Resto.Front.Api.SamplePlugin.Restaurant
{
    public class SectionSchemaModel
    {
        public SectionSchemaModel([NotNull] ISectionSchema sectionSchema)
        {
            if (sectionSchema == null)
                throw new ArgumentNullException("sectionSchema");

            var items = sectionSchema.ImageElements
                .Cast<RestaurantSectionObject>()
                .Concat(sectionSchema.MarkElements)
                .Concat(sectionSchema.TableElements)
                .Concat(sectionSchema.RectangleElements)
                .Concat(sectionSchema.EllipseElements);
            SectionItems = new ObservableCollection<RestaurantSectionObject>(items);

            Height = sectionSchema.Height;
            Width = sectionSchema.Width;
        }

        public int Height { get; set; }
        public int Width { get; set; }
        public ObservableCollection<RestaurantSectionObject> SectionItems { get; set; }
    }
}
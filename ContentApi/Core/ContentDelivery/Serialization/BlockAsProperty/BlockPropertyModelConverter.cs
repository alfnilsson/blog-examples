using System.Collections.Generic;
using System.Globalization;
using EPiServer.ContentApi.Core.Serialization;
using EPiServer.ContentApi.Core.Serialization.Models;
using EPiServer.Core;
using EPiServer.ServiceLocation;
using EPiServer.SpecializedProperties;

namespace Toders.ContentApi.Core.ContentDelivery.Serialization.BlockAsProperty
{
    /// <summary>
    /// A PropertyModelConverter that maps properties of type PropertyBlock to BlockPropertyModel
    /// </summary>
    // Add this PropertyModelConverter together with all other PropertyModelConverters. Episerver will iterate through all by descending SortOrder
    [ServiceConfiguration(typeof(IPropertyModelConverter), Lifecycle = ServiceInstanceScope.Singleton)]
    public class BlockPropertyModelConverter : IPropertyModelConverter
    {
        public bool HasPropertyModelAssociatedWith(PropertyData propertyData)
        {
            return propertyData is PropertyBlock;
        }

        public IPropertyModel ConvertToPropertyModel(
            PropertyData propertyData,
            CultureInfo language,
            bool excludePersonalizedContent,
            bool expand)
        {
            var propertyBlock = propertyData as PropertyBlock;
            if (propertyBlock == null)
            {
                return null;
            }

            return new BlockPropertyModel(propertyBlock);
        }

        /// <summary>
        /// The DefaultPropertyModelConverter have SortOrder 0, having a lower SortOrder will place this after the default
        /// </summary>
        public int SortOrder { get; } = -1;

        /// <summary>
        /// This is not used by any other place in Episerver (as of Episerver.ContentDelivery.Cms 2.4.0)
        /// </summary>
        public IEnumerable<TypeModel> ModelTypes { get; } = new TypeModel[0];
    }
}

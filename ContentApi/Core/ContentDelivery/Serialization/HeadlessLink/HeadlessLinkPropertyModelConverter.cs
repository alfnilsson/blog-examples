using System.Collections.Generic;
using System.Globalization;
using EPiServer.ContentApi.Core.Serialization;
using EPiServer.ContentApi.Core.Serialization.Models;
using EPiServer.Core;
using EPiServer.ServiceLocation;
using Toders.ContentApi.Core.ContentDelivery.ContentPropertyAttribute;

namespace Toders.ContentApi.Core.ContentDelivery.Serialization.HeadlessLink
{
    /// <summary>
    /// A PropertyModelConverter that will use another PropertyModel for ContentReference that should contain a URL instead of only the ContentReference value.
    /// </summary>
    // Add this PropertyModelConverter together with all other PropertyModelConverters. Episerver will iterate through all by descending SortOrder
    [ServiceConfiguration(typeof(IPropertyModelConverter), Lifecycle = ServiceInstanceScope.Singleton)]
    public class HeadlessLinkPropertyModelConverter : IPropertyModelConverter
    {
        private readonly IContentPropertyAttribute contentPropertyAttribute;

        public HeadlessLinkPropertyModelConverter(IContentPropertyAttribute contentPropertyAttribute)
        {
            this.contentPropertyAttribute = contentPropertyAttribute;
        }

        public bool HasPropertyModelAssociatedWith(PropertyData propertyData)
        {
            var propertyContentReference = propertyData as PropertyContentReference;
            return this.IsHeadlessLink(propertyContentReference);
        }

        public IPropertyModel ConvertToPropertyModel(
            PropertyData propertyData,
            CultureInfo language,
            bool excludePersonalizedContent,
            bool expand)
        {
            var propertyContentReference = propertyData as PropertyContentReference;
            if (this.IsHeadlessLink(propertyContentReference) == false)
            {
                return null;
            }

            return new HeadlessContentReferencePropertyModel(propertyContentReference);
        }

        /// <summary>
        /// Looks at the Property to define whether it's Property representation in code is decorated with the HeadlessLinkAttribute.
        /// </summary>
        /// <param name="propertyContentReference">The PropertyContentReference that might have the HeadlessLinkAttribute.</param>
        /// <returns>True or false depending on whether the Property is decorated the HeadlessLinkAttribute.</returns>
        private bool IsHeadlessLink(PropertyContentReference propertyContentReference)
        {
            if (propertyContentReference == null)
            {
                return false;
            }

            var attribute = this.contentPropertyAttribute.Get<HeadlessLinkAttribute>(propertyContentReference);
            if (attribute == null)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// The DefaultPropertyModelConverter have SortOrder 0, having a higher SortOrder will place this before the default
        /// </summary>
        public int SortOrder { get; } = 1;

        /// <summary>
        /// This is not used by any other place in Episerver (as of Episerver.ContentDelivery.Cms 2.4.0)
        /// </summary>
        public IEnumerable<TypeModel> ModelTypes { get; } = new TypeModel[0];
    }
}

using EPiServer.ContentApi.Core.Serialization.Models;
using ImageVault.EPiServer;

namespace Toders.ContentApi.ContentDelivery.Serialization.Models
{
    // Inherit PropertyModel<TValue, TType> where TValue is the type of the Value property and TType is the type of your PropertyData.
    public class ImageVaultMediaReferencePropertyModel : PropertyModel<MediaReference, PropertyMedia>
    {
        public ImageVaultMediaReferencePropertyModel(PropertyMedia propertyMedia)
            : base(propertyMedia)
        {
            // Set the Value to the value of your PropertyData
            this.Value = propertyMedia.MediaReference;
        }
    }
}
using EPiServer.ContentApi.Core;
using ImageVault.EPiServer;

namespace Toders.ContentApi.Site.ContentDelivery.PropertyModels
{
    public class ImageVaultMediaReferencePropertyModel : PropertyModel<MediaReference, PropertyMedia>
    {
        public ImageVaultMediaReferencePropertyModel(PropertyMedia propertyMedia)
            : base(propertyMedia)
        {
            this.Value = propertyMedia.MediaReference;
        }
    }
}
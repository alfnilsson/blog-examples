using System.Linq;
using EPiServer;
using EPiServer.ContentApi.Core;
using EPiServer.Web;
using ImageVault.Client;
using ImageVault.Common.Data;
using ImageVault.EPiServer;
using PocHeadlessCms.Site.ContentDelivery.PropertyModels.Models;

namespace Toders.ContentApi.Site.ContentDelivery.PropertyModels
{
    public class MoreInfoImageVaultMediaReferencePropertyModel : PropertyModel<MoreInfoMediaReference, PropertyMedia>
    {
        public MoreInfoImageVaultMediaReferencePropertyModel(PropertyMedia propertyMedia)
            : base(propertyMedia)
        {
            var media = ClientFactory.GetSdkClient()
                .Load<Media>(propertyMedia.MediaReference, new PropertyMediaSettings())
                .FirstOrDefault();

            string absoluteUrl = UriSupport.Combine(SiteDefinition.Current.SiteUrl.ToString(), media.Url);
            this.Value = new MoreInfoMediaReference
            {
                MediaReference = propertyMedia.MediaReference,
                Url = absoluteUrl
            };
        }
    }
}
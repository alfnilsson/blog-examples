using System.Linq;
using System.Web.Mvc;
using EPiServer.ContentApi.Core.Serialization.Models;
using EPiServer.Web;
using ImageVault.Client;
using ImageVault.Common.Data;
using ImageVault.EPiServer;
using Toders.ContentApi.Core.ContentDelivery.Serialization.Models.ImageVault;

namespace Toders.ContentApi.Core.ContentDelivery.Serialization.Models
{
    // Inherit PropertyModel<TValue, TType> where TValue is the type of the Value property and TType is the type of your PropertyData.
    // In this example we want to have a more complex model containing the image url that should be delivered with the JSON response.
    public class MoreInfoImageVaultMediaReferencePropertyModel : PropertyModel<MoreInfoMediaReferenceModel, PropertyMedia>
    {
        public MoreInfoImageVaultMediaReferencePropertyModel(PropertyMedia propertyMedia)
            : base(propertyMedia)
        {
            var mediaReference = propertyMedia.MediaReference;
            if (mediaReference == null)
            {
                return;
            }

            this.Value = new MoreInfoMediaReferenceModel
            {
                MediaReference = mediaReference,
                Url = GetImageUrl(mediaReference)
            };
        }

        /// <summary>
        /// Using the ImageVault Sdk Client, fetch the Media for the reference
        /// </summary>
        /// <param name="mediaReference">The reference to the media.</param>
        /// <returns></returns>
        private static string GetImageUrl(MediaReference mediaReference)
        {
            var media = ClientFactory.GetSdkClient()
                .Load<Media>(mediaReference, new ViewContext(), new PropertyMediaSettings())
                .FirstOrDefault();

            if (media == null)
            {
                return null;
            }

            return UriUtil.Combine(SiteDefinition.Current.SiteUrl.ToString(), media.Url);
        }
    }
}
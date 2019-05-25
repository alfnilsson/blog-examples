using System.Globalization;
using EPiServer.ContentApi.Core.Serialization.Models;
using EPiServer.Core;
using EPiServer.ServiceLocation;
using EPiServer.Web.Routing;
using Toders.ContentApi.Core.ContentDelivery.Serialization.Models.HeadlessLink;

namespace Toders.ContentApi.Core.ContentDelivery.Serialization.HeadlessLink
{
    public class HeadlessContentReferencePropertyModel : PropertyModel<HeadlessLinkModel, PropertyContentReference>
    {
        public HeadlessContentReferencePropertyModel(PropertyContentReference propertyContentReference)
            : base(propertyContentReference)
        {
            ContentReference contentLink = propertyContentReference.ContentLink;
            if (ContentReference.IsNullOrEmpty(contentLink))
            {
                return;
            }

            var cultureInfo = GetCultureInfo(propertyContentReference);

            var url = GetUrl(contentLink, cultureInfo);
            this.Value = new HeadlessLinkModel
            {
                ContentLink = new ContentModelReference // We need to convert the ContentReference to a ContentModelReference. ContentReference is not very serializable.
                {
                    Id = propertyContentReference.ID,
                    WorkId = propertyContentReference.WorkID,
                    GuidValue = propertyContentReference.GuidValue,
                    ProviderName = propertyContentReference.ProviderName
                },
                Url = url
            };
        }

        private static string GetUrl(ContentReference contentLink, CultureInfo cultureInfo)
        {
            var urlResolver = ServiceLocator.Current.GetInstance<IUrlResolver>();
            return urlResolver.GetUrl(contentLink, cultureInfo.Name, new UrlResolverArguments
            {
                ForceCanonical = true
            });
        }

        private static CultureInfo GetCultureInfo(PropertyContentReference propertyContentReference)
        {
            string languageBranch = propertyContentReference.Parent.LanguageBranch;
            if (string.IsNullOrWhiteSpace(languageBranch))
            {
                return null;
            }

            return new CultureInfo(languageBranch);
        }
    }
}

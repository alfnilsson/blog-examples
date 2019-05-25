using EPiServer.ContentApi.Core.Serialization.Models;

namespace Toders.ContentApi.Core.ContentDelivery.Serialization.Models.HeadlessLink
{
    public class HeadlessLinkModel
    {
        public ContentModelReference ContentLink { get; set; }

        public string Url { get; set; }
    }
}

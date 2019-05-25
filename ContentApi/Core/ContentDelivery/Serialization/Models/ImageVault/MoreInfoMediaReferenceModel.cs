using ImageVault.EPiServer;

namespace Toders.ContentApi.Core.ContentDelivery.Serialization.Models.ImageVault
{
    /// <summary>
    /// A more complex model that can contain information that will be serialized in the JSON result.
    /// </summary>
    public class MoreInfoMediaReferenceModel
    {
        public MediaReference MediaReference { get; set; }

        public string Url { get; set; }
    }
}
using EPiServer.ContentApi.Core;
using EPiServer.Core;

namespace Toders.ContentApi.Site.ContentDelivery.PropertyModels
{
    public class TypeModel
    {
        public PropertyData PropertyType { get; set; }
        public IPropertyModel ModelType { get; set; }
        public string Name { get; set; }
    }
}
using System;
using EPiServer.Core;

namespace Toders.ContentApi.Core.ContentDelivery.ContentPropertyAttribute
{
    public interface IContentPropertyAttribute
    {
        TAttribute Get<TAttribute>(PropertyData propertyData)
            where TAttribute : Attribute;
    }
}

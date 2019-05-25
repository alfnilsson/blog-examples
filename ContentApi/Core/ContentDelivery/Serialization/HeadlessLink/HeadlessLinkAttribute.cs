using System;

namespace Toders.ContentApi.Core.ContentDelivery.Serialization.HeadlessLink
{
    /// <summary>
    /// Make ContentReference properties urls in Headless
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class HeadlessLinkAttribute : Attribute
    {
    }
}

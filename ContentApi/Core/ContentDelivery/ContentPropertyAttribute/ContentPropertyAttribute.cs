using System;
using System.Linq;
using System.Reflection;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.ServiceLocation;

namespace Toders.ContentApi.Core.ContentDelivery.ContentPropertyAttribute
{
    /// <summary>
    /// A tool that helps you find a specific Attribute for a PropertyData.
    /// </summary>
    [ServiceConfiguration(ServiceType = typeof(IContentPropertyAttribute))]
    public class ContentPropertyAttribute : IContentPropertyAttribute
    {
        private readonly IPropertyDefinitionRepository propertyDefinitionRepository;
        private readonly IContentTypeRepository contentTypeRepository;

        public ContentPropertyAttribute()
        {
            IServiceLocator serviceLocator = ServiceLocator.Current;
            this.propertyDefinitionRepository = serviceLocator.GetInstance<IPropertyDefinitionRepository>();
            this.contentTypeRepository = serviceLocator.GetInstance<IContentTypeRepository>();
        }

        public TAttribute Get<TAttribute>(PropertyData propertyData)
            where TAttribute : Attribute
        {
            var contentTypeType = this.GetContentTypeType(propertyData);
            if (contentTypeType == null)
            {
                return null;
            }

            PropertyInfo[] properties = contentTypeType.GetProperties();

            var property = properties.FirstOrDefault(p => p.Name == propertyData.Name);
            if (property == null)
            {
                return default(TAttribute);
            }

            var attribute = this.GetAttribute<TAttribute>(property);
            return attribute;
        }

        /// <summary>
        /// Get the ContentType that this PropertyData is defined on.
        /// </summary>
        /// <param name="propertyData">The Property Data</param>
        /// <returns></returns>
        private Type GetContentTypeType(PropertyData propertyData)
        {
            PropertyDefinition propertyDefinition = this.propertyDefinitionRepository.Load(propertyData.PropertyDefinitionID);
            ContentType contentType = this.contentTypeRepository.Load(propertyDefinition.ContentTypeID);
            Type contentTypeType = contentType.ModelType;
            return contentTypeType;
        }

        /// <summary>
        /// Get all Attributes for a specific MemberInfo.
        /// </summary>
        /// <typeparam name="TAttribute">The Attribute Type to look for.</typeparam>
        /// <param name="member">The MemberInfo that might contain the Attribute.</param>
        /// <returns></returns>
        private TAttribute GetAttribute<TAttribute>(MemberInfo member)
            where TAttribute : Attribute

        {
            object[] customAttributes = member.GetCustomAttributes(typeof(TAttribute), false);
            foreach (object o in customAttributes)
            {
                return (TAttribute)o;
            }

            return default(TAttribute);
        }
    }
}
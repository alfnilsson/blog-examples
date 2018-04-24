using System;
using System.Linq;
using EPiServer.ContentApi.Core;
using EPiServer.Core;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using ImageVault.EPiServer;
using Toders.ContentApi.Site.ContentDelivery.PropertyModels;

namespace Toders.ContentApi.Site.ContentDelivery
{
    [InitializableModule]
    [ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
    public class InitializationModule : IInitializableModule
    {
        public void Initialize(InitializationEngine context)
        {
            var propertyModelHandler = ServiceLocator.Current.GetInstance<IPropertyModelHandler>();

			// Add a new Property Model, Episerver won't output Property Types that don't have a Property Model
            propertyModelHandler.ModelTypes.Add(new TypeModel
            {
                ModelType = typeof(MoreInfoImageVaultMediaReferencePropertyModel),
                ModelTypeString = "ImageVaultMediaReferencePropertyModel",
                PropertyType = typeof(PropertyMedia)
            });

            // Replace the default NumberPropertyModel
            Type property = typeof(PropertyNumber);
            Type newPropertyModel = typeof(CustomNumberPropertyModel);
            var propertyModels = propertyModelHandler.ModelTypes
                .Where(x => x.PropertyType == property);
            foreach (TypeModel contentAreaPropertyModel in propertyModels)
            {
                contentAreaPropertyModel.ModelType = newPropertyModel;
            }
        }

        public void Uninitialize(InitializationEngine context)
        {
        }

        public void ConfigureContainer(ServiceConfigurationContext context)
        {
        }
    }
}
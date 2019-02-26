using EPiServer.Framework;
using EPiServer.Framework.Initialization;

namespace Toders.ContentApi.Site.ContentDelivery
{
    using System.Web.Mvc;
    using System.Web.Routing;

    using EPiServer;
    using EPiServer.ContentApi.Cms;
    using EPiServer.ContentApi.Core.Configuration;
    using EPiServer.ContentApi.Core.Security;
    using EPiServer.ContentApi.Core.Serialization;
    using EPiServer.Security;
    using EPiServer.ServiceLocation;

    using NetR.Nobia.BrandAddons.Headless.Core;
    using NetR.Nobia.BrandAddons.Headless.Security;

    [InitializableModule]
    [ModuleDependency(typeof(ContentApiCmsInitialization))]
    public class ConfigurableModule : IConfigurableModule
    {
        public void Initialize(InitializationEngine context)
        {
        }

        public void Uninitialize(InitializationEngine context)
        {
        }

        public void ConfigureContainer(ServiceConfigurationContext context)
        {
            context.Services
                // Override that Episerver rewrites incoming requests which have accept headers 'application/json' to ContentDelivery API.
                .Intercept<EPiServer.ContentApi.Routing.ContentApiRouteService>((locator, contentApiRouteService)
                    => new Routing.ContentApiRouteService());
        }
    }
}

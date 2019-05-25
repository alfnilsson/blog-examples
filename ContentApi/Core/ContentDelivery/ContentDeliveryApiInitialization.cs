using EPiServer.ContentApi.Cms;
using EPiServer.ContentApi.Core.Configuration;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using Toders.ContentApi.Core.ContentDelivery.Routing;

namespace Toders.ContentApi.Core.ContentDelivery
{
    [ModuleDependency(typeof(ContentApiCmsInitialization))]
    public class ContentDeliveryApiInitialization : IConfigurableModule
    {
        public void Initialize(InitializationEngine context)
        {
            //Add initialization logic, this method is called once after CMS has been initialized
        }

        public void Uninitialize(InitializationEngine context)
        {
            //Add uninitialization logic
        }


        public void ConfigureContainer(ServiceConfigurationContext context)
        {
            // set minimumRoles to empty to allow anonymous calls (for visitors to view site in view mode)
            context.Services.Configure<ContentApiConfiguration>(config =>
            {
                config.Default().SetMinimumRoles(string.Empty);
            });

            context.Services
                // Override that Episerver rewrites incoming requests which have accept headers 'application/json' to ContentDelivery API.
                .Intercept<EPiServer.ContentApi.Routing.ContentApiRouteService>((locator, contentApiRouteService)
                    => new ContentApiRouteService());
        }
    }
}
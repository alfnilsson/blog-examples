using System.Web;

namespace Toders.ContentApi.Core.ContentDelivery.Routing
{
    public class ContentApiRouteService : EPiServer.ContentApi.Routing.ContentApiRouteService
    {
        public override bool ShouldRouteRequest(HttpRequestBase request)
        {
            return false;
        }
    }
}

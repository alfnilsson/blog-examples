using System.Web;

namespace Toders.ContentApi.Site.ContentDelivery
{
    public class ContentApiRouteService : EPiServer.ContentApi.Routing.ContentApiRouteService
    {
        public override bool ShouldRouteRequest(HttpRequestBase request)
        {
            return false;
        }
    }
}

using EPiServer.ContentApi.Core.Serialization.Models;
using EPiServer.Core;

namespace Toders.ContentApi.Core.ContentDelivery.Serialization.BlockAsProperty
{
    /// <summary>
    /// A ModelMapper that is quite identical to EPiServer.ContentApi.Core.Serialization.IContentModelMapper
    /// </summary>
    public interface IBlockModelMapper
    {
        ContentApiModel TransformContent(BlockData block);
    }
}

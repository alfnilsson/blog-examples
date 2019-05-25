using EPiServer.ContentApi.Core.Serialization.Models;
using EPiServer.Core;
using EPiServer.ServiceLocation;
using EPiServer.SpecializedProperties;

namespace Toders.ContentApi.Core.ContentDelivery.Serialization.BlockAsProperty
{
    /// <summary>
    /// A specific PropertyModel to be used when Blocks are added as Properties to a Content.
    /// </summary>
    public class BlockPropertyModel : PropertyModel<ContentApiModel,
        PropertyBlock>
    {
        public BlockPropertyModel(PropertyBlock propertyBlock)
            : base(propertyBlock)
        {
            var block = propertyBlock.Value as BlockData;
            if (block == null)
            {
                return;
            }

            // We want to serialize this similar to how normal Content is serialied.
            var blockModelMapper = ServiceLocator.Current.GetInstance<IBlockModelMapper>();
            ContentApiModel contentApiModel = blockModelMapper
                .TransformContent(block);
            this.Value = contentApiModel;
        }
    }
}

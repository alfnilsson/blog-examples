using EPiServer;
using EPiServer.ContentApi.Core.Serialization;
using EPiServer.ContentApi.Core.Serialization.Models;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.ServiceLocation;

namespace Toders.ContentApi.Core.ContentDelivery.Serialization.BlockAsProperty
{
    /// <summary>
    /// Implementation of IBlockModelMapper that is used to convert the Block into a ContentApiModel by "faking" that the block would be a Content Block instead of a Property Block.
    /// </summary>
    [ServiceConfiguration(ServiceType = typeof(IBlockModelMapper))]
    public class BlockModelMapper : IBlockModelMapper
    {
        private readonly IContentRepository contentRepository;

        private readonly IContentModelMapper contentModelMapper;

        private readonly IContentTypeRepository contentTypeRepository;

        public BlockModelMapper(
            IContentRepository contentRepository,
            IContentModelMapper contentModelMapper,
            IContentTypeRepository contentTypeRepository)
        {
            this.contentRepository = contentRepository;
            this.contentModelMapper = contentModelMapper;
            this.contentTypeRepository = contentTypeRepository;
        }

        public virtual ContentApiModel TransformContent(BlockData block)
        {
            BlockData fakeContent = this.CreateFakeContent(block);
            if (fakeContent == null)
            {
                return null;
            }

            CopyPropertyValuesToFake(block, fakeContent);
            // ReSharper disable once SuspiciousTypeConversion.Global
            ContentApiModel contentApiModel = this.contentModelMapper.TransformContent((IContent)fakeContent);
            CleanupFakeData(contentApiModel);

            return contentApiModel;
        }

        private BlockData CreateFakeContent(BlockData block)
        {
            ContentType contentType = this.contentTypeRepository.Load(block.GetOriginalType());
            if (contentType == null)
            {
                return null;
            }

            var contentTypeId = contentType.ID;
            var fakeContent = this.contentRepository.GetDefault<BlockData>(ContentReference.SiteBlockFolder, contentTypeId);
            return fakeContent;
        }

        private static void CopyPropertyValuesToFake(BlockData block, BlockData fakeContent)
        {
            var propertyDataCollection = block.Property.CreateWritableClone();
            foreach (var propertyData in propertyDataCollection)
            {
                fakeContent.Property.Set(propertyData.Name, propertyData);
            }
        }

        private static void CleanupFakeData(ContentApiModel contentApiModel)
        {
            contentApiModel.ContentLink = null;
            contentApiModel.ParentLink = null;
            contentApiModel.Name = null;
        }
    }
}

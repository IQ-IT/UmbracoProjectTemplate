using System;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using UmbracoProject.Configure.Models;

namespace UmbracoProject.Configure.Entities
{
    public static class ContentTypes
    {
        public static ContentTypesCreationModel Create(TemplatesCreationModel templates, ServiceContext context)
        {
            var result = new ContentTypesCreationModel();
            var compositionsFolder = context.ContentTypeService.CreateContentTypeContainer(-1, "Compositions");
            var pageContentTypeFolder = context.ContentTypeService.CreateContentTypeContainer(-1, "Pages");

            // Containers folder and website contenttype
            result.ContainersFolderId = CreateFolder(context, "Containers");
            result.WebsiteType = CreateWebsitetype(context, result.ContainersFolderId);

            // Compositions folder and compositionstypes
            result.CompositionsFolderId = CreateFolder(context, "Compositions");
            var contentComposition = CreateContentComposition(context, result.CompositionsFolderId);
            var metadataComposition = CreateMetadataComposition(context, result.CompositionsFolderId);


            throw new NotImplementedException();
        }

        private static ContentType CreateContentComposition(ServiceContext context, int compositionsFolderId)
        {
            var contentType = new ContentType(compositionsFolderId)
            {
                Alias = "ContentComposition",
                Description = "Composable content type with properties for ordinary text content",
                Icon = "icon-tab-key color-yellow",
                Name = "ContentComposition",
            };
            context.ContentTypeService.Save(contentType);
            var contentTab = new PropertyGroup(CreateContentPropertyTypes())
            {
                Name = "Content",
                SortOrder = 10
            };
            contentType.PropertyGroups.Add(contentTab);
            context.ContentTypeService.Save(contentType);
            return contentType;
        }

        private static PropertyTypeCollection CreateContentPropertyTypes()
        {
            throw new NotImplementedException();
        }


        private static ContentType CreateMetadataComposition(ServiceContext context, int compositionsFolderId)
        {
            var metadataType = new ContentType(compositionsFolderId)
            {
                Alias = "MetadataComposition",
                Description = "Composable content type for handling of metadata for pages",
                Icon = "icon-tab-key color-yellow",
                Name = "MetadataComposition",
            };
            context.ContentTypeService.Save(metadataType);
            // TODO: Create properties
            return metadataType;
        }

        private static ContentType CreateWebsitetype(ServiceContext context, int containersFolderId)
        {
            var websiteType = new ContentType(containersFolderId)
            {
                Alias = "Website",
                AllowedAsRoot = true,
                Description =
                    "Used as container for both content pages and global content for a website, must contain at least one frontpage element",
                Icon = "icon-globe-inverted-europe-africa",
                Name = "Website"
            };
            context.ContentTypeService.Save(websiteType);
            return websiteType;
        }

        private static int CreateFolder(ServiceContext context, string name)
        {
            var createAttempt = context.ContentTypeService.CreateContentTypeContainer(-1, name);
            if (!createAttempt) throw new InvalidOperationException("Exception ocurred trying to create content type folder");
            return createAttempt.Result.Entity.Id;
        }
    }
}
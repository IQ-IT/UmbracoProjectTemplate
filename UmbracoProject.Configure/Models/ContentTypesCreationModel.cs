using Umbraco.Core.Models;

namespace UmbracoProject.Configure.Models
{
    public class ContentTypesCreationModel
    {
        public int CompositionsFolderId { get; set; }
        public int ContainersFolderId { get; set; }
        public int PagesFolderId { get; set; }
        public ContentType WebsiteType { get; set; }
        public ContentType FrontpageType { get; set; }
        public ContentType TextpageType { get; set; }
    }
}
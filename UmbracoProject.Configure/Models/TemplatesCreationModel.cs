using Umbraco.Core.Models;

namespace UmbracoProject.Configure.Models
{
    public class TemplatesCreationModel
    {
        public ITemplate FrontpageTemplate { get; set; }
        public ITemplate TextpageTemplate { get; set; }
    }
}
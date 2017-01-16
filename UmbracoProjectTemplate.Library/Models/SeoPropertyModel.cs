namespace UmbracoProjectTemplate.Library.Models
{
    public class SeoPropertyModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool NoIndex { get; set; }
        public bool NoFollow { get; set; }
        public int CanonicalId { get; set; }
    }
}
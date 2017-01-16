using Newtonsoft.Json;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Core.PropertyEditors;
using UmbracoProjectTemplate.Library.Models;

namespace UmbracoProjectTemplate.Library.TypeConverters
{
    public class SeoPropertyValueConverter : PropertyValueConverterBase
    {
        public override bool IsConverter(PublishedPropertyType propertyType)
        {
            return propertyType.PropertyEditorAlias.Equals("Creuna.SeoPropertyEditor");
        }

        public override object ConvertSourceToObject(PublishedPropertyType propertyType, object source, bool preview)
        {
            if (source == null)
                return new SeoPropertyValueConverter();

            return JsonConvert.DeserializeObject<SeoPropertyModel>((string) source);
        }
    }
}
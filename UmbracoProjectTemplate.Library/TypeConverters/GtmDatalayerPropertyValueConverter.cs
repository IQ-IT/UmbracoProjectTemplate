using System.Collections.Generic;
using Newtonsoft.Json;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Core.PropertyEditors;

namespace UmbracoProjectTemplate.Library.TypeConverters
{
    public class GtmDatalayerPropertyValueConverter : PropertyValueConverterBase
    {
        public override bool IsConverter(PublishedPropertyType propertyType)
        {
            return propertyType.PropertyEditorAlias.Equals("Addition.GtmDatalayerPropertyEditor");
        }

        public override object ConvertSourceToObject(PublishedPropertyType propertyType, object source, bool preview)
        {
            if (source == null) 
                return new Dictionary<string, string>();

            return JsonConvert.DeserializeObject<Dictionary<string, string>>((string) source);
        }
    }
}
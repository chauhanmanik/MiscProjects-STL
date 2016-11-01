using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.IO;

namespace LASearch3
{
    public static class JsonHtmlHelpers
    {
        public static IHtmlContent JsonFor<T>(this HtmlHelper helper, T obj)
        {
            using (var stringWriter = new StringWriter())
            using (var jsonWriter = new JsonTextWriter(stringWriter))
            {
                var serializer = new JsonSerializer
                {
                    // Let's use camelCasing as is common practice in JavaScript
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                jsonWriter.StringEscapeHandling = StringEscapeHandling.EscapeHtml;

                // We don't want quotes around object names
                jsonWriter.QuoteName = false;
                serializer.Serialize(jsonWriter, obj);



                return new HtmlString(stringWriter.ToString());
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Clonked.Basecamp
{
    internal class ApiJsonConverter : JsonConverter
    {
        public Api Api { get; set; }

        public override bool CanConvert(Type objectType)
        {
            if (objectType == typeof(Api))
            {
                return true;
            }

            return false;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var api = existingValue as Api;
            if (api == null)
            {
                existingValue = Api;
            }

            return existingValue;
        }
    }
}

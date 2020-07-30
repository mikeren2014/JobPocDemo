using System;
using Newtonsoft.Json;
using static Newtonsoft.Json.JsonToken;
using static Newtonsoft.Json.Linq.JObject;

namespace JobPocDemo.Jobs
{
    public class JobConverter : JsonConverter
    {
        #region fields

        readonly string _type;

        #endregion

        #region constructors

        public JobConverter(string type) => _type = type;

        #endregion

        #region properties

        public override bool CanWrite => false;

        #endregion

        #region methods

        public override bool CanConvert(Type objectType) => objectType.IsAssignableFrom(typeof(IJob));

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType is Null)
                return null;

            var jObject = Load(reader);

            return _type switch
            {
                "CopyAccountSettingsJob" => jObject.ToObject<CopyAccountSettingsJob>(serializer),
                "CopyAccountJob" => jObject.ToObject<CopyAccountJob>(serializer),
                "CopyCatalogJob" => jObject.ToObject<CopyCatalogJob>(serializer),
                "CopyComponentJob" => jObject.ToObject<CopyComponentJob>(serializer),
                "CopyPriceJob" => jObject.ToObject<CopyPriceJob>(serializer),
                "CopyProductJob" => jObject.ToObject<CopyProductJob>(serializer),
                _ => null
            };
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer) => throw new NotImplementedException();

        #endregion
    }
}

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Penguin.Json.Abstractions.Interfaces;
using System;
using System.IO;

namespace Penguin.Json.JsonConverters
{
    public class IJsonPopulatedObjectConverter : JsonConverter<IJsonPopulatedObject>
    {
        public bool FaultTolerant { get; private set; }
        public IJsonPopulatedObjectConverter(bool faultTolerant = false) => this.FaultTolerant = faultTolerant;

        public override bool CanWrite => false;
        public override IJsonPopulatedObject ReadJson(JsonReader reader, Type objectType, IJsonPopulatedObject existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader is null)
            {
                throw new ArgumentNullException(nameof(reader));
            }

            if (serializer is null)
            {
                throw new ArgumentNullException(nameof(serializer));
            }

            if(reader.TokenType != JsonToken.StartObject)
            {
                return !this.FaultTolerant
                    ? throw new Exception($"Json object expected at path '{reader.Path}', but found {reader.TokenType}")
                    : (IJsonPopulatedObject)null;
            }

            JToken currentObject = JObject.ReadFrom(reader);

            //https://www.debugcn.com/en/article/68905262.html
            IJsonPopulatedObject toReturn = (IJsonPopulatedObject)Activator.CreateInstance(objectType);

            toReturn.RawJson = currentObject.ToString();

            serializer.Populate(new StringReader(toReturn.RawJson), toReturn);

            return toReturn;

        }

        public override void WriteJson(JsonWriter writer, IJsonPopulatedObject value, JsonSerializer serializer) => throw new NotImplementedException();
    }
}

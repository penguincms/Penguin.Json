using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Penguin.SystemExtensions.Abstractions.Interfaces;
using System;

namespace Penguin.Json.JsonConverters
{
    public class JTokenConvertibleConverter : JsonConverter<IConvertible<JToken>>
    {
        public JTokenConvertibleConverter()
        {
        }

        public override IConvertible<JToken> ReadJson(JsonReader reader, Type objectType, IConvertible<JToken> existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader is null)
            {
                throw new ArgumentNullException(nameof(reader));
            }

            if (objectType is null)
            {
                throw new ArgumentNullException(nameof(objectType));
            }

            JToken item = JToken.Load(reader);

            IConvertible<JToken> convertable = Activator.CreateInstance(objectType) as IConvertible<JToken>;

            convertable.Convert(item);

            return convertable;
        }

        public override void WriteJson(JsonWriter writer, IConvertible<JToken> value, JsonSerializer serializer)
        {
            if (writer is null)
            {
                throw new ArgumentNullException(nameof(writer));
            }

            string toWrite = value?.Convert()?.ToString();

            if (toWrite is null)
            {
                writer.WriteNull();
            }
            else
            {
                writer.WriteRawValue(toWrite);
            }
        }
    }
}
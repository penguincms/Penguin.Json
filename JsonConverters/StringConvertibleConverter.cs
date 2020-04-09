using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Penguin.SystemExtensions.Abstractions.Interfaces;
using System;

namespace Penguin.Json.JsonConverters
{
    public class StringConvertibleConverter : JsonConverter<IConvertible<string>>
    {
        public StringConvertibleConverter()
        {
        }

        public override IConvertible<string> ReadJson(JsonReader reader, Type objectType, IConvertible<string> existingValue, bool hasExistingValue, JsonSerializer serializer)
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

            string value = item.ToString();

            IConvertible<string> convertable = Activator.CreateInstance(objectType) as IConvertible<string>;

            convertable.Convert(value);

            return convertable;
        }

        public override void WriteJson(JsonWriter writer, IConvertible<string> value, JsonSerializer serializer)
        {
            if (writer is null)
            {
                throw new ArgumentNullException(nameof(writer));
            }

            if (value is null)
            {
                writer.WriteNull();
            }
            else
            {
                writer.WriteValue(value.Convert());
            }
        }
    }
}
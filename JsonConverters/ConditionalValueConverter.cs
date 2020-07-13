using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Penguin.Json.Abstractions.Attributes;
using Penguin.Reflection.Extensions;
using System;
using System.Linq;

namespace Penguin.Json.JsonConverters
{
    public class ConditionalValueConverter : JsonConverter<ConditionalValue>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override ConditionalValue ReadJson(JsonReader reader, Type objectType, ConditionalValue existingValue, bool hasExistingValue, JsonSerializer serializer)
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

            Type genericType;

            Type toCheck = objectType;

            while (!toCheck.GenericTypeArguments.Any())
            {
                toCheck = toCheck.BaseType;

                if (toCheck is null)
                {
                    throw new NullReferenceException("Unable to find generic type arguments in inheritance tree");
                }
            }

            genericType = toCheck.GenericTypeArguments[0];

            object realValue = value.Convert(genericType);

            return Activator.CreateInstance(objectType, new object[] { realValue }) as ConditionalValue;
        }

        public override void WriteJson(JsonWriter writer, ConditionalValue value, JsonSerializer serializer)
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
                writer.WriteValue(value.ToString());
            }
        }
    }
}
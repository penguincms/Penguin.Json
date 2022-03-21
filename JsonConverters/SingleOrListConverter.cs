﻿using Newtonsoft.Json;
using Penguin.Reflection.Extensions;
using System;
using System.Collections;

namespace Penguin.Json.JsonConverters
{
    public class SingleOrListConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType) => objectType.IsList();

        public override bool CanRead => true;
        public override bool CanWrite => false;

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader is null)
            {
                throw new ArgumentNullException(nameof(reader));
            }

            if (serializer is null)
            {
                throw new ArgumentNullException(nameof(serializer));
            }

            Type collectionType = objectType.GetCollectionType();
            IList collection = Activator.CreateInstance(objectType) as IList;
            object item;

            switch (reader.TokenType)
            {
                case JsonToken.StartArray:

                    //read into the list
                    reader.Read();

                    int cdepth = reader.Depth;

                    while (reader.Depth >= cdepth)
                    {
                        if (reader.Depth == cdepth)
                        {
                            item = serializer.Deserialize(reader, collectionType);

                            collection.Add(item);
                        }

                        reader.Read();
                    }

                    return collection;

                case JsonToken.StartObject:

                    item = serializer.Deserialize(reader, collectionType);

                    collection.Add(item);

                    return collection;
                case JsonToken.Null:
                case JsonToken.Undefined:
                    return null;
                default:
                    throw new NotImplementedException($"Unhandled token type {reader.TokenType}");
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) => throw new NotImplementedException();
    }
}

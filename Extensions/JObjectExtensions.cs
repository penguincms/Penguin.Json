﻿using Newtonsoft.Json.Linq;
using System;
using System.Reflection;

namespace Penguin.Json.Extensions
{
    public static class JObjectExtensions
    {
        public static TReturn Property<TReturn>(this JObject source, PropertyInfo propertyInfo) where TReturn : class
        {
            return propertyInfo is null ? throw new ArgumentNullException(nameof(propertyInfo)) : source.Property<TReturn>(propertyInfo, false);
        }

        public static JProperty Property(this JObject source, PropertyInfo propertyInfo)
        {
            return source is null ? throw new ArgumentNullException(nameof(source)) : source.Property(propertyInfo.GetJsonName());
        }

        public static TReturn Remove<TReturn>(this JObject source, PropertyInfo propertyInfo) where TReturn : class
        {
            return propertyInfo is null ? throw new ArgumentNullException(nameof(propertyInfo)) : source.Property<TReturn>(propertyInfo, true);
        }

        private static TReturn Property<TReturn>(this JObject source, PropertyInfo propertyInfo, bool Remove) where TReturn : class
        {
            return source.Property<TReturn>(propertyInfo.GetJsonName(), propertyInfo.PropertyType, Remove);
        }

        private static TReturn Property<TReturn>(this JObject source, string propertyName, Type propertyType, bool Remove) where TReturn : class
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (string.IsNullOrWhiteSpace(propertyName))
            {
                throw new ArgumentException("Property name can not be null or whitespace", nameof(propertyName));
            }

            if (propertyType is null)
            {
                throw new ArgumentNullException(nameof(propertyType));
            }

            TReturn newValue = null;

            if (source.Property(propertyName)?.Value is JToken jEntity)
            {
                newValue = JsonConvert.DeserializeObject(jEntity.ToString(), propertyType) as TReturn;
            }

            if (Remove)
            {
                _ = source.Remove(propertyName);
            }

            return newValue;
        }
    }
}
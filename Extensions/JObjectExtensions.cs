using Newtonsoft.Json.Linq;
using System;
using System.Reflection;

namespace Penguin.Json.Extensions
{
    public static class JObjectExtensions
    {
        public static TReturn Property<TReturn>(this JObject source, PropertyInfo propertyInfo) where TReturn : class
        {
            return source.Property<TReturn>(propertyInfo, false);
        }

        public static JProperty Property(this JObject source, PropertyInfo propertyInfo)
        {
            return source.Property(propertyInfo.GetJsonName());
        }

        public static TReturn Remove<TReturn>(this JObject source, PropertyInfo propertyInfo) where TReturn : class
        {
            return source.Property<TReturn>(propertyInfo, true);
        }

        private static TReturn Property<TReturn>(this JObject source, PropertyInfo propertyInfo, bool Remove) where TReturn : class
        {
            return source.Property<TReturn>(propertyInfo.GetJsonName(), propertyInfo.PropertyType, Remove);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters", Justification = "<Pending>")]
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

            JToken jEntity = source.Property(propertyName).Value as JToken;

            if (Remove)
            {
                source.Remove(propertyName);
            }

            TReturn newValue = null;

            if (!(jEntity is null))
            {
                newValue = JsonConvert.DeserializeObject(jEntity.ToString(), propertyType) as TReturn;
            }

            return newValue;
        }
    }
}
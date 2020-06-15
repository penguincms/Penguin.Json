using Newtonsoft.Json;
using System;
using System.Reflection;

namespace Penguin.Json.Extensions
{
    public static class PropertyInfoExtensions
    {
        public static string GetJsonName(this PropertyInfo propertyInfo)
        {
            if (propertyInfo is null)
            {
                throw new ArgumentNullException(nameof(propertyInfo));
            }

            string jPropName = propertyInfo.Name;

            if (propertyInfo.GetCustomAttribute<JsonPropertyAttribute>() is JsonPropertyAttribute jp)
            {
                jPropName = jp.PropertyName ?? jPropName;
            }

            return jPropName;
        }
    }
}
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Penguin.Json.Abstractions.Interfaces;
using System;
using System.Collections;
using System.Linq;
using System.Reflection;

namespace Penguin.Json.Extensions
{
    public static class IJsonPopulatedObjectExtensions
    {
        public static string GetUpdatedJson(this IJsonPopulatedObject source, JsonSerializerSettings serializerSettings = null)
        {
            if (source is null)
            {
                return null;
            }

            return string.IsNullOrWhiteSpace(source.RawJson)
                ? JsonConvert.SerializeObject(source)
                : GetUpdatedJson(source, JObject.Parse(source.RawJson), serializerSettings);
        }

        internal static void SetRawJson(this IJsonPopulatedObject source, string rawJson)
        {
            if (source is null)
            {
                throw new System.ArgumentNullException(nameof(source));
            }

            if (string.IsNullOrEmpty(rawJson))
            {
                throw new System.ArgumentException("Json string can not be null or empty", nameof(rawJson));
            }

            JObject rawObject = JObject.Parse(rawJson);

            foreach (PropertyInfo propertyInfo in source.GetType().GetProperties().Where(p => p.GetIndexParameters().Length == 0))
            {
                if (propertyInfo.GetGetMethod() is null)
                {
                    continue;
                }

                object v = propertyInfo.GetValue(source);

                if (v is IJsonPopulatedObject jpo)
                {
                    JProperty property = rawObject.Property(propertyInfo);
                    jpo.SetRawJson(property.Value.ToString());
                    property.Remove();
                }
                else if (v is IEnumerable e && rawObject.Property(propertyInfo)?.Value is JArray aObjects)
                {
                    int index = 0;

                    foreach (object o in e)
                    {
                        if (o is IJsonPopulatedObject ijpo)
                        {
                            JToken thisObject = aObjects[index];
                            ijpo.SetRawJson(thisObject.ToString());
                        }

                        index++;
                    }
                }
            }

            source.RawJson = rawJson;
        }

        public static string GetPropertyName(this IJsonPopulatedObject o, string propertyName)
        {
            if(o is null)
            {
                return propertyName;
            }

            return GetPropertyInfoByJsonName(o, propertyName)?.Name ?? propertyName;
        }

        private static PropertyInfo GetPropertyInfoByJsonName(object o, string jsonName)
        {
            if (o is null)
            {
                throw new ArgumentNullException(nameof(o));
            }

            foreach (PropertyInfo pi in o.GetType().GetProperties())
            {
                if (pi.GetCustomAttribute<JsonPropertyAttribute>() is JsonPropertyAttribute jpa && jpa.PropertyName == jsonName)
                {
                    return pi;
                }
            }

            return null;
        }
        private static JToken GetToken(object o, Type oType, JsonSerializerSettings serializerSettings)
        {
            string jVal = null;

            if (oType is null)
            {
                if (o is null)
                {
                    throw new NullReferenceException("oType can not be null if there is no o to derive type from");
                }
                else
                {
                    oType = o.GetType();
                }
            }

            if (typeof(IJsonPopulatedObject).IsAssignableFrom(oType))
            {
                if (o is IJsonPopulatedObject jo)
                {
                    jVal = jo.GetUpdatedJson(serializerSettings);
                }
            }
            else
            {
                if (o != null)
                {
                    jVal = JsonConvert.SerializeObject(o, serializerSettings);
                }
            }

            if (jVal is null)
            {
                return null;
            }
            else
            {
                return JToken.Parse(jVal);
            }
        }

        private static string GetUpdatedJson(object source, JObject oldObject, JsonSerializerSettings serializerSettings = null)
        {
            serializerSettings = serializerSettings ?? new JsonSerializerSettings();

            foreach (PropertyInfo pi in source.GetType().GetProperties().Where(p => p.GetIndexParameters().Length == 0 && p.GetGetMethod() != null))
            {
                if (!(pi.GetCustomAttribute<JsonIgnoreAttribute>() is null))
                {
                    continue;
                }

                string jName = pi.GetJsonName();

                JProperty oldProp = oldObject.Property(jName);

                oldProp.Remove();

                object curVal = pi.GetValue(source);

                JToken jVal = null;

                if (curVal is IEnumerable e && !(e is string))
                {
                    JArray a = oldProp.Value as JArray ?? new JArray();

                    a.Clear();

                    foreach (object curI in e)
                    {
                        a.Add(GetToken(curI, null, serializerSettings));
                    }

                    jVal = a;
                }
                else
                {
                    jVal = GetToken(curVal, pi.PropertyType, serializerSettings);
                }

                oldObject.Add(jName, jVal);
            }

            return oldObject.ToString();
        }
    }
}
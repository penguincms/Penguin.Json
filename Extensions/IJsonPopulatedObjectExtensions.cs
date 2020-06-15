﻿using Newtonsoft.Json;
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

            if (string.IsNullOrWhiteSpace(source.RawJson))
            {
                return JsonConvert.SerializeObject(source);
            }

            return GetUpdatedJson(source, JObject.Parse(source.RawJson), serializerSettings);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters", Justification = "<Pending>")]
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
                    jpo.SetRawJson(property.ToString());
                    property.Remove();
                }
                else if (v is IEnumerable e)
                {
                    int index = 0;
                    JArray aObjects = rawObject.Property(propertyInfo).Value as JArray;

                    foreach (object o in e)
                    {
                        if (o is IJsonPopulatedObject ijpo)
                        {
                            JToken thisObject = aObjects[index];
                            ijpo.SetRawJson(thisObject.ToString());
                            thisObject.Remove();
                        }

                        index++;
                    }
                }
            }

            source.RawJson = rawJson;
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
                string jName = pi.GetJsonName();

                JProperty oldProp = oldObject.Property(jName);

                oldProp.Remove();

                object curVal = pi.GetValue(source);

                JToken jVal = null;

                if (curVal is IEnumerable e)
                {
                    JArray a = oldProp.Value as JArray;

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
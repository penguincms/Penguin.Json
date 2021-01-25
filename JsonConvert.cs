using Newtonsoft.Json;
using Penguin.Json.Abstractions.Interfaces;
using Penguin.Json.Extensions;
using System;
using System.Diagnostics;
using System.Xml;
using System.Xml.Linq;
using NJsonConvert = Newtonsoft.Json.JsonConvert;

namespace Penguin.Json
{
    public static class JsonConvert
    {
        public static Func<JsonSerializerSettings> DefaultSettings
        {
            get => NJsonConvert.DefaultSettings;
            set => NJsonConvert.DefaultSettings = value;
        }

        [DebuggerStepThrough]
        public static T DeserializeAnonymousType<T>(string value, T anonymousTypeObject)
        {
            return NJsonConvert.DeserializeAnonymousType<T>(value, anonymousTypeObject);
        }

        [DebuggerStepThrough]
        public static T DeserializeAnonymousType<T>(string value, T anonymousTypeObject, JsonSerializerSettings settings)
        {
            return NJsonConvert.DeserializeAnonymousType<T>(value, anonymousTypeObject, settings);
        }

        [DebuggerStepThrough]
        public static T DeserializeObject<T>(string value, JsonSerializerSettings settings)
        {
            T toReturn = NJsonConvert.DeserializeObject<T>(value, settings);

            if (toReturn is IJsonPopulatedObject jo)
            {
                jo.SetRawJson(value);
            }

            return toReturn;
        }

        [DebuggerStepThrough]
        public static object DeserializeObject(string value)
        {
            object toReturn = NJsonConvert.DeserializeObject(value);

            if (toReturn is IJsonPopulatedObject jo)
            {
                jo.SetRawJson(value);
            }

            return toReturn;
        }

        [DebuggerStepThrough]
        public static object DeserializeObject(string value, JsonSerializerSettings settings)
        {
            object toReturn = NJsonConvert.DeserializeObject(value, settings);

            if (toReturn is IJsonPopulatedObject jo)
            {
                jo.SetRawJson(value);
            }

            return toReturn;
        }

        [DebuggerStepThrough]
        public static object DeserializeObject(string value, Type type)
        {
            object toReturn = NJsonConvert.DeserializeObject(value, type);

            if (toReturn is IJsonPopulatedObject jo)
            {
                jo.SetRawJson(value);
            }

            return toReturn;
        }

        [DebuggerStepThrough]
        public static T DeserializeObject<T>(string value)
        {
            T toReturn = NJsonConvert.DeserializeObject<T>(value);

            if (toReturn is IJsonPopulatedObject jo)
            {
                jo.SetRawJson(value);
            }

            return toReturn;
        }

        public static object DeserializeObject(string value, Type type, JsonSerializerSettings settings)
        {
            object toReturn = NJsonConvert.DeserializeObject(value, type, settings);

            if (toReturn is IJsonPopulatedObject jo)
            {
                jo.SetRawJson(value);
            }

            return toReturn;
        }

        [DebuggerStepThrough]
        public static object DeserializeObject(string value, Type type, params JsonConverter[] converters)
        {
            object toReturn = NJsonConvert.DeserializeObject(value, type, converters);

            if (toReturn is IJsonPopulatedObject jo)
            {
                jo.SetRawJson(value);
            }

            return toReturn;
        }

        [DebuggerStepThrough]
        public static T DeserializeObject<T>(string value, params JsonConverter[] converters)
        {
            T toReturn = NJsonConvert.DeserializeObject<T>(value, converters);

            if (toReturn is IJsonPopulatedObject jo)
            {
                jo.SetRawJson(value);
            }

            return toReturn;
        }

        public static XmlDocument DeserializeXmlNode(string value, string deserializeRootElementName, bool writeArrayAttribute, bool encodeSpecialCharacters)
        {
            return NJsonConvert.DeserializeXmlNode(value, deserializeRootElementName, writeArrayAttribute, encodeSpecialCharacters);
        }

        public static XmlDocument DeserializeXmlNode(string value, string deserializeRootElementName, bool writeArrayAttribute)
        {
            return NJsonConvert.DeserializeXmlNode(value, deserializeRootElementName, writeArrayAttribute);
        }

        public static XmlDocument DeserializeXmlNode(string value, string deserializeRootElementName)
        {
            return NJsonConvert.DeserializeXmlNode(value, deserializeRootElementName);
        }

        public static XmlDocument DeserializeXmlNode(string value)
        {
            return NJsonConvert.DeserializeXmlNode(value);
        }

        public static XDocument DeserializeXNode(string value, string deserializeRootElementName, bool writeArrayAttribute, bool encodeSpecialCharacters)
        {
            return NJsonConvert.DeserializeXNode(value, deserializeRootElementName, writeArrayAttribute, encodeSpecialCharacters);
        }

        public static XDocument DeserializeXNode(string value)
        {
            return NJsonConvert.DeserializeXNode(value);
        }

        public static XDocument DeserializeXNode(string value, string deserializeRootElementName)
        {
            return NJsonConvert.DeserializeXNode(value, deserializeRootElementName);
        }

        public static XDocument DeserializeXNode(string value, string deserializeRootElementName, bool writeArrayAttribute)
        {
            return NJsonConvert.DeserializeXNode(value, deserializeRootElementName, writeArrayAttribute);
        }

        [DebuggerStepThrough]
        public static void PopulateObject(string value, object target)
        {
            NJsonConvert.PopulateObject(value, target);
        }

        public static void PopulateObject(string value, object target, JsonSerializerSettings settings)
        {
            NJsonConvert.PopulateObject(value, target, settings);
        }

        [DebuggerStepThrough]
        public static string SerializeObject(object value, Type type, Newtonsoft.Json.Formatting formatting, JsonSerializerSettings settings)
        {
            return NJsonConvert.SerializeObject(value, type, formatting, settings);
        }

        [DebuggerStepThrough]
        public static string SerializeObject(object value, Newtonsoft.Json.Formatting formatting, JsonSerializerSettings settings)
        {
            return NJsonConvert.SerializeObject(value, formatting, settings);
        }

        [DebuggerStepThrough]
        public static string SerializeObject(object value, JsonSerializerSettings settings)
        {
            return NJsonConvert.SerializeObject(value, settings);
        }

        [DebuggerStepThrough]
        public static string SerializeObject(object value, Newtonsoft.Json.Formatting formatting, params JsonConverter[] converters)
        {
            return NJsonConvert.SerializeObject(value, formatting, converters);
        }

        [DebuggerStepThrough]
        public static string SerializeObject(object value, params JsonConverter[] converters)
        {
            return NJsonConvert.SerializeObject(value, converters);
        }

        [DebuggerStepThrough]
        public static string SerializeObject(object value, Type type, JsonSerializerSettings settings)
        {
            return NJsonConvert.SerializeObject(value, type, settings);
        }

        [DebuggerStepThrough]
        public static string SerializeObject(object value, Newtonsoft.Json.Formatting formatting)
        {
            return NJsonConvert.SerializeObject(value, formatting);
        }

        [DebuggerStepThrough]
        public static string SerializeObject(object value)
        {
            return NJsonConvert.SerializeObject(value);
        }

        public static string SerializeXmlNode(XmlNode node, Newtonsoft.Json.Formatting formatting, bool omitRootObject)
        {
            return NJsonConvert.SerializeXmlNode(node, formatting, omitRootObject);
        }

        public static string SerializeXmlNode(XmlNode node, Newtonsoft.Json.Formatting formatting)
        {
            return NJsonConvert.SerializeXmlNode(node, formatting);
        }

        public static string SerializeXmlNode(XmlNode node)
        {
            return NJsonConvert.SerializeXmlNode(node);
        }

        public static string SerializeXNode(XObject node, Newtonsoft.Json.Formatting formatting, bool omitRootObject)
        {
            return NJsonConvert.SerializeXNode(node, formatting, omitRootObject);
        }

        public static string SerializeXNode(XObject node, Newtonsoft.Json.Formatting formatting)
        {
            return NJsonConvert.SerializeXNode(node, formatting);
        }

        public static string SerializeXNode(XObject node)
        {
            return NJsonConvert.SerializeXNode(node);
        }

        public static string ToString(object value)
        {
            return NJsonConvert.ToString(value);
        }

        public static string ToString(DateTimeOffset value)
        {
            return NJsonConvert.ToString(value);
        }

        public static string ToString(DateTimeOffset value, DateFormatHandling format)
        {
            return NJsonConvert.ToString(value, format);
        }

        public static string ToString(bool value)
        {
            return NJsonConvert.ToString(value);
        }

        public static string ToString(char value)
        {
            return NJsonConvert.ToString(value);
        }

        public static string ToString(Enum value)
        {
            return NJsonConvert.ToString(value);
        }

        public static string ToString(int value)
        {
            return NJsonConvert.ToString(value);
        }

        public static string ToString(short value)
        {
            return NJsonConvert.ToString(value);
        }

        public static string ToString(ushort value)
        {
            return NJsonConvert.ToString(value);
        }

        public static string ToString(uint value)
        {
            return NJsonConvert.ToString(value);
        }

        public static string ToString(ulong value)
        {
            return NJsonConvert.ToString(value);
        }

        public static string ToString(float value)
        {
            return NJsonConvert.ToString(value);
        }

        public static string ToString(double value)
        {
            return NJsonConvert.ToString(value);
        }

        public static string ToString(byte value)
        {
            return NJsonConvert.ToString(value);
        }

        public static string ToString(sbyte value)
        {
            return NJsonConvert.ToString(value);
        }

        public static string ToString(decimal value)
        {
            return NJsonConvert.ToString(value);
        }

        public static string ToString(Guid value)
        {
            return NJsonConvert.ToString(value);
        }

        public static string ToString(TimeSpan value)
        {
            return NJsonConvert.ToString(value);
        }

        public static string ToString(Uri value)
        {
            return NJsonConvert.ToString(value);
        }

        public static string ToString(string value)
        {
            return NJsonConvert.ToString(value);
        }

        public static string ToString(string value, char delimiter)
        {
            return NJsonConvert.ToString(value, delimiter);
        }

        public static string ToString(DateTime value)
        {
            return NJsonConvert.ToString(value);
        }

        public static string ToString(string value, char delimiter, StringEscapeHandling stringEscapeHandling)
        {
            return NJsonConvert.ToString(value, delimiter, stringEscapeHandling);
        }

        public static string ToString(long value)
        {
            return NJsonConvert.ToString(value);
        }

        public static string ToString(DateTime value, DateFormatHandling format, DateTimeZoneHandling timeZoneHandling)
        {
            return NJsonConvert.ToString(value, format, timeZoneHandling);
        }
    }
}
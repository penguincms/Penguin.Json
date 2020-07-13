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
            get
            {
                return NJsonConvert.DefaultSettings;
            }
            set
            {
                NJsonConvert.DefaultSettings = value;
            }
        }

        [DebuggerStepThrough]
        public static T DeserializeAnonymousType<T>(string value, T anonymousTypeObject) => NJsonConvert.DeserializeAnonymousType<T>(value, anonymousTypeObject);

        [DebuggerStepThrough]
        public static T DeserializeAnonymousType<T>(string value, T anonymousTypeObject, JsonSerializerSettings settings) => NJsonConvert.DeserializeAnonymousType<T>(value, anonymousTypeObject, settings);

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

        public static XmlDocument DeserializeXmlNode(string value, string deserializeRootElementName, bool writeArrayAttribute, bool encodeSpecialCharacters) => NJsonConvert.DeserializeXmlNode(value, deserializeRootElementName, writeArrayAttribute, encodeSpecialCharacters);

        public static XmlDocument DeserializeXmlNode(string value, string deserializeRootElementName, bool writeArrayAttribute) => NJsonConvert.DeserializeXmlNode(value, deserializeRootElementName, writeArrayAttribute);

        public static XmlDocument DeserializeXmlNode(string value, string deserializeRootElementName) => NJsonConvert.DeserializeXmlNode(value, deserializeRootElementName);

        public static XmlDocument DeserializeXmlNode(string value) => NJsonConvert.DeserializeXmlNode(value);

        public static XDocument DeserializeXNode(string value, string deserializeRootElementName, bool writeArrayAttribute, bool encodeSpecialCharacters) => NJsonConvert.DeserializeXNode(value, deserializeRootElementName, writeArrayAttribute, encodeSpecialCharacters);

        public static XDocument DeserializeXNode(string value) => NJsonConvert.DeserializeXNode(value);

        public static XDocument DeserializeXNode(string value, string deserializeRootElementName) => NJsonConvert.DeserializeXNode(value, deserializeRootElementName);

        public static XDocument DeserializeXNode(string value, string deserializeRootElementName, bool writeArrayAttribute) => NJsonConvert.DeserializeXNode(value, deserializeRootElementName, writeArrayAttribute);

        [DebuggerStepThrough]
        public static void PopulateObject(string value, object target) => NJsonConvert.PopulateObject(value, target);

        public static void PopulateObject(string value, object target, JsonSerializerSettings settings) => NJsonConvert.PopulateObject(value, target, settings);

        [DebuggerStepThrough]
        public static string SerializeObject(object value, Type type, Newtonsoft.Json.Formatting formatting, JsonSerializerSettings settings) => NJsonConvert.SerializeObject(value, type, formatting, settings);

        [DebuggerStepThrough]
        public static string SerializeObject(object value, Newtonsoft.Json.Formatting formatting, JsonSerializerSettings settings) => NJsonConvert.SerializeObject(value, formatting, settings);

        [DebuggerStepThrough]
        public static string SerializeObject(object value, JsonSerializerSettings settings) => NJsonConvert.SerializeObject(value, settings);

        [DebuggerStepThrough]
        public static string SerializeObject(object value, Newtonsoft.Json.Formatting formatting, params JsonConverter[] converters) => NJsonConvert.SerializeObject(value, formatting, converters);

        [DebuggerStepThrough]
        public static string SerializeObject(object value, params JsonConverter[] converters) => NJsonConvert.SerializeObject(value, converters);

        [DebuggerStepThrough]
        public static string SerializeObject(object value, Type type, JsonSerializerSettings settings) => NJsonConvert.SerializeObject(value, type, settings);

        [DebuggerStepThrough]
        public static string SerializeObject(object value, Newtonsoft.Json.Formatting formatting) => NJsonConvert.SerializeObject(value, formatting);

        [DebuggerStepThrough]
        public static string SerializeObject(object value) => NJsonConvert.SerializeObject(value);

        public static string SerializeXmlNode(XmlNode node, Newtonsoft.Json.Formatting formatting, bool omitRootObject) => NJsonConvert.SerializeXmlNode(node, formatting, omitRootObject);

        public static string SerializeXmlNode(XmlNode node, Newtonsoft.Json.Formatting formatting) => NJsonConvert.SerializeXmlNode(node, formatting);

        public static string SerializeXmlNode(XmlNode node) => NJsonConvert.SerializeXmlNode(node);

        public static string SerializeXNode(XObject node, Newtonsoft.Json.Formatting formatting, bool omitRootObject) => NJsonConvert.SerializeXNode(node, formatting, omitRootObject);

        public static string SerializeXNode(XObject node, Newtonsoft.Json.Formatting formatting) => NJsonConvert.SerializeXNode(node, formatting);

        public static string SerializeXNode(XObject node) => NJsonConvert.SerializeXNode(node);

        public static string ToString(object value) => NJsonConvert.ToString(value);

        public static string ToString(DateTimeOffset value) => NJsonConvert.ToString(value);

        public static string ToString(DateTimeOffset value, DateFormatHandling format) => NJsonConvert.ToString(value, format);

        public static string ToString(bool value) => NJsonConvert.ToString(value);

        public static string ToString(char value) => NJsonConvert.ToString(value);

        public static string ToString(Enum value) => NJsonConvert.ToString(value);

        public static string ToString(int value) => NJsonConvert.ToString(value);

        public static string ToString(short value) => NJsonConvert.ToString(value);

        public static string ToString(ushort value) => NJsonConvert.ToString(value);

        public static string ToString(uint value) => NJsonConvert.ToString(value);

        public static string ToString(ulong value) => NJsonConvert.ToString(value);

        public static string ToString(float value) => NJsonConvert.ToString(value);

        public static string ToString(double value) => NJsonConvert.ToString(value);

        public static string ToString(byte value) => NJsonConvert.ToString(value);

        [CLSCompliant(false)]
        public static string ToString(sbyte value) => NJsonConvert.ToString(value);

        public static string ToString(decimal value) => NJsonConvert.ToString(value);

        public static string ToString(Guid value) => NJsonConvert.ToString(value);

        public static string ToString(TimeSpan value) => NJsonConvert.ToString(value);

        public static string ToString(Uri value) => NJsonConvert.ToString(value);

        public static string ToString(string value) => NJsonConvert.ToString(value);

        public static string ToString(string value, char delimiter) => NJsonConvert.ToString(value, delimiter);

        public static string ToString(DateTime value) => NJsonConvert.ToString(value);

        public static string ToString(string value, char delimiter, StringEscapeHandling stringEscapeHandling) => NJsonConvert.ToString(value, delimiter, stringEscapeHandling);

        public static string ToString(long value) => NJsonConvert.ToString(value);

        public static string ToString(DateTime value, DateFormatHandling format, DateTimeZoneHandling timeZoneHandling) => NJsonConvert.ToString(value, format, timeZoneHandling);
    }
}
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Penguin.Json.Abstractions.Interfaces;
using System.Collections.Generic;
using System.Reflection;

namespace Penguin.Json
{
    public class JsonPopulatedPropertiesObject : IJsonPopulatedPropertiesObject
    {
        private JObject backingObject;

        public IEnumerable<string> Properties
        {
            get
            {
                List<string> BoundProperties = new();

                foreach (PropertyInfo pi in GetType().GetProperties())
                {
                    if (pi.GetCustomAttribute<JsonPropertyAttribute>() is JsonPropertyAttribute jpa)
                    {
                        BoundProperties.Add(jpa.PropertyName);

                        yield return pi.Name;
                    }
                }

                foreach (JProperty prop in backingObject.Properties())
                {
                    if (!BoundProperties.Contains(prop.Name))
                    {
                        yield return prop.Name;
                    }
                }
            }
        }

        public string RawJson
        {
            get => backingObject.ToString();
            set => backingObject = JObject.Parse(value);
        }

        /// <summary>
        /// Gets the value of a requested property, first from the hard-coded properties and then from the
        /// backing JSON
        /// </summary>
        /// <param name="propertyName">The name of the property to retrieve</param>
        /// <returns>The current value of the property</returns>
        public object GetProperty(string propertyName)
        {
            if (GetType().GetProperty(propertyName) is PropertyInfo pi && pi.GetCustomAttribute<JsonPropertyAttribute>() is not null)
            {
                return pi.GetValue(this);
            }

            JObject backingObject = JObject.Parse((this as IJsonPopulatedObject).RawJson);

            return backingObject[propertyName].ToString();
        }
    }
}
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Penguin.Json.Abstractions.Interfaces;
using System.Collections.Generic;
using System.Reflection;

namespace Penguin.Json
{
    public class JsonPopulatedObject : IJsonPopulatedObject
    {
        private JObject backingObject;

        IEnumerable<string> IJsonPopulatedObject.Properties
        {
            get
            {
                List<string> BoundProperties = new List<string>();

                foreach (PropertyInfo pi in this.GetType().GetProperties())
                {
                    if (pi.GetCustomAttribute<JsonPropertyAttribute>() is JsonPropertyAttribute jpa)
                    {
                        BoundProperties.Add(jpa.PropertyName);

                        yield return pi.Name;
                    }
                }

                foreach (JProperty prop in this.backingObject.Properties())
                {
                    if (!BoundProperties.Contains(prop.Name))
                    {
                        yield return prop.Name;
                    }
                }
            }
        }

        string IJsonPopulatedObject.RawJson
        {
            get => this.backingObject.ToString();
            set => this.backingObject = JObject.Parse(value);
        }

        /// <summary>
        /// Gets the value of a requested property, first from the hard-coded properties and then from the
        /// backing JSON
        /// </summary>
        /// <param name="propertyName">The name of the property to retrieve</param>
        /// <returns>The current value of the property</returns>
        object IJsonPopulatedObject.GetProperty(string propertyName)
        {
            if (this.GetType().GetProperty(propertyName) is PropertyInfo pi && pi.GetCustomAttribute<JsonPropertyAttribute>() is JsonPropertyAttribute jpa)
            {
                return pi.GetValue(this);
            }

            JObject backingObject = JObject.Parse((this as IJsonPopulatedObject).RawJson);

            return backingObject[propertyName].ToString();
        }
    }
}
using Newtonsoft.Json.Linq;
using Penguin.Json.Extensions;
using System;
using System.Linq;
using System.Reflection;

namespace Penguin.Json
{
    public class JPropertyMapping
    {
        public JProperty JProperty { get; set; }

        public string JsonPath
        {
            get
            {
                string toReturn = this.JProperty.Name;

                if (this.Parent != null)
                {
                    toReturn = $"{this.Parent.JsonPath}.{toReturn}";
                }

                return toReturn;
            }
        }

        public JPropertyMapping Parent { get; set; }

        public string Path
        {
            get
            {
                string toReturn = this.PropertyInfo.Name;

                if (this.Parent != null)
                {
                    toReturn = $"{this.Parent.Path}.{toReturn}";
                }

                return toReturn;
            }
        }

        public PropertyInfo PropertyInfo { get; set; }
        public object Source { get; set; }
        public object Value { get; set; }

        public JPropertyMapping(object source, JProperty jProperty)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            this.PropertyInfo = source.GetType().GetProperties().Single(p => p.GetJsonName() == jProperty.Name);

            this.Value = this.PropertyInfo.GetValue(source);
        }
    }
}
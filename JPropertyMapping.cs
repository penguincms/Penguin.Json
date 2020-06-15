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
                string toReturn = JProperty.Name;

                if (Parent != null)
                {
                    toReturn = $"{Parent.JsonPath}.{toReturn}";
                }

                return toReturn;
            }
        }

        public JPropertyMapping Parent { get; set; }

        public string Path
        {
            get
            {
                string toReturn = PropertyInfo.Name;

                if (Parent != null)
                {
                    toReturn = $"{Parent.Path}.{toReturn}";
                }

                return toReturn;
            }
        }

        public PropertyInfo PropertyInfo { get; set; }
        public object Source { get; set; }
        public object Value { get; set; }

        public JPropertyMapping(object source, JProperty jProperty, JPropertyMapping parent = null)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            PropertyInfo = source.GetType().GetProperties().Single(p => p.GetJsonName() == jProperty.Name);

            Value = PropertyInfo.GetValue(source);
        }
    }
}
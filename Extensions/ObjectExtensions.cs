using Newtonsoft.Json;
using Penguin.Json.JsonConverters;

namespace Penguin.Json.Extensions
{
    public static class ObjectExtensions
    {
        public static T JsonClone<T>(this T source)
        {
            JsonSerializerSettings serializerSettings = new()
            {
                TypeNameHandling = TypeNameHandling.All,
                ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            };

            serializerSettings.Converters.Add(new StringConvertibleConverter());

            string json = JsonConvert.SerializeObject(source, serializerSettings);

            T clone = JsonConvert.DeserializeObject<T>(json, serializerSettings);

            return clone;
        }
    }
}
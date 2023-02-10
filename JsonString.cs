using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Penguin.SystemExtensions.Abstractions.Interfaces;

namespace Penguin.Json
{
    public class JsonString : IConvertible<string>, IConvertible<JToken>
    {
        private string _value;

        public bool IsValid => Validate(_value);

        public string Value
        {
            get => IsValid ? JToken.Parse(_value).ToString(Formatting.Indented) : _value;
            set => _value = value;
        }

        public JsonString(string value)
        {
            _value = value;
        }

        public JsonString()
        {
        }

        public static implicit operator JsonString(string b)
        {
            return new JsonString(b);
        }

        public static implicit operator JsonString(JToken b)
        {
            return new JsonString(b?.ToString());
        }

        public static implicit operator JToken(JsonString d)
        {
            return (d?.IsValid ?? false) ? JToken.Parse(d) : null;
        }

        public static implicit operator string(JsonString d)
        {
            return d?.Value;
        }

        public static bool Validate(string json)
        {
            json = json?.Trim();

            if (string.IsNullOrWhiteSpace(json) || json.Length < 2)
            {
                return false;
            }

            char sChar = json[0];
            char eChar;

            switch (sChar)
            {
                case '[':
                    eChar = ']';
                    break;

                case '{':
                    eChar = '}';
                    break;

                default:
                    return false;
            }

            return json[^1] == eChar;
        }

        public string Convert()
        {
            return _value;
        }

        public void Convert(string fromT)
        {
            _value = fromT;
        }

        JToken IConvertible<JToken>.Convert()
        {
            return this;
        }

        void IConvertible<JToken>.Convert(JToken fromT)
        {
            _value = fromT.ToString();
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
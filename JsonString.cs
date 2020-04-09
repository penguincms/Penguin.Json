using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Penguin.SystemExtensions.Abstractions.Interfaces;

namespace Penguin.Json
{
    public class JsonString : IConvertible<string>, IConvertible<JToken>
    {
        private string _value;

        public string Value
        {
            get
            {
                if (this.IsValid())
                {
                    return JToken.Parse(_value).ToString(Formatting.Indented);
                }
                else
                {
                    return this._value;
                }
            }
            set => _value = value;
        }

        public JsonString(string value)
        {
            _value = value;
        }

        public JsonString()
        {
        }

        public static implicit operator JsonString(string b) => new JsonString(b);

        public static implicit operator JsonString(JToken b) => new JsonString(b.ToString());

        public static implicit operator JToken(JsonString d) => d.IsValid() ? JToken.Parse(d) : null;

        public static implicit operator string(JsonString d) => d.Value;

        public static bool IsValid(string json)
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

                default: return false;
            }

            if (json[json.Length - 1] != eChar)
            {
                return false;
            }

            return true;
        }

        public string Convert() => _value;

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

        public bool IsValid() => IsValid(this._value);

        public override string ToString()
        {
            return Value;
        }
    }
}
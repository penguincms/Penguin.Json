using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Penguin.SystemExtensions.Abstractions.Interfaces;

namespace Penguin.Json
{
    public class JsonString : IConvertible<string>, IConvertible<JToken>
    {
        private string _value;

        public bool IsValid => Validate(this._value);

        public string Value
        {
            get
            {
                if (this.IsValid)
                {
                    return JToken.Parse(this._value).ToString(Formatting.Indented);
                }
                else
                {
                    return this._value;
                }
            }
            set => this._value = value;
        }

        public JsonString(string value)
        {
            this._value = value;
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

            if (json[json.Length - 1] != eChar)
            {
                return false;
            }

            return true;
        }

        public string Convert()
        {
            return this._value;
        }

        public void Convert(string fromT)
        {
            this._value = fromT;
        }

        JToken IConvertible<JToken>.Convert()
        {
            return this;
        }

        void IConvertible<JToken>.Convert(JToken fromT)
        {
            this._value = fromT.ToString();
        }

        public override string ToString()
        {
            return this.Value;
        }
    }
}
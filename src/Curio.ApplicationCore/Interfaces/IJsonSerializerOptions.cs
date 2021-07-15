using System;
using System.Text.Json;

namespace Curio.ApplicationCore.Interfaces
{
    public interface IJsonSerializerOptions<TOptions>
    {
        TOptions Options { get; }
    }

    public class JsonSerializerOptionsWrapper : IJsonSerializerOptions<JsonSerializerOptions>
    {
        private JsonSerializerOptions jsonSerializerOptions;

        public JsonSerializerOptionsWrapper()
            : this(null)
        { }

        public JsonSerializerOptionsWrapper(JsonSerializerOptions jsonSerializerOptions)
        {
            this.jsonSerializerOptions = jsonSerializerOptions;
        }

        public JsonSerializerOptions Options
        {
            get
            {
                if (jsonSerializerOptions is null)
                    jsonSerializerOptions = GetDefaultOptions();

                return jsonSerializerOptions;
            }
            set
            {
                if (value is null)
                    throw new ApplicationException("Cannot set the options to be null.");

                jsonSerializerOptions = value;
            }
        }

        public static JsonSerializerOptions GetDefaultOptions()
        {
            var jsonSerializerOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web)
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            return jsonSerializerOptions;
        }
    }
}

using System.Text.Json;

namespace Curio.Core.Extensions
{
    public static class JsonExtensions
    {
        private static readonly JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public static T FromJson<T>(this string json) =>
            JsonSerializer.Deserialize<T>(json, options);

        public static string ToJson<T>(this T obj) =>
            JsonSerializer.Serialize<T>(obj, options);
    }
}

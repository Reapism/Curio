using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Curio.Domain.Extensions
{
    public static class JsonExtensions
    {
        private static readonly JsonSerializerOptions Options = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public static object FromJson<T>(this string json, Type type) =>
            JsonSerializer.Deserialize(json, type, Options);

        public static T FromJson<T>(this string json) =>
            JsonSerializer.Deserialize<T>(json, Options);

        public static async Task<object> FromJsonAsync<T>(this string json, Type type, CancellationToken cancellationToken = default)
        {
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(json));
            return await JsonSerializer.DeserializeAsync(stream, type, Options, cancellationToken);
        }

        public static async Task<T> FromJsonAsync<T>(this string json, CancellationToken cancellationToken = default)
        {
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(json));
            return await JsonSerializer.DeserializeAsync<T>(stream, Options, cancellationToken);
        }

        public static string ToJson<T>(this T value) =>
            JsonSerializer.Serialize<T>(value, Options);

        public static string ToJson(this object obj, Type type) =>
            JsonSerializer.Serialize(obj, type, Options);

        public static async Task<Stream> ToJsonAsync<T>(this T value, CancellationToken cancellationToken = default)
        {
            var json = ToJson(value);
            var bytes = Encoding.UTF8.GetBytes(json);
            var stream = new MemoryStream(bytes);
            await JsonSerializer.SerializeAsync<T>(stream, value, Options, cancellationToken);
            return stream;
        }

        public static async Task<Stream> ToJsonAsync(this object obj, Type type, CancellationToken cancellationToken = default)
        {
            var json = ToJson(obj);
            var bytes = Encoding.UTF8.GetBytes(json);
            var stream = new MemoryStream(bytes);
            await JsonSerializer.SerializeAsync(stream, obj, type, Options, cancellationToken);
            return stream;
        }
    }
}

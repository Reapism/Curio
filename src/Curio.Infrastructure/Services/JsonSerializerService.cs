using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Curio.ApplicationCore.Interfaces;

namespace Curio.Infrastructure.Services
{
    public class JsonSerializerService : IJsonSerializer
    {
        public T DeserializeJson<T>(Stream stream, ISerializerOptions options)
        {
            var memoryStream = new MemoryStream();
            stream.CopyTo(memoryStream);

            var jsonBytes = memoryStream.ToArray();
            var readOnlySpan = new ReadOnlySpan<byte>(jsonBytes, 0, jsonBytes.Length);
            var value = DeserializeJson<T>(readOnlySpan, options);

            return value;
        }

        public T DeserializeJson<T>(ReadOnlySpan<byte> utf8Json, ISerializerOptions options)
        {
            var value = JsonSerializer.Deserialize<T>(utf8Json, options.JsonOptions);
            return value;
        }

        public T DeserializeJson<T>(string filePath, ISerializerOptions options)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("File does not exist.", filePath);
            }

            var fileBytes = File.ReadAllBytes(filePath);
            var readOnlySpan = new ReadOnlySpan<byte>(fileBytes, 0, fileBytes.Length);
            var value = JsonSerializer.Deserialize<T>(readOnlySpan, options.JsonOptions);

            return value;
        }

        public async Task<T> DeserializeJsonAsync<T>(string filePath, ISerializerOptions options = null)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("File does not exist.", filePath);
            }

            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            var value = await DeserializeJsonAsync<T>(fileStream, options);

            return value;
        }

        public async Task<T> DeserializeJsonAsync<T>(Stream stream, ISerializerOptions options = null)
        {
            var value = await JsonSerializer.DeserializeAsync<T>(stream, options.JsonOptions);

            return value;
        }

        public void SerializeJson<T>(T value, string outputPath, ISerializerOptions options)
        {
            var jsonString = JsonSerializer.Serialize(value, options.JsonOptions);
            File.WriteAllText(outputPath, jsonString);
        }

        public ReadOnlySpan<byte> SerializeJson<T>(T value, ISerializerOptions options)
        {
            var jsonString = JsonSerializer.Serialize(value, options.JsonOptions);
            var bytes = Encoding.UTF8.GetBytes(jsonString);
            var readOnlySpan = new ReadOnlySpan<byte>(bytes, 0, bytes.Length);

            return readOnlySpan;
        }

        public async Task SerializeJsonAsync<T>(T value, string outputPath, ISerializerOptions options)
        {
            var fileStream = new FileStream(outputPath, FileMode.OpenOrCreate, FileAccess.Write);
            await JsonSerializer.SerializeAsync(fileStream, value, options.JsonOptions);
        }
    }
}

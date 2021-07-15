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
        private readonly IJsonSerializerOptions options;

        public JsonSerializerService(IJsonSerializerOptions options)
        {
            this.options = options;
        }
        public T Deserialize<T>(Stream stream)
        {
            var memoryStream = new MemoryStream();
            stream.CopyTo(memoryStream);

            var jsonBytes = memoryStream.ToArray();
            var readOnlySpan = new ReadOnlySpan<byte>(jsonBytes, 0, jsonBytes.Length);
            var value = Deserialize<T>(readOnlySpan);

            return value;
        }

        public T Deserialize<T>(ReadOnlySpan<byte> utf8Json)
        {
            var value = JsonSerializer.Deserialize<T>(utf8Json, options.Options);
            return value;
        }

        public T Deserialize<T>(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("File does not exist.", filePath);
            }

            var fileBytes = File.ReadAllBytes(filePath);
            var readOnlySpan = new ReadOnlySpan<byte>(fileBytes, 0, fileBytes.Length);
            var value = JsonSerializer.Deserialize<T>(readOnlySpan, options.Options);

            return value;
        }

        public async Task<T> DeserializeAsync<T>(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("File does not exist.", filePath);
            }

            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            var value = await DeserializeAsync<T>(fileStream);

            return value;
        }

        public async Task<T> DeserializeAsync<T>(Stream stream)
        {
            var value = await JsonSerializer.DeserializeAsync<T>(stream, options.Options);

            return value;
        }

        public void Serialize<T>(T value, string outputPath)
        {
            var jsonString = JsonSerializer.Serialize(value, options.Options);
            File.WriteAllText(outputPath, jsonString);
        }

        public ReadOnlySpan<byte> Serialize<T>(T value)
        {
            var jsonString = JsonSerializer.Serialize(value, options.Options);
            var bytes = Encoding.UTF8.GetBytes(jsonString);
            var readOnlySpan = new ReadOnlySpan<byte>(bytes, 0, bytes.Length);

            return readOnlySpan;
        }

        public async Task SerializeAsync<T>(T value, string outputPath)
        {
            var fileStream = new FileStream(outputPath, FileMode.OpenOrCreate, FileAccess.Write);
            await JsonSerializer.SerializeAsync(fileStream, value, options.Options);
        }

        public async Task<string> SerializeAsync<T>(T value)
        {
            var memoryStream = new MemoryStream();
            await JsonSerializer.SerializeAsync(memoryStream, value, options.Options);
            
            var streamReader = new StreamReader(memoryStream);
            var jsonString = await streamReader.ReadToEndAsync();

            return jsonString;
        }
    }
}

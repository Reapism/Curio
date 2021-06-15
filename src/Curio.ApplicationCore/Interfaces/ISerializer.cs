using System;
using System.IO;
using System.Threading.Tasks;

namespace Curio.ApplicationCore.Interfaces
{
    public interface IJsonSerializer
    {
        Task<T> DeserializeJsonAsync<T>(string filePath, ISerializerOptions options = null);
        Task<T> DeserializeJsonAsync<T>(Stream stream, ISerializerOptions options = null);

        T DeserializeJson<T>(Stream stream, ISerializerOptions options);
        T DeserializeJson<T>(ReadOnlySpan<byte> utf8Json, ISerializerOptions options);
        T DeserializeJson<T>(string inputPath, ISerializerOptions options);

        Task SerializeJsonAsync<T>(T value, string outputPath, ISerializerOptions options);

        void SerializeJson<T>(T value, string outputPath, ISerializerOptions options);
        ReadOnlySpan<byte> SerializeJson<T>(T value, ISerializerOptions options);
    }
}

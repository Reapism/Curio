using System;
using System.IO;
using System.Threading.Tasks;

namespace Curio.ApplicationCore.Interfaces
{
    public interface IJsonSerializer
    {
        Task<T> DeserializeAsync<T>(string filePath);
        Task<T> DeserializeAsync<T>(Stream stream);

        T Deserialize<T>(Stream stream);
        T Deserialize<T>(ReadOnlySpan<byte> utf8Json);
        T Deserialize<T>(string inputPath);

        Task SerializeAsync<T>(T value, string outputPath);
        Task<string> SerializeAsync<T>(T value);

        void Serialize<T>(T value, string outputPath);
        ReadOnlySpan<byte> Serialize<T>(T value);
    }
}

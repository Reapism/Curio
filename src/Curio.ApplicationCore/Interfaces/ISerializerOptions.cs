using System.Text.Json;

namespace Curio.ApplicationCore.Interfaces
{
    public interface ISerializerOptions
    {
        JsonSerializerOptions JsonOptions { get; }
    }
}

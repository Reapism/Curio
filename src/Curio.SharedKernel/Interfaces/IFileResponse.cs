namespace Curio.SharedKernel.Interfaces
{
    public interface IFileResponse
    {
        string FileName { get; set; }
        bool IsEmpty { get; set; }
        byte[] Data { get; set; }
    }
}

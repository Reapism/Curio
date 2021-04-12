using System.Security.Cryptography;
using System.Text;

namespace Curio.Core.Interfaces
{
    public interface IHashingService
    {
        string Hash(string value);
    }

    /// <summary>
    /// Hashes in UTF-8
    /// </summary>
    public class HashingService : IHashingService
    {
        private readonly HashAlgorithm hashAlgorithm;

        public HashingService()
        {
            hashAlgorithm = SHA512.Create();
        }

        public string Hash(string value)
        {
            var bytes = ToBytes(value);
            var hashedBytes = hashAlgorithm.ComputeHash(bytes);
            var hashedString = ToString(hashedBytes);

            return hashedString;
        }

        private byte[] ToBytes(string value)
        {
            var bytes = Encoding.UTF8.GetBytes(value);

            return bytes;
        }

        private string ToString(byte[] bytes)
        {
            var value = Encoding.UTF8.GetString(bytes);

            return value;
        }
    }
}

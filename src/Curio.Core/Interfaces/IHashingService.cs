using System.Security.Cryptography;
using Curio.Core.Extensions;

namespace Curio.Core.Interfaces
{
    public interface IHashingService
    {
        string Hash(string value);
    }

    /// <summary>
    /// Provides a service for hasing a string value in
    /// SHA512 in UTF-8.
    /// </summary>
    public class Sha512HashingService : IHashingService
    {
        private readonly HashAlgorithm hashAlgorithm;

        public Sha512HashingService()
        {
            hashAlgorithm = SHA512.Create();
        }

        public string Hash(string value)
        {
            var bytes = value.ToUtf8Bytes();
            var hashedBytes = hashAlgorithm.ComputeHash(bytes);
            var hashedString = hashedBytes.ToUtf8String();

            return hashedString;
        }
    }

    /// <summary>
    /// Provides a service for hasing a string value in
    /// SHA256 in UTF-8.
    /// </summary>
    public class Sha256HashingService : IHashingService
    {
        private readonly HashAlgorithm hashAlgorithm;

        public Sha256HashingService()
        {
            hashAlgorithm = SHA256.Create();
        }

        public string Hash(string value)
        {
            var bytes = value.ToUtf8Bytes();
            var hashedBytes = hashAlgorithm.ComputeHash(bytes);
            var hashedString = hashedBytes.ToUtf8String();

            return hashedString;
        }
    }
}

using System.Text;

namespace Curio.Core.Extensions
{
    public static class EncodingExtensions
    {
        public static byte[] ToAsciiBytes(this string value)
        {
            var bytes = Encoding.ASCII.GetBytes(value);

            return bytes;
        }

        public static string ToAsciiString(this byte[] bytes)
        {
            var value = Encoding.ASCII.GetString(bytes);

            return value;
        }

        public static byte[] ToUnicodeBytes(this string value)
        {
            var bytes = Encoding.Unicode.GetBytes(value);

            return bytes;
        }

        public static string ToUnicodeString(this byte[] bytes)
        {
            var value = Encoding.Unicode.GetString(bytes);

            return value;
        }

        public static byte[] ToUtf8Bytes(this string value)
        {
            var bytes = Encoding.UTF8.GetBytes(value);

            return bytes;
        }

        public static string ToUtf8String(this byte[] bytes)
        {
            var value = Encoding.UTF8.GetString(bytes);

            return value;
        }
    }
}

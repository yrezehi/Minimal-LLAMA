using System.Runtime.InteropServices;

namespace LLama.Extensions
{
    public static class BytesExtensions
    {
        public static long GetBytesSize<T>(this T @object) =>
            Marshal.SizeOf(@object);

        public static byte[] GetBytes<T>(this T @object) =>
            new byte[Marshal.SizeOf(@object)];
    }
}

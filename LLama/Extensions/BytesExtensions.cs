using System.Runtime.InteropServices;

namespace LLama.Extensions
{
    public static class BytesExtensions
    {
        public static long GetBytesSize(this object @object) =>
            Marshal.SizeOf(@object);

        public static byte[] GetBytes(this object @object) =>
            new byte[Marshal.SizeOf(@object)];
    }
}

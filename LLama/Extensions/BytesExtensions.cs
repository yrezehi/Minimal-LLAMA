using System.Runtime.InteropServices;

namespace LLama.Extensions
{
    public static class BytesExtensions
    {
        public static long GetBytesSize(this object @object) =>
            Marshal.SizeOf(@object);
    }
}

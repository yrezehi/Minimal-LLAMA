using System.Runtime.InteropServices;
using LLama.Configuration;
using LLama.Extensions;

namespace LLama.Native.Configuration.Native
{
	public static class ConfigurationLoader {

        public static BinConfiguration Load(string path)
        {
            using FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
            byte[] inBytes = new byte[Marshal.SizeOf(typeof(BinConfiguration))];

            if (fileStream.Read(inBytes, 0, inBytes.Length) != inBytes.Length)
            {
                throw new ArgumentException("Failed to ready the configuration!");
            }

            GCHandle handleGC = GCHandle.Alloc(inBytes, GCHandleType.Pinned);

            BinConfiguration configurationInstance;

            try {
                configurationInstance = (BinConfiguration)Marshal.PtrToStructure(handleGC.AddrOfPinnedObject(), typeof(BinConfiguration));
            } finally { handleGC.Free(); }

            configurationInstance.vocab_size = Math.Abs(configurationInstance.vocab_size);

            return configurationInstance;
        }
    }
}

using LLama.Configuration;
using LLama.Extensions;
using LLama.Memory;
using LLama.Native.Configuration.Native;

namespace LLama.Models
{
    public class ModelLoader
    {
        public static void Load(string path)
        {
            BinConfiguration configuration = ConfigurationLoader.Load(path);
            MemoryFile fileLoader = MemoryFile.Load(path, configuration.GetBytesSize());


        }
    }
}

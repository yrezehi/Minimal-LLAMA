using LLama.Memory;

namespace LLama.Models
{
    public static class InitialWeights
    {
        public static void Initialize(this MemoryFile memoryFile)
        {
            long offset;

            memoryFile.Read(ref offset);
        }
    }
}

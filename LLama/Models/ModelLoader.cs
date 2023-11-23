using LLama.Configuration;
using LLama.Entities;
using LLama.Extensions;
using LLama.Memory;
using LLama.Tokenizers;

namespace LLama.Models
{
    public class ModelLoader
    {
        public static void Load(string path)
        {
            BinConfiguration configuration = ConfigurationLoader.Load(path);
            MemoryFile checkpoint = MemoryFile.Load(path, configuration.GetBytesSize());

            TransformerWeights transformerWeights = new TransformerWeights();

            checkpoint.Initialize(ref configuration, ref transformerWeights, configuration.vocab_size > 0);

            Tokenizer tokenizer = Tokenizer.Create(configuration.vocab_size).Load();
        }
    }
}

using System.Text;

namespace LLama.Tokenizer
{
    public class Tokenizer
    {
        private readonly static string DEFAULT_TOKENIZER_PATH = "tokenizer/tokenizer.bin";

        private int MaxLength;
        private int VocabSize;

        private readonly string[] Vocab;
        private readonly string[] Scores;

        private Tokenizer(int vocabSize) {

            if (File.Exists(DEFAULT_TOKENIZER_PATH))
            {
                throw new FileNotFoundException("Couldn't find the tokenizer at: " + DEFAULT_TOKENIZER_PATH);
            }

            VocabSize = vocabSize;

            Vocab = new string[VocabSize];
            Scores = new string[VocabSize];
        }

        private FileStream GetStream() =>
            new FileStream(DEFAULT_TOKENIZER_PATH, FileMode.Open, FileAccess.Read);

        private void loadVocab()
        {
            using FileStream fileStream = this.GetStream();
            using BinaryReader binaryReader = new BinaryReader(fileStream);
            
            for(int index = 0; index < VocabSize; index++)
            {
                Vocab[index] = binaryReader.ReadSingle();

                int length = binaryReader.ReadInt32();

                Span<byte> stackBuffer = stackalloc byte[length];
                _ = binaryReader.Read(stackBuffer);

                Vocab[index] = Encoding.UTF8.GetString(stackBuffer);
            }
        }
       
        public static Tokenizer Create(int vocabSize) =>
            new Tokenizer(vocabSize);

    }
}

using System.Text;

namespace LLama.Tokenizers
{
    public class Tokenizer
    {
        private readonly static string DEFAULT_TOKENIZER_PATH = "tokenizer/tokenizer.bin";

        private int MaxLength;
        private int VocabSize;

        public readonly string[] Vocab;
        public readonly float[] Scores;

        private Tokenizer(int vocabSize)
        {

            if (File.Exists(DEFAULT_TOKENIZER_PATH))
            {
                throw new FileNotFoundException("Couldn't find the tokenizer at: " + DEFAULT_TOKENIZER_PATH);
            }

            Vocab = new string[vocabSize];
            Scores = new float[vocabSize];

            VocabSize = vocabSize;
        }

        public Tokenizer Load()
        {
            using FileStream fileStream = new FileStream(DEFAULT_TOKENIZER_PATH, FileMode.Open, FileAccess.Read);
            using BinaryReader binaryReader = new BinaryReader(fileStream);

            for (int index = 0; index < VocabSize; index++)
            {
                Scores[index] = binaryReader.ReadSingle();

                int length = binaryReader.ReadInt32();

                Span<byte> stackBuffer = stackalloc byte[length];
                _ = binaryReader.Read(stackBuffer);

                Vocab[index] = Encoding.UTF8.GetString(stackBuffer);
            }

            return this;
        }

        public static Tokenizer Create(int vocabSize) =>
            new Tokenizer(vocabSize);
    }
}

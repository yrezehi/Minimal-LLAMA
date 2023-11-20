namespace LLama.Tokenizer
{
    public class Tokenizer
    {
        private readonly static string DEFAULT_TOKENIZER_PATH = "tokenizer/tokenizer.bin";

        private readonly FileStream FileStream;
        private int MaxLength;

        private int VocabSize;

        private readonly string[] Vocab;
        private readonly string[] Scores;

        private Tokenizer(int vocabSize) {

            if (File.Exists(DEFAULT_TOKENIZER_PATH))
            {
                throw new FileNotFoundException("Couldn't find the tokenizer at: " + DEFAULT_TOKENIZER_PATH);
            }

            FileStream = this.GetStream();
            VocabSize = vocabSize;

            Vocab = new string[VocabSize];
            Scores = new string[VocabSize];
        }

        private FileStream GetStream() =>
            new FileStream(DEFAULT_TOKENIZER_PATH, FileMode.Open, FileAccess.Read);
       
        public static Tokenizer Create(int vocabSize) =>
            new Tokenizer(vocabSize);

    }
}

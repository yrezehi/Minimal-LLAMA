namespace LLama.Tokenizer
{
    public class Tokenizer
    {
        private readonly static string DEFAULT_TOKENIZER_PATH = "tokenizer/tokenizer.bin";

        public Tokenizer() {
            if (File.Exists(DEFAULT_TOKENIZER_PATH))
            {
                throw new FileNotFoundException("Couldn't find the tokenizer at: " + DEFAULT_TOKENIZER_PATH);
            }

        }
    }
}

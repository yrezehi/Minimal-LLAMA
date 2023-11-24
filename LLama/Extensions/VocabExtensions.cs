namespace LLama.Extensions
{
	public static class VocabExtensions
	{
		public static int FindVocab(this string[] vocab, string @string, int vocabSize) {
            for (int i = 0; i < vocabSize; i++)
                if (@string == vocab[i])
                    return i;
            return -1;
        }
		// vocab.TernarySearch(@string, 0, vocabize);
	}
}

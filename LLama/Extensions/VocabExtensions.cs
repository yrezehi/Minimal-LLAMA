namespace LLama.Extensions
{
	public static class VocabExtensions
	{
		public static int FindVocab(this string[] vocab, string @string) =>
			vocab.TernarySearch<string>(0, vocab.Length - 1, @string);
	}
}

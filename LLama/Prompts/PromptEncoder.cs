using LLama.Extensions;

namespace LLama.Prompts
{
	public static class PromptEncoder
	{
		public static void Encode(this string prompt, string[] vocab, int tokenLength)
		{
			int[] tokens = new int[tokenLength];

			for (int index = 0; index < prompt.Length; index++)
			{
				int vocabIndex = vocab.FindVocab(prompt.ElementAt(index).ToString());

				if (vocabIndex == -1)
				{
					throw new ArgumentException("Couldn't find vocab for: " + prompt);
				}

				tokens[tokenLength] = vocabIndex;
			}
		}
	}
}

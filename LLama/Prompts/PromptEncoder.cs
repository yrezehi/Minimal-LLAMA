using LLama.Extensions;
using System.Text;

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

		public static void MergeConsecutivePairs(string[] vocab)
		{
			StringBuilder stringBuffer = new StringBuilder();

			while (true)
			{
				float bestScore = float.MinValue;

				for (int index = 0; index < 10; index++)
				{
					stringBuffer.Clear();
					stringBuffer.Append();
				}
			}
		}
	}
}

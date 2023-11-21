using LLama.Extensions;
using LLama.Prompts.Entities;
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

		public static void MergeConsecutivePairs(string[] vocab, int[] scores)
		{
			StringBuilder stringBuffer = new StringBuilder();
			VocabScore vocabScore = new VocabScore();
			int[] tokens = new int[10];

			while (true)
			{
				float bestScore = float.MinValue;

				for (int index = 0; index < 10; index++)
				{
					var vocabBuffered = stringBuffer.CleanAppends(vocab[index], vocab[index + 1]);
					int vocabIndex = vocab.FindVocab(vocabBuffered);

					if (vocabIndex != -1 && scores[index] > bestScore)
					{
						vocabScore.VocabIndex = vocabIndex;
						vocabScore.Identifer = index;
						vocabScore.HighestScore = scores[index];
					}
				}

				if (vocabScore.VocabIndex == -1)
					break;

				tokens[vocabScore.Identifer] = vocabScore.VocabIndex;

				for (int index = vocabScore.Identifer; index < 1 - 1; index++)
				{
					tokens[index] = tokens[index + 1];
				}

				_ = tokens.Length;
			}
		}
	}
}

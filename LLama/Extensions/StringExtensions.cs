using System.Text;

namespace LLama.Extensions
{
	public static class StringExtensions
	{
		public static string CleanAppends(this StringBuilder builder, params string[] strings)
		{
			builder.Clear();

			for (int index = 0; index < strings.Length; index++)
			{
				builder.Append(strings[index]);
			}

			return builder.ToString();
		}
	}
}

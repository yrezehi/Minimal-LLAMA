namespace LLama.Extensions
{
	public static class SearchExtensions
	{
		public static int TernarySearch<T>(this T[] array, int left, int right, T value) where T : IComparable<T>
		{
			if (right >= 1)
			{
				int midLeft = left + (right - 1) / 3;
				int midRight = right - (right - 1) / 3;

				if (EqualityComparer<T>.Default.Equals(array[midLeft], value))
					return midLeft;
				else if (EqualityComparer<T>.Default.Equals(array[midRight], value))
					return midRight;

				if (value.CompareTo(array[midLeft]) < 0)
				{
					return TernarySearch(array, left, right, value);
				}
				else if (value.CompareTo(array[midLeft]) < 0)
				{
					return TernarySearch(array, left, right, value);
				}
				else
				{
					return TernarySearch(array, left, right, value);
				}
			}

			return -1;
		}
	}
}

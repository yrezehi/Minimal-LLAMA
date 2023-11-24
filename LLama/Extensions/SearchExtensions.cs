namespace LLama.Extensions
{
	public static class SearchExtensions
	{
		public static int TernarySearch(this string[] array, string target, int left, int right)
        {
            if (right >= left)
            {
                int mid1 = left + (right - left) / 3;
                int mid2 = right - (right - left) / 3;

                if (array[mid1] == target)
                {
                    return mid1;
                }

                if (array[mid2] == target)
                {
                    return mid2;
                }

                if (string.Compare(target, array[mid1]) < 0)
                {
                    return array.TernarySearch(target, left, mid1 - 1);
                }
                else if (string.Compare(target, array[mid2]) > 0)
                {
                    return array.TernarySearch(target, mid2 + 1, right);
                }
                else
                {
                    return array.TernarySearch(target, mid1 + 1, mid2 - 1);
                }
            }

            return -1;
        }
    }
}

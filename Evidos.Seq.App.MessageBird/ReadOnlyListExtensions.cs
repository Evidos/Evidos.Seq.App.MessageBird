using System.Collections.Generic;

namespace System.Linq
{
	public static class ReadOnlyListExtensions
	{
		public static IReadOnlyList<T> ToReadOnlyList<T>(this IEnumerable<T> list)
		{
			IReadOnlyList<T> readonlyList = list as IReadOnlyList<T>;

			if (readonlyList != null)
			{
				return readonlyList;
			}

			return list.ToList().AsReadOnly();
		}
	}
}

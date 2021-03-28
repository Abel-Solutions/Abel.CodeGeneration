using System;
using System.Collections.Generic;
using System.Linq;

namespace MetaCode.Extensions
{
	public static class EnumerableExtensions
	{
		public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action) =>
			enumerable.ToList().ForEach(action);
	}
}

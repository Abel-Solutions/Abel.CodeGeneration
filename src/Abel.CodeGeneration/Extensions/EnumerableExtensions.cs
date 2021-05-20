using System;
using System.Collections.Generic;
using System.Linq;

namespace Abel.CodeGeneration.Extensions
{
	public static class EnumerableExtensions
	{
		public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action) =>
			enumerable.ToList().ForEach(action);

		public static void AddRange<T>(this IList<T> list, IEnumerable<T> enumerable) =>
			enumerable.ForEach(list.Add);
	}
}

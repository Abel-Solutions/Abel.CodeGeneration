using System.Linq;
using System.Reflection;

namespace MetaCode.Extensions
{
	public static class AssemblyExtensions
	{
		public static void Run(this Assembly assembly)
		{
			var mainClass = assembly.ExportedTypes.Single(t => t.GetMethod("Main") != null);
			mainClass.GetMethod("Main").Invoke(mainClass, null);
		}
	}
}

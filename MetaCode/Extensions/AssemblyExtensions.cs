﻿using System.Reflection;

namespace MetaCode.Extensions
{
	public static class AssemblyExtensions
	{
		public static void Execute(this Assembly assembly, string[] args = null) => 
			assembly.EntryPoint.Invoke(null, args);
	}
}

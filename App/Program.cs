using System;
using MetaCode;
using MetaCode.Extensions;
using MetaCode.Interfaces;
using Microsoft.CodeAnalysis;

namespace App
{
	public class Program
	{
		public static void Main()
		{
			// todo DI
			ICodeGen codeGen = new CodeGen();
			ICompiler compiler = new Compiler();

			var code = codeGen
				.AddLine("using System;")
				.AddLine("using System.Text;")
				.AddLine()
				.AddScoped("namespace MetaCode", n =>
				{
					n.AddScoped("public class Lol", c =>
					{
						c.AddScoped("public static void Main()", f =>
						{
							f.AddLine("Console.WriteLine(\"foo\");")
							.AddLine("Console.WriteLine(\"bar\");")
							.AddLine("Luls();");
						})
						.AddLine()
						.AddScoped("public static void Luls()", f =>
						{
							f.AddLine("Console.WriteLine(\"luls\");");
						});
					});
				})
				.Generate();

			Console.WriteLine(code);

			compiler
				.Compile(code, OutputKind.ConsoleApplication)
				.Execute();
		}
	}
}

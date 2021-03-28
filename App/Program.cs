using System;
using MetaCode;
using MetaCode.DynamicRun;

namespace App
{
	public class Program
	{
		public static void Main()
		{
			// todo DI
			ICodeGen codeGen = new CodeGen();
			var runner = new Runner();
			var compiler = new Compiler();
			ICodeRunner codeRunner = new CodeRunner(compiler, runner);
			
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

			codeRunner.Run(code);
		}
	}
}

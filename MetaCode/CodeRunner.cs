using MetaCode.DynamicRun;

namespace MetaCode
{
	public class CodeRunner : ICodeRunner
	{
		private readonly Compiler _compiler;
		private readonly Runner _runner;

		public CodeRunner(Compiler compiler, Runner runner)
		{
			_compiler = compiler;
			_runner = runner;
		}

		public void Run(string code)
		{
			//var assembly = _compiler.Compile(code);
			//assembly.ExportedTypes
			//_runner.Execute(bytes);
		}
	}
}

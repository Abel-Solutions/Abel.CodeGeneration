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
			var bytes = _compiler.Compile(code);
			_runner.Execute(bytes);
		}
	}
}

using System;
using System.Text;
using Abel.MetaCode.Interfaces;

namespace Abel.MetaCode
{
	public class ClassGenerator : CodeGenerator, IClassGenerator
	{
		private readonly string _name;

		public ClassGenerator(string name, StringBuilder sb)
			: base(sb) =>
			_name = name;

		public new IClassGenerator AddLine() =>
			(IClassGenerator)base.AddLine();

		public new IClassGenerator AddLine(string line) =>
			(IClassGenerator)base.AddLine(line);

		public IClassGenerator AddConstructor(Action<ICodeGenerator> action) =>
			(IClassGenerator)AddScoped($"public {_name}()", action);

		public IWithConstructor AddConstructor() =>
			new WithConstructor(_name, this);

		public IWithMethod AddMethod(string methodName) =>
			new WithMethod(methodName, this);
	}
}

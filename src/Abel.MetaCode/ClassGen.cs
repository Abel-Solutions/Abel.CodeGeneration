using System;
using System.Text;
using Abel.MetaCode.Interfaces;

namespace Abel.MetaCode
{
	public class ClassGen : CodeGen, IClassGen
	{
		private readonly string _name;

		public ClassGen(string name, StringBuilder sb)
			: base(sb) =>
			_name = name;

		public new IClassGen AddLine() => (IClassGen)base.AddLine();

		public new IClassGen AddLine(string line) => (IClassGen)base.AddLine(line);

		public IClassGen AddConstructor(Action<ICodeGen> action) =>
			(IClassGen)AddScoped($"public {_name}()", action);

		public IWithConstructor AddConstructor() =>
			new WithConstructor(_name, this);

		public IWithMethod AddMethod(string methodName) =>
			new WithMethod(methodName, this);
	}
}

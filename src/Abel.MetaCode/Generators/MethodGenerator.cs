using Abel.MetaCode.Interfaces;

namespace Abel.MetaCode.Generators
{
	public class MethodGenerator : Generator<IMethodGenerator>, IMethodGenerator
	{
		public MethodGenerator(ICodeWriter codeWriter)
			: base(codeWriter)
		{
		}
	}
}

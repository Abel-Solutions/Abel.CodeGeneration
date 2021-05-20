using Abel.CodeGeneration.Interfaces;

namespace Abel.CodeGeneration.Generators
{
	public class MethodGenerator : Generator<IMethodGenerator>, IMethodGenerator
	{
		internal MethodGenerator(ICodeWriter codeWriter)
			: base(codeWriter)
		{
		}
	}
}

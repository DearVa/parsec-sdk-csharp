using CppSharp.AST;

namespace ParsecSharp.AutoGen;

public static class CppSharpExtension
{
	public static Class GetClass(this Method method)
	{
		return (Class)method.Namespace;
	}
}
# Abel.MetaCode

Abel.MetaCode is a suite of tools for generating, compiling, executing and mocking classes.

## Code generation

Generate code with a fluent code builder.

Simple console app which prints "Hello world":

~~~
var code = new CodeGenerator()
	.Using("System")
	.AddClass("Lol", cl => cl
		.AddMethod("Main")
			.WithModifiers("public static")
			.WithContent(method => method
				.AddLine("Console.WriteLine(\"Hello world\");")))
	.Generate();
~~~

A more advanced example with namespace and explicit modifiers and parameters on contructor and method:

~~~
var code = new CodeGenerator()
	.Using("System")
	.Using("System.Text")
	.AddLine()
	.AddNamespace("MetaCode", nspace => nspace
		.AddClass("Lol")
			.WithParent("object")
			.WithContent(cl => cl
				.AddConstructor()
					.WithParameters("string lol")
					.WithContent(ctor => ctor
						.AddLine("Console.WriteLine(lol);"))
				.AddMethod("Main")
					.WithModifiers("public static")
					.WithContent(method => method
						.AddLine("Console.WriteLine(\"foo\");"))))
	.Generate();
~~~

## Run code at run-time

~~~
new Compiler()
	.Compile(code, OutputKind.ConsoleApplication)
	.Execute();
~~~

## Mock classes

The Mocker class works similar to Moq and demonstrates both code generation and using the compiled code at run-time.

This example shows it in action, together with FluentAssertions: 

~~~
var mock = new Mocker<ISomething>();
mock.Setup(m => m.ToString(2), "2");
mock.Setup(m => m.GetDouble(2), 4);

mock.Object.ToString(2).Should().Be("2");
mock.Object.GetDouble(2).Should().Be(4);
~~~
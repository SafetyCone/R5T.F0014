using System;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.T0132;
using R5T.T0133;
using R5T.T0134;


namespace R5T.F0014
{
	[FunctionalityMarker]
	public interface IClassGenerator : IFunctionalityMarker
	{
		public CompilationRequirements<ClassDeclarationSyntax> CreateClass(
			string className)
		{
			var @class = Instances.SyntaxGenerator.Class(className);

			var output = CompilationRequirements.From(@class);
			return output;
		}

		public CompilationRequirements<ClassDeclarationSyntax> CreatePublicStaticClass(
			string className)
        {
			var @class = this.CreateClass(className)
				.Modify(x => x
					.MakePublic()
					.MakeStatic());

			return @class;
        }

		public CompilationRequirements<NamespaceDeclarationSyntax> CreateNamespace(
			string namespaceName)
        {
			var @namespace = Instances.SyntaxGenerator.Namespace(namespaceName);

			var output = CompilationRequirements.From(@namespace);
			return output;
		}

		public CompilationRequirements<NamespaceDeclarationSyntax> CreateClass(
			string namespaceName,
			string className,
			out ISyntaxNodeAnnotation<ClassDeclarationSyntax> classAnnotation)
		{
			var @namespace = this.CreateNamespace(namespaceName);

			var @class = this.CreateClass(className);

			@namespace = @namespace.Add(
				@class,
				(xNamespace, xClass) => xNamespace.AddMembers(xClass
					.PrependNewLine()
					.Indent(
						Instances.IndentationGenerator.ForClass())),
				out classAnnotation);

			return @namespace;
		}

		public CompilationRequirements<NamespaceDeclarationSyntax> CreatePublicStaticClass(
			string namespaceName,
			string className,
			out ISyntaxNodeAnnotation<ClassDeclarationSyntax> classAnnotation)
		{
			var @class = this.CreateClass(
				namespaceName,
				className,
				out classAnnotation);

			// Temp variable for using in anonymous method.
			var tempClassAnnotation = classAnnotation;

			var output = @class.Modify(
				@namespace => tempClassAnnotation.Modify(
					@namespace,
					xClass => xClass
						.MakePublic()
						.MakeStatic()));

			return output;
		}
	}
}
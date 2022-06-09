using System;
using System.Linq;

using Microsoft.CodeAnalysis;

using R5T.T0133;
using R5T.T0134;


namespace System
{
    public static class CompilationRequirementsExtensions
    {
        public static CompilationRequirements<TNode> Add<TNode, TDescendantNode>(this CompilationRequirements<TNode> node,
            CompilationRequirements<TDescendantNode> descendantNode,
            Func<TNode, TDescendantNode, TNode> add,
            out ISyntaxNodeAnnotation<TDescendantNode> descendantAnnotation)
            where TNode : SyntaxNode
            where TDescendantNode : SyntaxNode
        {
            var modifiedNode = add(
                node.Node,
                descendantNode.Node.Annotate(out descendantAnnotation));

            var output = new CompilationRequirements<TNode>(
                modifiedNode,
                node.NamespaceNames.Append(descendantNode.NamespaceNames).Distinct(),
                node.NameAliases.Append(descendantNode.NameAliases).Distinct(),
                node.ProjectReferenceFilePaths.Append(descendantNode.ProjectReferenceFilePaths).Distinct());

            return output;
        }
    }
}

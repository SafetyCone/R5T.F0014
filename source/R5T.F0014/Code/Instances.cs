using System;

using R5T.F0007;


namespace R5T.F0014
{
    public static class Instances
    {
        public static IIndentationGenerator IndentationGenerator { get; } = F0007.IndentationGenerator.Instance;
        public static ISyntaxGenerator SyntaxGenerator { get; } = F0007.SyntaxGenerator.Instance;
    }
}

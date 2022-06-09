using System;


namespace R5T.F0014
{
	public class ClassGenerator : IClassGenerator
	{
    	#region Infrastructure

	    public static ClassGenerator Instance { get; } = new();

	    private ClassGenerator()
	    {
	    }

    	#endregion
	}
}
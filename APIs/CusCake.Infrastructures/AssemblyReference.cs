using System.Reflection;

namespace CusCake.Infrastructures
{
    public class AssemblyReference
    {
        public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
    }
}

using System;
using System.IO;
using System.Reflection;
using System.Runtime.Loader;
using TypeSupport;
using Xunit;

namespace NamespaceA
{
    public class ClassA
    {
        public int A = 1;
        public class ClassB
        {
            public int b = 1;
            public class ClassC
            {
                public int c = 1;
            }
        }
        public enum EnumD
        {
            E,
            F
        }
    }

}
namespace TypeSupportTests
{

    public class GlobalTypeMapTests
    {
        [Theory]
        [InlineData("int", typeof(int))]
        [InlineData("float", typeof(float))]
        [InlineData("string",typeof(string))]
        public void AliasTests(string s0, Type t0)
        {
            var d = new GlobalTypeMap();
            Assert.Equal(t0, d.FindType(s0));
        }
        [Theory]
        [InlineData("Int32", typeof(int))]
        [InlineData("Single", typeof(float))]
        [InlineData("String", typeof(string))]
        public void TypeCodeTests(string s0, Type t0)
        {
            var d = new GlobalTypeMap();
            Assert.Equal(t0, d.FindType(s0));
        }
        [Theory]
        [InlineData("System.Int32", typeof(int))]
        [InlineData("System.Single", typeof(float))]
        [InlineData("System.String", typeof(string))]
        [InlineData("System.IO.Path", typeof(Path))]
        public void FullNameTests(string s0, Type t0)
        {
            var d = new GlobalTypeMap();
            Assert.Equal(t0, d.FindType(s0));
        }
        [Theory]
        [InlineData("NamespaceA.ClassA", typeof(NamespaceA.ClassA))]
        [InlineData("NamespaceA.ClassA+EnumD", typeof(NamespaceA.ClassA.EnumD))]
        [InlineData("NamespaceA.ClassA+ClassB", typeof(NamespaceA.ClassA.ClassB))]
        [InlineData("NamespaceA.ClassA+ClassB+ClassC", typeof(NamespaceA.ClassA.ClassB.ClassC))]
        public void LocalTypeTests(string s0, Type t0)
        {
            var d = new GlobalTypeMap();
            Assert.Equal(t0, d.FindType(s0));
        }
#if DEBUG
        private const string AssemblyFolder = @"..\..\..\..\TypeSupportExamples\bin\Debug\net5.0";
#else
        private const string AssemblyFolder = @"..\..\..\..\TypeSupportExamples\bin\Release\net5.0";
#endif
        [Theory]
        [InlineData("NamespaceB1.ClassA, AssemblyB", @"AssemblyB.dll")]
        public void AssemblyQualifiedTypeTests(string s0, string p0)
        {
            var d = new GlobalTypeMap();
            Type t0 = d.FindType(s0);
            Assert.Null(t0);
            string afolder = Path.Combine(Directory.GetCurrentDirectory(), AssemblyFolder);
            string p0a = Path.Combine(afolder, p0);
            Assert.True(File.Exists(p0a));
            AssemblyLoadContext.Default.Resolving += Default_Resolving;
            Assembly a1 = AssemblyLoadContext.Default.LoadFromAssemblyPath(p0a);
            t0 = d.FindType(s0);
            Type t1 = Type.GetType(s0);
            Assert.Equal(s0, TypeSupport.TypeSupport.FormatAssemblyQualifiedName(t0));
            Assert.NotNull(t1);
        }

        private Assembly Default_Resolving(AssemblyLoadContext arg1, AssemblyName arg2)
        {
            string afolder = Path.Combine(Directory.GetCurrentDirectory(), AssemblyFolder);
            string apath = Path.Combine(afolder, arg2.Name + ".dll");
            if(File.Exists(apath))
            {
                return arg1.LoadFromAssemblyPath(apath);
            }
            return null;
        }
    }
}

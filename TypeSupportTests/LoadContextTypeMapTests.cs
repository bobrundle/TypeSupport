using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;
using TypeSupport;
using Xunit;

namespace TypeSupportTests
{
    public class LoadContextTypeMapTests
    {
#if DEBUG
        private const string AssemblyFolder = @"..\..\..\..\TypeSupportExamples\bin\Debug\net5.0";
#else
        private const string AssemblyFolder = @"..\..\..\..\TypeSupportExamples\bin\Release\net5.0";
#endif
        [Fact]
        public void CtorTest()
        {
            AssemblyLoadContext alc = new AssemblyLoadContext("alc");
            string apath = Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), AssemblyFolder), "AssemblyA.dll");
            var d = new LoadContextTypeMap(alc);
            Assembly a = alc.LoadFromAssemblyPath(apath);
            Assert.Empty(d);
        }
        [Fact]
        public void GetTypesTest()
        {
            AssemblyLoadContext alc = new AssemblyLoadContext("alc");
            alc.Resolving += Alc_Resolving;
            string apath = Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), AssemblyFolder), "AssemblyA.dll");
            var d = new LoadContextTypeMap(alc);
            Assembly a0 = alc.LoadFromAssemblyPath(apath);
            Assembly a1 = d.FindAssembly("AssemblyA");
            Assert.Equal(a0.FullName, a1.FullName);
            Assembly[] aa = d.FindAssemblies("AssemblyA");
            Assert.Single(aa);
            Type[] nst1 = d.GetNamespaceTypes("ClassAInterface");
            Assert.Single(nst1);
            Type[] at1 = d.GetAssemblyTypes("AssemblyA");
            Assert.Equal(2, at1.Length);
        }

        private Assembly Alc_Resolving(AssemblyLoadContext arg1, AssemblyName arg2)
        {
            string apath = Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), AssemblyFolder), arg2.Name + ".dll");
            return arg1.LoadFromAssemblyPath(apath);
        }
    }
}

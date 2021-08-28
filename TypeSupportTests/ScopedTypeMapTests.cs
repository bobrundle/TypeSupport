using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypeSupport;
using Xunit;

namespace NamespaceA
{
    public class ClassD
    {
        public int c = 1;
    }
}

namespace TypeSupportTests
{
    public class ScopedTypeMapTests
    {
        [Theory]
        [InlineData("NamespaceA.ClassD", "", typeof(NamespaceA.ClassD))]
        [InlineData("ClassD", "NamespaceA", typeof(NamespaceA.ClassD))]
        public void UsingNamespaceTest(string tn0, string nn0, Type t0)
        {
            GlobalTypeMap g = new GlobalTypeMap();
            ScopedTypeMap u = new ScopedTypeMap(g);
            u.UsingNamespace(nn0);
            Type t1 = u.FindType(tn0);
            Assert.Equal(t0, t1);
        }
        [Theory]
        [InlineData("NamespaceA.ClassD","TypeSupportTests", typeof(NamespaceA.ClassD))]
        [InlineData("ClassD", "TypeSupportTests", typeof(NamespaceA.ClassD))]
        public void UsingAssemblyTest(string tn0, string an0, Type t0)
        {
            GlobalTypeMap g = new GlobalTypeMap();
            ScopedTypeMap u = new ScopedTypeMap(g);
            u.UsingAssembly(an0);
            Type t1 = u.FindType(tn0);
            Assert.Equal(t0, t1);
        }
    }
}

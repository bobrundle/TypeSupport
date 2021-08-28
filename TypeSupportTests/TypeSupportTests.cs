using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TypeSupportTests
{
    public class ClassE
    {
        int a = 1;
    }

    public class TypeSupportTests
    {
        [Theory]
        [InlineData(typeof(ClassE),"TypeSupportTests.ClassE, TypeSupportTests")]
        [InlineData(null, "")]
        public void FormatAssemblyQualifiedNameTests(Type t0, string s0)
        {
            string s1 = TypeSupport.TypeSupport.FormatAssemblyQualifiedName(t0);
            Assert.Equal(s0, s1);
        }
        [Theory]
        [InlineData(typeof(ClassE), "TypeSupportTests.ClassE", "TypeSupportTests")]
        [InlineData(null,"","")]
        public void ParseAssemblyQualifiedName(Type t0, string tn0, string an0)
        {
            (string tn1, string an1) = TypeSupport.TypeSupport.ParseAssemblyQualifiedName(t0 == null ? null : t0.AssemblyQualifiedName);
            Assert.Equal(tn0, tn1);
            Assert.Equal(an0, an1);
        }
    }
}

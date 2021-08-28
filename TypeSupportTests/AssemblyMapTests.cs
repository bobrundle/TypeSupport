using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TypeSupport;
using Xunit;

namespace TypeSupportTests
{
    public class AssemblyMapTests
    {
        [Fact]
        public void CtorTest()
        {
            var d = new AssemblyMap();
            Assert.Empty(d);
            Assert.False(d.IsReadOnly);
            Assert.Empty(d.Keys);
            Assert.Empty(d.Values);
        }
        [Fact]
        public void IDictionaryTest()
        {
            var d = new AssemblyMap();
            Assembly a01 = Assembly.GetExecutingAssembly();
            Assembly a02 = typeof(AssemblyMap).Assembly;
            string an01 = a01.GetName().Name;
            string an02 = a02.GetName().Name;
            var tm0 = new AssemblyMapEntry("", null);
            Assert.Null(tm0.Assembly);
            d[an01] = new AssemblyMapEntry(an01, a01);
            var e1 = d[an01];
            Assert.NotNull(e1);
            Assert.Equal(an01, e1.Name);
            Assert.Equal(a01.FullName, e1.Assembly.FullName);
            var e0 = new AssemblyMapEntry(an02, a02);
            d.Add(an02, e0);
            Assert.True(d.ContainsKey(an02));
            Assert.True(d.TryGetValue(an02, out AssemblyMapEntry e2));
            Assert.Equal(2, d.Count);
            Assert.True(d.Remove(an02));
            d.Add(new KeyValuePair<string, AssemblyMapEntry>(an02, e0));
            Assert.True(d.Contains(new KeyValuePair<string, AssemblyMapEntry>(an02, e0)));
            KeyValuePair<string, AssemblyMapEntry>[] es = new KeyValuePair<string, AssemblyMapEntry>[2];
            d.CopyTo(es, 0);
            Array.Sort(es, (x, y) => String.Compare(x.Key, y.Key));
            Assert.Equal(an02, es[0].Key);
            Assert.True(d.Remove(new KeyValuePair<string, AssemblyMapEntry>(an02, e0)));
            foreach (var e in d)
            {
                Assert.Equal(an01, e.Key);
            }
            d.Clear();
            Assert.Empty(d);
        }
    }
}

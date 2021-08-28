using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypeSupport;
using Xunit;

namespace TypeSupportTests
{
    public class TypeMapTests
    {
        [Fact]
        public void CtorTest()
        {
            var d = new TypeMap();
            Assert.Empty(d);
            Assert.False(d.IsReadOnly);
            Assert.Empty(d.Keys);
            Assert.Empty(d.Values);
        }
        [Fact]
        public void IDictionaryTest()
        {
            var d = new TypeMap();
            string tn01 = "int";
            string tn02 = "float";
            var tm0 = new TypeMapEntry("", null);
            Assert.Null(tm0.Type);
            d[tn01] = new TypeMapEntry(tn01, typeof(int));
            var e1 = d[tn01];
            Assert.NotNull(e1);
            Assert.Equal(tn01, e1.Name);
            Assert.Equal(typeof(int), e1.Type);
            var e0 = new TypeMapEntry(tn02, typeof(float));
            d.Add(tn02, e0);
            Assert.True(d.ContainsKey(tn02));
            Assert.True(d.TryGetValue(tn02, out TypeMapEntry e2));
            Assert.Equal(2, d.Count);
            Assert.True(d.Remove(tn02));
            d.Add(new KeyValuePair<string,TypeMapEntry>(tn02, e0));
            Assert.True(d.Contains(new KeyValuePair<string, TypeMapEntry>(tn02, e0)));
            KeyValuePair<string,TypeMapEntry>[] es = new KeyValuePair<string, TypeMapEntry>[2];
            d.CopyTo(es, 0);
            Array.Sort(es, (x, y) => String.Compare(x.Key, y.Key));
            Assert.Equal(tn02, es[0].Key);
            Assert.True(d.Remove(new KeyValuePair<string, TypeMapEntry>(tn02, e0)));
            foreach(var e in d)
            {
                Assert.Equal(tn01, e.Key);
            }
            d.Clear();
            Assert.Empty(d);
        }
    }
}

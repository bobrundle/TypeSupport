using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TypeSupport
{
    public class ScopedTypeMap : TypeMap
    {
        GlobalTypeMap _parent = null;
        public ScopedTypeMap(GlobalTypeMap parent)
        {
            if (parent == null) throw new ArgumentNullException("parent");
            _parent = parent;
        }
        public void UsingAssembly(string name)
        {
            Assembly a = _parent.FindAssembly(name);
            AddAssembly(a, true);
        }
        public void UsingAssembly(Assembly a)
        {
            AddAssembly(a, true);
        }
        public void UsingNamespace(string name)
        {
            Type[] ts = _parent.GetNamespaceTypes(name);
            foreach(Type t in ts)
            {
                AddType(t.Name, t);
            }
        }
        public override Type FindType(string name)
        {
            Type t = base.FindType(name);
            if(t == null)
            {
                t = _parent.FindType(name);
            }
            return t;
        }
        public override Type[] FindTypes(string name)
        {
            List<Type> ts = new List<Type>();
            ts.AddRange(base.FindTypes(name));
            ts.AddRange(_parent.FindTypes(name));
            return ts.ToArray();
        }
    }
}

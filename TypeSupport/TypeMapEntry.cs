using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;

namespace TypeSupport
{
    public class TypeMapEntry
    {
        public TypeMapEntry(string name, Type type)
        {
            Name = name;
            Types.Add(type);
        }
        public string Name { get; } 
        public List<Type> Types = new List<Type>();
        public Type Type => Types.Count > 0 ? Types[0] : null;

        internal Type GetType(string assemblyName)
        {
            foreach (var t in Types)
            {
                if (t.Assembly.GetName().Name == assemblyName)
                {
                    return t;
                }
            }
            return null;
        }
    }
}

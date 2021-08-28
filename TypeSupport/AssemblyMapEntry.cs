using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TypeSupport
{
    public class AssemblyMapEntry
    {
        public AssemblyMapEntry(string name, Assembly a)
        {
            Name = name;
            Assemblies.Add(a);
        }
        public string Name { get;  }
        public List<Assembly> Assemblies = new List<Assembly>();

        public Assembly Assembly => Assemblies.Count > 0 ? Assemblies[0] : null;

        public Assembly GetAssembly(string fullName)
        {
            foreach(Assembly a in Assemblies)
            {
                if(a.FullName == fullName)
                {
                    return a;
                }
            }
            return null;
        }
    }
}

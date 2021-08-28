using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;

namespace TypeSupport
{
    class AssemblyComparer : Comparer<Assembly>
    {
        public override int Compare(Assembly x, Assembly y)
        {
            if (x == null)
                return y == null ? 0 : 1;
            else if (y == null)
                return -1;
            else if (x.FullName == y.FullName)
            {
                var alcx = AssemblyLoadContext.GetLoadContext(x);
                var alcy = AssemblyLoadContext.GetLoadContext(y);
                if (alcx == alcy)
                {
                    return 0;
                }
                else if (AssemblyLoadContext.Default == alcx)
                {
                    return -1;
                }
                else if (AssemblyLoadContext.Default == alcy)
                {
                    return 1;
                }
                else
                {
                    return String.Compare(alcx.Name, alcy.Name);
                }
            }
            else
            {
                return String.Compare(x.FullName, y.FullName);
            }
        }
    }
}

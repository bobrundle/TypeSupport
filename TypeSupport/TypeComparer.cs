using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;

namespace TypeSupport
{
    public class TypeComparer : Comparer<Type>
    {
        public override int Compare(Type x, Type y)
        {
            if (x == null)
                return y == null ? 0 : 1;
            else if (y == null)
                return -1;
            else if(x.FullName == y.FullName)
            {
                if(x.Assembly.FullName == y.Assembly.FullName)
                {
                    var alcx = AssemblyLoadContext.GetLoadContext(x.Assembly);
                    var alcy = AssemblyLoadContext.GetLoadContext(y.Assembly);
                    if(alcx == alcy)
                    {
                        return 0;
                    }
                    else if(AssemblyLoadContext.Default == alcx)
                    {
                        return -1;
                    }
                    else if(AssemblyLoadContext.Default == alcy)
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
                    return String.Compare(x.Assembly.FullName, y.Assembly.FullName);
                }
            }
            else
            {
                return String.Compare(x.FullName, y.FullName);
            }
        }
    }
}

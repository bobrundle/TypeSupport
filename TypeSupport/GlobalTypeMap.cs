using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;

namespace TypeSupport
{
    public class GlobalTypeMap : LoadContextTypeMap
    {
        public GlobalTypeMap() : base(null)
        {
            AddTypeCodes();
            AddBuiltinTypes();
            foreach (var lc in AssemblyLoadContext.All)
            {
                try
                {
                    AddLoadContext(lc);
                }
                catch(Exception ex)
                {
                    Trace.WriteLine($"Exception adding load context {lc.Name}: {ex.Message}");
                }
            }
        }

    }
}

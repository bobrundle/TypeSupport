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
    public class LoadContextTypeMap : TypeMap
    {
        private AssemblyMap _assemblyMap = new AssemblyMap();
        private TypeMap _assemblyTypeMap = new TypeMap();
        private TypeMap _namespaceTypeMap = new TypeMap();
        private AssemblyLoadContext _loadContext = null;
        public LoadContextTypeMap(AssemblyLoadContext loadContext)
        {
            _loadContext = loadContext;
            if (_loadContext != null)
            {
                AddLoadContext(loadContext);
            }
            AppDomain.CurrentDomain.AssemblyLoad += CurrentDomain_AssemblyLoad;
        }
        public override void AddType(string name, Type type)
        {
            if(type != null)
            {
                string aname = type.Assembly.GetName().Name;
                _assemblyMap.AddAssembly(aname, type.Assembly);
                _assemblyTypeMap.AddType(aname, type);
                _namespaceTypeMap.AddType(TypeSupport.GetNameSpace(type), type);
                base.AddType(name, type);
            }
        }
        private void CurrentDomain_AssemblyLoad(object sender, AssemblyLoadEventArgs args)
        {
            try
            {
                if (_loadContext == null || _loadContext == AssemblyLoadContext.GetLoadContext(args.LoadedAssembly))
                {
                    AddAssembly(args.LoadedAssembly, false);
                }
            } catch(Exception ex)
            {
                Trace.WriteLine("Exception in assembly load: " + ex.Message);
            }
        }
        public Type[] GetNamespaceTypes(string name)
        {
            return _namespaceTypeMap.FindTypes(name);
        }
        public Type[] GetAssemblyTypes(Assembly a)
        {
            return a != null ? GetAssemblyTypes(a.GetName().Name) : new Type[0];
        }
        public Type[] GetAssemblyTypes(string name)
        {
            return _assemblyTypeMap.FindTypes(name);
        }
        public Assembly FindAssembly(string name)
        {
            return _assemblyMap.FindAssembly(name);
        }
        public Assembly[] FindAssemblies(string name)
        {
            return _assemblyMap.FindAssemblies(name);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.Loader;

namespace TypeSupport
{
    public class TypeMap : IDictionary<string, TypeMapEntry>
    {
        private Dictionary<string, TypeMapEntry> _map = new Dictionary<string, TypeMapEntry>();

        public ICollection<string> Keys => ((IDictionary<string, TypeMapEntry>)_map).Keys;

        public ICollection<TypeMapEntry> Values => ((IDictionary<string, TypeMapEntry>)_map).Values;

        public int Count => ((ICollection<KeyValuePair<string, TypeMapEntry>>)_map).Count;

        public bool IsReadOnly => ((ICollection<KeyValuePair<string, TypeMapEntry>>)_map).IsReadOnly;

        public TypeMapEntry this[string key] { get => ((IDictionary<string, TypeMapEntry>)_map)[key]; set => ((IDictionary<string, TypeMapEntry>)_map)[key] = value; }

        public TypeMap()
        {
        }
        public void AddBuiltinTypes()
        {
            AddType("int", typeof(int));
            AddType("uint", typeof(uint));
            AddType("long", typeof(long));
            AddType("ulong", typeof(ulong));
            AddType("short", typeof(short));
            AddType("ushort", typeof(ushort));
            AddType("float", typeof(float));
            AddType("double", typeof(double));
            AddType("string", typeof(string));
            AddType("decimal", typeof(decimal));
            AddType("bool", typeof(bool));
            AddType("char", typeof(char));
            AddType("byte", typeof(byte));
        }
        public void AddTypeCodes()
        {
            foreach (var name in Enum.GetNames(typeof(TypeCode)))
            {
                Type t = Type.GetType("System." + name);
                AddType(name, t);
            }
        }

        public virtual void AddType(string name, Type type)
        {
            if(_map.TryGetValue(name, out TypeMapEntry entry))
            {
                int index = entry.Types.BinarySearch(type, new TypeComparer());
                if(index < 0)
                {
                    entry.Types.Insert(~index, type);
                }
            }
            else
            {
                _map[name] = new TypeMapEntry(name, type);
            }
        }
        public virtual Type FindType(string name)
        {
            if (name != null)
            {
                (string typeName, string assemblyName) = TypeSupport.ParseAssemblyQualifiedName(name);
                if (TryGetValue(typeName, out TypeMapEntry tme))
                {
                    if(!String.IsNullOrEmpty(assemblyName))
                    {
                        return tme.GetType(assemblyName);
                    }
                    else
                    {
                        return tme.Type;
                    }
                }
            }
            return null;
        }
        public virtual Type[] FindTypes(string name)
        {
            if (TryGetValue(name, out TypeMapEntry tme))
            {
                return tme.Types.ToArray();
            }
            return new Type[0];
        }
        public void AddLoadContext(AssemblyLoadContext alc)
        {
            foreach (var a in alc.Assemblies)
            {
                AddAssembly(a);
            }
        }

        public virtual void AddAssembly(Assembly a, bool useSimpleName = false)
        {
            if(a != null)
            {
                foreach (var t in a.GetTypes())
                {
                    string name = useSimpleName ? t.Name : t.FullName;
                    AddType(name, t);
                }
            }
        }


        public void Add(string key, TypeMapEntry value)
        {
            ((IDictionary<string, TypeMapEntry>)_map).Add(key, value);
        }

        public bool ContainsKey(string key)
        {
            return ((IDictionary<string, TypeMapEntry>)_map).ContainsKey(key);
        }

        public bool Remove(string key)
        {
            return ((IDictionary<string, TypeMapEntry>)_map).Remove(key);
        }

        public bool TryGetValue(string key, [MaybeNullWhen(false)] out TypeMapEntry value)
        {
            return ((IDictionary<string, TypeMapEntry>)_map).TryGetValue(key, out value);
        }

        public void Add(KeyValuePair<string, TypeMapEntry> item)
        {
            ((ICollection<KeyValuePair<string, TypeMapEntry>>)_map).Add(item);
        }

        public void Clear()
        {
            ((ICollection<KeyValuePair<string, TypeMapEntry>>)_map).Clear();
        }

        public bool Contains(KeyValuePair<string, TypeMapEntry> item)
        {
            return ((ICollection<KeyValuePair<string, TypeMapEntry>>)_map).Contains(item);
        }

        public void CopyTo(KeyValuePair<string, TypeMapEntry>[] array, int arrayIndex)
        {
            ((ICollection<KeyValuePair<string, TypeMapEntry>>)_map).CopyTo(array, arrayIndex);
        }

        public bool Remove(KeyValuePair<string, TypeMapEntry> item)
        {
            return ((ICollection<KeyValuePair<string, TypeMapEntry>>)_map).Remove(item);
        }

        public IEnumerator<KeyValuePair<string, TypeMapEntry>> GetEnumerator()
        {
            return ((IEnumerable<KeyValuePair<string, TypeMapEntry>>)_map).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_map).GetEnumerator();
        }
    }
}

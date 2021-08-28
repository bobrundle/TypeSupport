using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TypeSupport
{
    public class AssemblyMap : IDictionary<string,AssemblyMapEntry>
    {
        Dictionary<string, AssemblyMapEntry> _map = new Dictionary<string, AssemblyMapEntry>();
        public virtual void AddAssembly(string name, Assembly a)
        {
            if (_map.TryGetValue(name, out AssemblyMapEntry entry))
            {
                int index = entry.Assemblies.BinarySearch(a, new AssemblyComparer());
                if (index < 0)
                {
                    entry.Assemblies.Insert(~index, a);
                }
            }
            else
            {
                _map[name] = new AssemblyMapEntry(name, a);
            }
        }

        public Assembly FindAssembly(string name)
        {
            if(_map.TryGetValue(name, out AssemblyMapEntry entry))
            {
                return entry.Assembly;
            }
            return null;
        }
        public Assembly[] FindAssemblies(string name)
        {
            if (_map.TryGetValue(name, out AssemblyMapEntry entry))
            {
                return entry.Assemblies.ToArray();
            }
            return new Assembly[0];
        }
        public AssemblyMapEntry this[string key] { get => ((IDictionary<string, AssemblyMapEntry>)_map)[key]; set => ((IDictionary<string, AssemblyMapEntry>)_map)[key] = value; }

        public ICollection<string> Keys => ((IDictionary<string, AssemblyMapEntry>)_map).Keys;

        public ICollection<AssemblyMapEntry> Values => ((IDictionary<string, AssemblyMapEntry>)_map).Values;

        public int Count => ((ICollection<KeyValuePair<string, AssemblyMapEntry>>)_map).Count;

        public bool IsReadOnly => ((ICollection<KeyValuePair<string, AssemblyMapEntry>>)_map).IsReadOnly;

        public void Add(string key, AssemblyMapEntry value)
        {
            ((IDictionary<string, AssemblyMapEntry>)_map).Add(key, value);
        }

        public void Add(KeyValuePair<string, AssemblyMapEntry> item)
        {
            ((ICollection<KeyValuePair<string, AssemblyMapEntry>>)_map).Add(item);
        }

        public void Clear()
        {
            ((ICollection<KeyValuePair<string, AssemblyMapEntry>>)_map).Clear();
        }

        public bool Contains(KeyValuePair<string, AssemblyMapEntry> item)
        {
            return ((ICollection<KeyValuePair<string, AssemblyMapEntry>>)_map).Contains(item);
        }

        public bool ContainsKey(string key)
        {
            return ((IDictionary<string, AssemblyMapEntry>)_map).ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<string, AssemblyMapEntry>[] array, int arrayIndex)
        {
            ((ICollection<KeyValuePair<string, AssemblyMapEntry>>)_map).CopyTo(array, arrayIndex);
        }

        public IEnumerator<KeyValuePair<string, AssemblyMapEntry>> GetEnumerator()
        {
            return ((IEnumerable<KeyValuePair<string, AssemblyMapEntry>>)_map).GetEnumerator();
        }

        public bool Remove(string key)
        {
            return ((IDictionary<string, AssemblyMapEntry>)_map).Remove(key);
        }

        public bool Remove(KeyValuePair<string, AssemblyMapEntry> item)
        {
            return ((ICollection<KeyValuePair<string, AssemblyMapEntry>>)_map).Remove(item);
        }

        public bool TryGetValue(string key, [MaybeNullWhen(false)] out AssemblyMapEntry value)
        {
            return ((IDictionary<string, AssemblyMapEntry>)_map).TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_map).GetEnumerator();
        }
    }
}

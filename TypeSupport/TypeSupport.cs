using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeSupport
{
    public static class TypeSupport
    {
        public static string FormatAssemblyQualifiedName(Type t)
        {
            if (t != null)
            {
                return t.FullName + ", " + t.Assembly.GetName().Name;
            }
            return "";
        }
        public static (string, string) ParseAssemblyQualifiedName(string s)
        {
            if (s != null)
            {
                string[] ss = s.Split(',');
                if(ss.Length > 1)
                {
                    return (ss[0], ss[1].Trim());
                }
                else
                {
                    return (ss[0], "");
                }
            }
            return ("", "");
        }
        public static string GetNameSpace(Type t)
        {
            if(t != null)
            {
                string fullname = t.FullName;
                string[] ss = fullname.Split('.');
                if(ss.Length > 1)
                {
                    string[] ss1 = new string[ss.Length - 1];
                    Array.Copy(ss, 0, ss1, 0, ss.Length - 1);
                    return string.Join('.', ss1);
                }
            }
            return "";
        }
    }
}

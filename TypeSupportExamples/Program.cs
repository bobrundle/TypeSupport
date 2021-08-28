using ClassAInterface;
using System;
using System.IO;
using System.Reflection;
using System.Runtime.Loader;
using TypeSupport;

namespace TypeSupportExamples
{
    public class ClassA : IClassAInterface
    {
        public string Hello()
        {
            return "In main program";
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            // Find builtin type

            Type t00 = typeof(int);
            Type t01 = Type.GetType("int");  // null
            Type t02 = Type.GetType("System.Int32");

            // Find system type

            Type t10 = typeof(DateTime);
            Type t11 = Type.GetType("DateTime"); // null
            Type t12 = Type.GetType("System.DateTime");

            // Find local type

            Type t20 = typeof(ClassA);
            Type t21 = Type.GetType("ClassA"); // null
            Type t22 = Type.GetType("TypeSupportExamples.ClassA");

            // 3 Names of a type

            Console.WriteLine(t22.Name);
            Console.WriteLine(t22.FullName);
            Console.WriteLine(t22.AssemblyQualifiedName);

            // Find type dependency

            Type t30 = typeof(IClassAInterface);
            Type t31 = Type.GetType("IClassAInterface"); // null
            Type t32 = Type.GetType("ClassAInterface.IClassAInterface"); //null
            Type t34 = Type.GetType("ClassAInterface.IClassAInterface, ClassAInterface");

            // Using the global type map

            Console.WriteLine();
            var globalTM = new GlobalTypeMap();
            Type t0 = globalTM.FindType("int");
            Console.WriteLine(t0.FullName);
            Type t1 = globalTM.FindType("DateTime");
            Console.WriteLine(t1.FullName);
            Type t2 = globalTM.FindType("TypeSupportExamples.ClassA");
            Console.WriteLine(t2.FullName);

            // Using the scoped type map

            var scopedTM1 = new ScopedTypeMap(globalTM);
            scopedTM1.UsingNamespace("TypeSupportExamples");
            Type t3 = scopedTM1.FindType("ClassA");
            IClassAInterface d0 = Activator.CreateInstance(t3) as IClassAInterface;
            Console.WriteLine();
            Console.WriteLine(d0.Hello()); // In main program

            // Using the scoped type map on loaded assemblies

            var scopedTM2 = new ScopedTypeMap(globalTM);
            string apath0 = Path.Combine(Directory.GetCurrentDirectory(), "AssemblyA.dll");
            Assembly a0 = AssemblyLoadContext.Default.LoadFromAssemblyPath(apath0);
            scopedTM2.UsingNamespace("NamespaceA1");
            Type t4 = scopedTM2.FindType("ClassA"); // This is NamespaceA1.ClassA in AssemblyA
            IClassAInterface d1 = Activator.CreateInstance(t4) as IClassAInterface;
            Console.WriteLine(d1.Hello());

            // Using the scoped type map on load contexts

            var scopedTM3 = new ScopedTypeMap(globalTM);
            string apath1 = Path.Combine(Directory.GetCurrentDirectory(),@"AssemblyB.dll");
            AssemblyLoadContext alc0 = new AssemblyLoadContext("alc0");
            Assembly a1 = alc0.LoadFromAssemblyPath(apath1);
            scopedTM3.UsingNamespace("NamespaceB1");
            Type t5 = scopedTM3.FindType("ClassA"); // This is NamespaceB1.ClassA in AssemblyB
            IClassAInterface d2 = Activator.CreateInstance(t5) as IClassAInterface;
            Console.WriteLine(d2.Hello());

            // Scoping multiple types

            var scopedTM4 = new ScopedTypeMap(globalTM);
            scopedTM4.UsingNamespace("TypeSupportExamples");
            scopedTM4.UsingNamespace("NamespaceA1");
            scopedTM4.UsingNamespace("NamespaceA2");
            scopedTM4.UsingNamespace("NamespaceB1");
            Type[] tt = scopedTM4.FindTypes("ClassA");
            Console.WriteLine();
            foreach (var t in tt)
                Console.WriteLine(t.FullName);

        }

    }
}

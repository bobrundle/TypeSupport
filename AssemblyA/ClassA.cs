using ClassAInterface;
using System;

namespace NamespaceA1
{
    public class ClassA : IClassAInterface
    {
        public string Hello() { return "This is NamespaceA1.ClassA in AssemblyA"; }
    }
}

namespace NamespaceA2
{
    public class ClassA : IClassAInterface
    {
        public string Hello() { return "This is NamespaceA2.ClassA in AssemblyA"; }
    }
}

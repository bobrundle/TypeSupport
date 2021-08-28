using ClassAInterface;
using System;

namespace NamespaceB1
{
    public class ClassA : IClassAInterface
    {
        public string Hello() { return "This is NamespaceB1.ClassA in AssemblyB"; }
    }
}

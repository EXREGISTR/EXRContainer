using System;

namespace EXRContainer {
    [AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method)]
    internal class EXRConstructorAttribute : Attribute { }
}
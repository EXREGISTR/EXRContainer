using System;

namespace EXRContainer {
    [AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method)]
    public class EXRInitializatorAttribute : Attribute { }
}
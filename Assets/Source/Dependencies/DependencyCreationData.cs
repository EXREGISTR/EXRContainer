using System;
using System.Collections.Generic;
using EXRContainer.Core;

namespace EXRContainer.Dependencies {
    internal class DependencyCreationData {
        public Type ConcreteType { get; set; }
        public object Instance { get; set; }
        public LifeTime LifeTime { get; set; }
        public bool NonLazy { get; set; }

        public IDependencyCreator Creator { get; set; }

        public OnResolve<object> OnResolveCallback { get; set; }
        public Finalizator<object> Finalizator { get; set; }

        public PreCreationCallback PreCreationCallback { get; set; }
        public Factory<object> Factory { get; set; }
        public OnInstantiatedCallback<object> OnInstantiatedCallback { get; set; }

        public Dictionary<Type, object> Arguments { get; set; }
        public HashSet<Type> ContractTypes { get; set; }
    }
}
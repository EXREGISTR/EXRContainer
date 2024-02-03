using System;
using System.Collections.Generic;
using EXRContainer.Core;

namespace EXRContainer.Dependencies {
    internal interface IDependencyCreationData {
        public Type ConcreteType { get; }
        public bool NonLazy { get; }
        public IEnumerable<Type> ContractTypes { get; }
        public IDependencyCreator Creator { get; }
    }

    internal class DependencyCreationData : IDependencyCreationData {
        public Type ConcreteType { get; set; }
        public object Instance { get; set; }
        public LifeTime LifeTime { get; set; }
        public bool NonLazy { get; set; }

        public Func<DependencyCreationData, IDependencyCreator> CreatorProvider { get; set; }
        public IDependencyCreator Creator => CreatorProvider(this);

        public OnResolve<object> OnResolveCallback { get; set; }
        public Finalizator<object> Finalizator { get; set; }

        public PreCreationCallback PreCreationCallback { get; set; }
        public Factory<object> Factory { get; set; }
        public PostCreationCallback<object> PostCreationCallback { get; set; }

        public Dictionary<Type, object> Arguments { get; set; }
        public HashSet<Type> ContractTypes { get; set; }
        IEnumerable<Type> IDependencyCreationData.ContractTypes => ContractTypes;
    }
}
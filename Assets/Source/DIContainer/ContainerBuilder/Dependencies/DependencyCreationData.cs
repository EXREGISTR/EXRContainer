using System;
using System.Collections.Generic;
using EXRContainer.Core;

namespace EXRContainer.Dependencies {
    internal interface IDependencyCreationData {
        public Type ConcreteType { get; }
        public bool NonLazy { get; }
        public LifeTime LifeTime { get; }
        public IEnumerable<Type> ContractTypes { get; }
        public IDependency Dependency { get; }
        public OnResolveCallback<object> OnResolveCallback { get; }
        public Finalizator<object> Finalizator { get; set; }
    }

    internal class DependencyCreationData<TService> : IDependencyCreationData where TService : class {
        public DependencyCreationData(LambdasGenerationConfiguration config) {
            CodeGenerationConfiguration = config;
            ConcreteType = typeof(TService);
        }

        public LambdasGenerationConfiguration CodeGenerationConfiguration { get; }
        public Type ConcreteType { get; }

        public object Instance { get; set; }
        public LifeTime LifeTime { get; set; }
        public bool NonLazy { get; set; }

        public Func<DependencyCreationData<TService>, IDependency> DependencyProvider { get; set; }
        public IDependency Dependency => DependencyProvider(this);

        public OnResolveCallback<object> OnResolveCallback { get; set; }
        public Finalizator<object> Finalizator { get; set; }

        public PreCreationCallback PreCreationCallback { get; set; }
        public Factory<TService> Factory { get; set; }
        public PostCreationCallback<TService> PostCreationCallback { get; set; }

        public Dictionary<Type, object> Arguments { get; set; }
        public HashSet<Type> ContractTypes { get; set; }
        IEnumerable<Type> IDependencyCreationData.ContractTypes => ContractTypes;
    }
}
using System;
using System.Collections.Generic;

namespace EXRContainer.Core {
    public readonly struct DependencyProvider {
        public IDependency Source { get; }
        public IEnumerable<Type> ContractTypes { get; }
        public LifeTime LifeTime { get; }
        public OnResolveCallback<object> OnResolveCallback { get; }
        public Finalizator<object> Finalizator { get; }

        public DependencyProvider(IDependency source, IEnumerable<Type> contractTypes, 
            LifeTime lifeTime, OnResolveCallback<object> onResolveCallback, Finalizator<object> finalizator) {
            Source = source;
            ContractTypes = contractTypes;
            LifeTime = lifeTime;
            OnResolveCallback = onResolveCallback;
            Finalizator = finalizator;
        }
    }
}

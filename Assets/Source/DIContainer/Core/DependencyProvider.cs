using System;
using System.Collections.Generic;

namespace EXRContainer.Core {
    public readonly struct DependencyProvider {
        public readonly IDependency Source;
        public readonly IEnumerable<Type> ContractTypes;
        public readonly LifeTime LifeTime;
        public readonly OnResolveCallback<object> OnResolveCallback;
        public readonly Finalizator<object> Finalizator;

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

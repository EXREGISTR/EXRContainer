using EXRContainer.LambdaGeneration;
using EXRContainer.Core;
using System;
using UnityEngine;

namespace EXRContainer.Dependencies {
    internal partial class DependencyConfigurator<TService> : ICreationMethodChoiser<TService> {
        
        public ICallbacksChoiser<TService> FromComponentOnPrefab(GameObject prefab) {
            throw new NotImplementedException();
        }

        public ICallbacksChoiser<TService> FromNewComponentFor(Func<IDIContext, GameObject> objectProvider) {
            throw new NotImplementedException();
        }

        public ICallbacksChoiser<TService> FromComponentFor(Func<IDIContext, GameObject> objectProvider) {
            throw new NotImplementedException();
        }

        public ICallbacksChoiser<TService> FromNewComponentOnPrefab(GameObject prefab) {
            throw new NotImplementedException();
        }

        public IWithoutCreationCallbacksChoiser<TService> FromFactory(Factory<TService> factory) {
            throw new NotImplementedException();
        }

        public IWithoutCreationCallbacksCompleteChoiser<TService> FromInstance(TService service) {
            throw new NotImplementedException();
        }
    }
}
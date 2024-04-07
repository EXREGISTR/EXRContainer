using EXRContainer.Core;
using System;
using UnityEngine;

namespace EXRContainer.Dependencies {
    public partial interface ICreationMethodChoiser<TService> where TService : class {
        public ICallbacksChoiser<TService> FromNewComponentOnPrefab(GameObject prefab);
        public ICallbacksChoiser<TService> FromNewComponentFor(Func<IDIContext, GameObject> objectProvider);
        public ICallbacksChoiser<TService> FromComponentOnPrefab(GameObject prefab);
        public ICallbacksChoiser<TService> FromComponentFor(Func<IDIContext, GameObject> objectProvider);
    }
}
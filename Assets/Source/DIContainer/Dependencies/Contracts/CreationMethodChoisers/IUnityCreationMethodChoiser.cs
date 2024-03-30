using UnityEngine;

namespace EXRContainer.Dependencies {
    public partial interface ICreationMethodChoiser<TService> where TService : class {
        public ICallbacksChoiser<TService> FromNewComponentOnPrefab(GameObject prefab);
        public ICallbacksChoiser<TService> FromComponentOnPrefab(GameObject prefab);
    }
}
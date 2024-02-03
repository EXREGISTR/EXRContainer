using System;
using System.Collections.Generic;
using System.Linq;

namespace EXRContainer.Core {
    public class DependenciesContext : IDIContext {
        private readonly DIContainer container;
        private Dictionary<Type, object> localObjects;

        public DependenciesContext(DIContainer container) {
            this.container = container;
        }

        public void AddDependency<T>(T instance, IEnumerable<Type> contractTypes) {
            localObjects ??= new Dictionary<Type, object>();

            if (contractTypes == null || !contractTypes.Any()) {
                localObjects[typeof(T)] = instance;
                return;
            }

            foreach (var type in contractTypes) {
                var result = localObjects.TryAdd(type, instance);
                if (!result) throw new AlreadyExistSingleException(type);
            }
        }

        public object Resolve(Type dependencyType) {
            if (localObjects != null && localObjects.TryGetValue(dependencyType, out var instance)) {
                return instance;
            }

            return container.Resolve(this, dependencyType);
        }
        public void Delete(object instance) => container.Release(this, instance);
        public void Dispose() => container.DeleteContext(this);

        public override string ToString() => container.ToString() + GetHashCode().ToString();
    }
}

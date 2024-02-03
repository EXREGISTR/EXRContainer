using System;
using System.Collections.Generic;

namespace EXRContainer.Core {
    public class DependenciesContext : IDIContext {
        private readonly DIContainer container;
        private Dictionary<Type, object> localObjects;

        public DependenciesContext(DIContainer container) {
            this.container = container;
        }

        public void AddDependency(object instance) {
            var type = instance.GetType();

            if (localObjects == null) {
                localObjects = new Dictionary<Type, object> { [type] = instance };
                return;
            }

            if (!localObjects.ContainsKey(type)) {
                localObjects[type] = instance;
            } else {
                throw new AlreadyExistSingleException(type);
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

using System;
using System.Collections.Generic;
using System.Linq;

namespace EXRContainer.Core {
    public class DependenciesContext : IDIContext {
        private readonly DIContainer container;
        private readonly Dictionary<Type, object> localObjects;

        public DependenciesContext(DIContainer container) {
            this.container = container;
            this.localObjects = new Dictionary<Type, object>();
            RegisterContext();
        }

        private void RegisterContext() {
            localObjects[typeof(IDIContext)] = this;
        }

        public void AddDependency<T>(T instance, IEnumerable<Type> contractTypes) {
            if (contractTypes == null || !contractTypes.Any()) {
                AddDependency(typeof(T), instance);
                return;
            }

            foreach (var type in contractTypes) {
                AddDependency(type, instance);
            }
        }

        private void AddDependency(Type type, object instance) {
            DependencyTypeValidator.AssertTypeForUsable(type);

            var result = localObjects.TryAdd(type, instance);
            if (!result) throw new AlreadyExistSingleException(type);
        }

        public object Resolve(Type dependencyType) {
            if (localObjects.TryGetValue(dependencyType, out var instance)) {
                return instance;
            }

            return container.Resolve(this, dependencyType);
        }
        public void Delete(object instance) => container.Release(this, instance);
        public void Dispose() => container.DeleteContext(this);

        public override string ToString() => container.ToString() + GetHashCode().ToString();
    }
}

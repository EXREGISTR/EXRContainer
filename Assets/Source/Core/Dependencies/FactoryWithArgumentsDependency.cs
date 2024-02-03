using System;
using System.Collections.Generic;

namespace EXRContainer.Core {
    internal class FactoryWithArgumentsDependency : FactoryDependency, IDependency {
        private readonly Dictionary<Type, object> arguments;

        public FactoryWithArgumentsDependency(
            Dictionary<Type, object> arguments,
            PreCreationCallback preCreationCallback, 
            Factory<object> factory)
            : base(preCreationCallback, factory) {
            this.arguments = arguments;
        }

        public new object Resolve(IDIContext context) {
            var localContext = new LocalContext(arguments, context);
            return base.Resolve(localContext);
        }

        private readonly struct LocalContext : IDIContext {
            private readonly Dictionary<Type, object> arguments;
            private readonly IDIContext parent;

            public LocalContext(Dictionary<Type, object> arguments, IDIContext parent) {
                this.arguments = arguments;
                this.parent = parent;
            }

            public object Resolve(Type dependencyType) {
                if (arguments.TryGetValue(dependencyType, out var argument)) {
                    return argument;
                }

                return parent.Resolve(dependencyType);
            }

            public void AddDependency<T>(T instance, IEnumerable<Type> contractTypes) => parent.AddDependency(instance, contractTypes);

            public void Delete(object instance) => parent.Delete(instance);

            public void Dispose() => parent.Dispose();
        }
    }
}
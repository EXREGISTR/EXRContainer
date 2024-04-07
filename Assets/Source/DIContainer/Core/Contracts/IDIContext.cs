using System;
using System.Collections.Generic;

namespace EXRContainer.Core {
    public interface IDIContext {
        public void AddDependency<T>(T instance, IEnumerable<Type> contractTypes);
        public object Resolve(Type dependencyType);
        public void Delete(object instance);
    }
}

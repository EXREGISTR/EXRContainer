using System;

namespace EXRContainer.Core {
    public interface IDIContainer {
        public IDIContext CreateContext();
        public object Resolve(IDIContext context, Type dependencyType);
        public void Release(IDIContext context, object instance);
    }
}

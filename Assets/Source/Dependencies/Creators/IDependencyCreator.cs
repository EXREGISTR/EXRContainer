using EXRContainer.Core;

namespace EXRContainer.Dependencies {
    public interface IDependencyCreator {
        public IDependency Create();
    }
}
using EXRContainer.Core;

namespace EXRContainer.Dependencies {
    public class FactoryDependencyCreator {
        private readonly Factory<object> factory;

        
    }

    internal interface IDependencyCreator {
        public IDependency Create();
    }
}
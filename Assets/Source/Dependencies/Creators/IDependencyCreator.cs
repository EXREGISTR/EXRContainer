using EXRContainer.Core;

namespace EXRContainer.Dependencies {
    public class FactoryDependencyCreator {
        private readonly Factory<object> factory;

        
    }

    public class InstanceDependencyCreator {
        private readonly object instance;


    }

    internal interface IDependencyCreator {
        public IDependency Create();
    }
}
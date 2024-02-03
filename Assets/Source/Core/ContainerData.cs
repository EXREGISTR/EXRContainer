using System.Collections.Generic;

namespace EXRContainer.Core {
    internal readonly struct ContainerData {
        public IEnumerable<DependencyProvider> NonLazySingletonsDependencies { get; }
        public IEnumerable<DependencyProvider> NonLazyScopedDependencies { get; }

        public ContainerData(IEnumerable<DependencyProvider> nonLazySingletons, IEnumerable<DependencyProvider> nonLazyScoped) {
            NonLazySingletonsDependencies = nonLazySingletons;
            NonLazyScopedDependencies = nonLazyScoped;
        }
    }
}
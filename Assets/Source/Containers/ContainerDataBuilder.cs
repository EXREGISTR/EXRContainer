using System.Collections.Generic;
using EXRContainer.Core;

namespace EXRContainer {
    internal struct ContainerDataBuilder {
        private List<DependencyProvider> nonLazySingletons;
        private List<DependencyProvider> scopedNonLazy;

        public void PlaceNonLazy(DependencyProvider provider) {
            if (provider.LifeTime == LifeTime.Singleton) {
                nonLazySingletons ??= new List<DependencyProvider>();
                nonLazySingletons.Add(provider);
            } else if (provider.LifeTime == LifeTime.Scoped) {
                scopedNonLazy ??= new List<DependencyProvider>();
                scopedNonLazy.Add(provider);
            }
        }

        public readonly ContainerData Build() => new(nonLazySingletons, scopedNonLazy);
    }
}
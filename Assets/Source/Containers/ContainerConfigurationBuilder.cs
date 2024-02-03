using System.Collections.Generic;
using EXRContainer.Core;

namespace EXRContainer {
    internal struct ContainerConfigurationBuilder {
        private List<DependencyProvider> singletonsNonLazy;
        private List<DependencyProvider> scopedNonLazy;

        public void PlaceNonLazy(DependencyProvider provider) {
            if (provider.LifeTime == LifeTime.Singleton) {
                singletonsNonLazy ??= new List<DependencyProvider>();
                singletonsNonLazy.Add(provider);
            } else if (provider.LifeTime == LifeTime.Scoped) {
                scopedNonLazy ??= new List<DependencyProvider>();
                scopedNonLazy.Add(provider);
            }
        }

        public readonly IContainerData Build() => new ContainerData();
    }
}
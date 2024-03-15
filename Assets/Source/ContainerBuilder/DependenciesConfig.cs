using EXRContainer.Core;

namespace EXRContainer {
    internal readonly struct DependenciesConfig {
        public LifeTime DefaultLifeTime { get; }
        public bool NonLazyCreationByDefault { get; }
    }
}
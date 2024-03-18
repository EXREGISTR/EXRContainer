using EXRContainer.Core;

namespace EXRContainer {
    public readonly struct DependenciesConfiguration {
        public LifeTime DefaultLifeTime { get; }
        public bool NonLazyCreationByDefault { get; }

        public DependenciesConfiguration(LifeTime defaultLifeTime, bool nonLazyCreationByDefault) {
            DefaultLifeTime = defaultLifeTime;
            NonLazyCreationByDefault = nonLazyCreationByDefault;
        }
    }
}
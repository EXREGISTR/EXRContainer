using EXRContainer.Core;

namespace EXRContainer {
    internal readonly struct ContainerConfiguration {
        public CodeGenerationData CodeGenerationData { get; }
        public LifeTime DefaultLifeTime { get; }
        public bool NonLazyCreationByDefault { get; }
    }
}
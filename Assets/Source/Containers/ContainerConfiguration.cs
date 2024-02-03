using EXRContainer.CodeGeneration;

namespace EXRContainer {
    internal readonly struct ContainerConfiguration {
        public FactoryLambdaCreator InstantiationLambdaCreator { get; }
        public FinalizationLambdaCreator FinalizationLambdaCreator { get; }
    }
}
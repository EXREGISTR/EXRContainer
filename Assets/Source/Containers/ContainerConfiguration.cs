using EXRContainer.CodeGeneration;
using EXRContainer.Core;

namespace EXRContainer {
    public readonly struct ContainerConfiguration {
        public ILambdaCreator<Factory<object>> InstantiationLambdaCreator { get; }
        public ILambdaCreator<Finalizator<object>> FinalizationLambdaCreator { get; }
    }
}
using EXRContainer.Core;
using EXRContainer.LambdaGeneration;

namespace EXRContainer {
    internal interface ILambdasContainer {
        public Factory<object> GetFactory(in DependencyGenerationData data);
        public Finalizator<object> GetFinalizator(in DependencyGenerationData data);
        public OnResolveCallback<object> GetOnResolveCallback(in DependencyGenerationData data);
    }
}
using EXRContainer.Core;

namespace EXRContainer.LambdaGeneration {
    internal class OnResolveGenerator : LambdaGenerator<OnResolve<object>> {
        public OnResolveGenerator(IGenerationExecutor providers) : base(providers) { }
    }
}
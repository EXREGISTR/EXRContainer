using EXRContainer.Core;

namespace EXRContainer.LambdaGeneration {
    internal class FinalizatorGenerator : LambdaGenerator<Finalizator<object>> {
        public FinalizatorGenerator(IGenerationExecutor providers) : base(providers) { }
    }
}
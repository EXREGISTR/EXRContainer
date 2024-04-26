using EXRContainer.LambdaGeneration;

namespace EXRContainer {
    internal class LambdasGenerationConfiguration {
        private readonly FactoryGenerationExecutor defaultFactoryExecutor;
        private readonly LambdaGenerationExecutor defaultOnResolveExecutor;
        private readonly LambdaGenerationExecutor defaultFinalizatorExecutor;

        public LambdasGenerationConfiguration(
            FactoryGenerationExecutor defaultFactoryExecutor,
            LambdaGenerationExecutor defaultFinalizatorExecutor,
            LambdaGenerationExecutor defaultOnResolveExecutor) {
            this.defaultFactoryExecutor = defaultFactoryExecutor;
            this.defaultFinalizatorExecutor = defaultFinalizatorExecutor;
            this.defaultOnResolveExecutor = defaultOnResolveExecutor;
        }

        public FactoryGenerationExecutor CreateFactoryExecutor() => new(defaultFactoryExecutor);
        public LambdaGenerationExecutor CreateFinalizatorExecutor() => new(defaultFinalizatorExecutor);
        public LambdaGenerationExecutor CreateOnResolveCallbackExecutor() => new(defaultOnResolveExecutor);
    }
}
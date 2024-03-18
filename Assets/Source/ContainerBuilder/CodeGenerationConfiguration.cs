using EXRContainer.CodeGeneration;
using EXRContainer.Core;

namespace EXRContainer {
    internal class CodeGenerationConfiguration {
        private readonly FactoryLambdaCreator defaultFactoryCreator;
        private readonly FinalizationLambdaCreator defaultFinalizatorCreator;

        public CodeGenerationConfiguration(
            FactoryLambdaCreator defaultFactoryCreator, 
            FinalizationLambdaCreator defaultFinalizatorCreator) {
            this.defaultFactoryCreator = defaultFactoryCreator;
            this.defaultFinalizatorCreator = defaultFinalizatorCreator;
        }

        public ILambdaCreator<Factory<object>> DefaultFactoryCreator => defaultFactoryCreator;
        public ILambdaCreator<Finalizator<object>> DefaultFinalizatorCreator => defaultFinalizatorCreator;

        public FactoryLambdaCreator CopyFactoryCreator => new(defaultFactoryCreator);
        public FinalizationLambdaCreator CopyFinalizatorCreator => new(defaultFinalizatorCreator);
    }
}
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


        public FactoryLambdaCreator CreateFactoryCreator()
            => new(defaultFactoryCreator);
        public FactoryLambdaCreator CreateFactoryCreator(ICreationExpressionsProvider provider) 
            => new(provider, defaultFactoryCreator);
        public FinalizationLambdaCreator CreateFinalizatorCreator() => new(defaultFinalizatorCreator);
    }
}
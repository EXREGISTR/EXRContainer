using EXRContainer.LambdaGeneration;
using EXRContainer.Core;

namespace EXRContainer {
    internal class CodeGenerationConfiguration {
        private readonly FactoryExpressionsContainer defaultFactoryExpressions;
        private readonly FinalizatorExpressionsContainer defaultFinalizatorExpressions;

        private readonly FactoryGenerator defaultFactoryCreator;
        private readonly FinalizatorGenerator defaultFinalizatorCreator;

        public CodeGenerationConfiguration(
            FactoryGenerator defaultFactoryCreator, 
            FinalizatorGenerator defaultFinalizatorCreator) {
            this.defaultFactoryCreator = defaultFactoryCreator;
            this.defaultFinalizatorCreator = defaultFinalizatorCreator;
        }

        public ILambdaCreator<Factory<object>> DefaultFactoryCreator => defaultFactoryCreator;
        public ILambdaCreator<Finalizator<object>> DefaultFinalizatorCreator => defaultFinalizatorCreator;


        public FactoryGenerator CreateFactoryCreator()
            => new(defaultFactoryExpressions);
        public FactoryGenerator CreateFactoryCreator(IDependencyInitializationProvider initializator) 
            => new(initializator, defaultFactoryCreator);
        public FinalizatorGenerator CreateFinalizatorCreator() => new(defaultFinalizatorCreator);
    }
}
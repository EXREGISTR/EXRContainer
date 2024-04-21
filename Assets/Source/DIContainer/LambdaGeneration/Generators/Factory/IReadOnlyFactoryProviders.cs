using System.Collections.Generic;

namespace EXRContainer.LambdaGeneration {
    internal interface IReadOnlyFactoryProviders {
        public IEnumerable<IVariablesRegistrationProvider> GetVariablesRegistrationProviders();
        public IEnumerable<IExpressionsProvider> GetBeforeCreation();
        public IEnumerable<IExpressionsProvider> GetPostCreation();
    }
}

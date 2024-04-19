using System.Collections.Generic;

namespace EXRContainer.LambdaGeneration {
    internal interface IReadOnlyFinalizatorExpressionsContainer {
        public IEnumerable<IVariablesRegistrationProvider> GetVariablesRegistrationProviders();
        public IEnumerable<IExpressionsProvider> GetProviders();
    }
}
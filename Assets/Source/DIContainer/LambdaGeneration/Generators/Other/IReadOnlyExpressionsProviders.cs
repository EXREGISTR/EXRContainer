using System.Collections.Generic;

namespace EXRContainer.LambdaGeneration {
    internal interface IReadOnlyExpressionsProviders {
        public IEnumerable<IVariablesRegistrationProvider> GetVariablesRegistrationProviders();
        public IEnumerable<IExpressionsProvider> GetProviders();
    }
}
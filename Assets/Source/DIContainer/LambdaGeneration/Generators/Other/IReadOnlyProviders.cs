using System.Collections.Generic;

namespace EXRContainer.LambdaGeneration {
    internal interface IReadOnlyProviders {
        public IEnumerable<IVariablesRegistrationProvider> GetVariablesRegistrationProviders();
        public IEnumerable<IExpressionsProvider> GetProviders();
    }
}
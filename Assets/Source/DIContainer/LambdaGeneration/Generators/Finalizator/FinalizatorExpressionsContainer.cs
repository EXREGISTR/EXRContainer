using System.Collections.Generic;

namespace EXRContainer.LambdaGeneration {
    internal class FinalizatorExpressionsContainer : IExpressionProvidersContainer, IReadOnlyFinalizatorExpressionsContainer {
        private readonly IReadOnlyFinalizatorExpressionsContainer parent;
        private readonly LinkedList<IExpressionsProvider> providers;

        public FinalizatorExpressionsContainer(IReadOnlyFinalizatorExpressionsContainer parent = null) {
            this.parent = parent;
        }

        public void ExecuteGeneration(GenerationContext context) {
            parent.
        }

        public void FirstProvider(IExpressionsProvider provider) => providers.AddFirst(provider);

        public void LastProvider(IExpressionsProvider provider) => providers.AddLast(provider);

        public IEnumerable<IExpressionsProvider> GetProviders() {
            
        }

        public IEnumerable<IVariablesRegistrationProvider> GetVariablesRegistrationProviders() {

        }

    }
}
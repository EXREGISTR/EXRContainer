using System.Collections.Generic;
using System.Linq;

namespace EXRContainer.LambdaGeneration {
    internal class FinalizatorGenerationExecutor : IGenerationExecutor, IReadOnlyFinalizatorExpressionsContainer {
        private readonly IReadOnlyFinalizatorExpressionsContainer parent;

        private List<IVariablesRegistrationProvider> variablesProviders;
        private Stack<IExpressionsProvider> expressionProviders;

        public FinalizatorGenerationExecutor(IReadOnlyFinalizatorExpressionsContainer parent = null) {
            this.parent = parent;
        }

        public void Execute(GenerationContext context) {
            foreach (var provider in GetVariablesRegistrationProviders()) {
                provider.RegisterVariables(context);
            }

            foreach (var provider in GetProviders()) {
                provider.RegisterExpressions(context);
            }
        }

        public void Push(IExpressionsProvider provider) {
            expressionProviders ??= new Stack<IExpressionsProvider>();
            expressionProviders.Push(provider);
        }

        private void PushToVariablesProviders(object provider) {
            if (provider is IVariablesRegistrationProvider variablesProvider) {
                variablesProviders ??= new List<IVariablesRegistrationProvider>();
                variablesProviders.Add(variablesProvider);
            }
        }

        public IEnumerable<IExpressionsProvider> GetProviders() {
            var providers = expressionProviders;
            var noProviders = providers == null;

            if (parent == null) {
                return noProviders ? Enumerable.Empty<IExpressionsProvider>() : providers;
            }

            var parentProviders = parent.GetProviders();
            return noProviders ? parentProviders : providers.Concat(parentProviders);
        }

        public IEnumerable<IVariablesRegistrationProvider> GetVariablesRegistrationProviders() {
            var providers = variablesProviders;
            var noProviders = providers == null;

            if (parent == null) {
                return noProviders ? Enumerable.Empty<IVariablesRegistrationProvider>() : providers;
            }

            var parentProviders = parent.GetVariablesRegistrationProviders();
            return noProviders ? parentProviders : parentProviders.Concat(providers);
        }

    }
}
using System.Collections.Generic;
using System.Linq;

namespace EXRContainer.LambdaGeneration {
    internal class FactoryGenerationExecutor : IGenerationExecutor, IReadOnlyFactoryProviders {
        private readonly IReadOnlyFactoryProviders parent;

        private List<IVariablesRegistrationProvider> variablesProviders;

        private Queue<IExpressionsProvider> beforeCreationProviders;
        private readonly IDependencyInitializationProvider initializator;
        private Queue<IExpressionsProvider> postCreationProviders;

        public FactoryGenerationExecutor(IDependencyInitializationProvider initializator, IReadOnlyFactoryProviders parent = null) {
            this.initializator = initializator;
            this.parent = parent;
            PushToVariablesProviders(initializator);
        }

        public FactoryGenerationExecutor(FactoryGenerationExecutor parent) {
            this.initializator = parent.initializator;
            this.parent = parent;
        }

        public void BeforeCreation(IExpressionsProvider provider) {
            beforeCreationProviders ??= new Queue<IExpressionsProvider>();
            beforeCreationProviders.Enqueue(provider);
            PushToVariablesProviders(provider);
        }

        public void PostCreation(IExpressionsProvider provider) {
            postCreationProviders ??= new Queue<IExpressionsProvider>();
            postCreationProviders.Enqueue(provider);
            PushToVariablesProviders(provider);
        }

        public void Execute(GenerationContext context) {
            RegisterVariables(context);
            RegisterExpressions(context);
        }
        
        private void RegisterVariables(IVariablesRegistrator registrationProvider) {
            var registrators = GetVariablesRegistrationProviders();

            foreach (var registrator in registrators) {
                registrator.RegisterVariables(registrationProvider);
            }
        }

        private void RegisterExpressions(GenerationContext context) {
            var beforeCreation = GetBeforeCreation();
            var postCreation = GetPostCreation();

            foreach (var provider in beforeCreation) {
                provider.RegisterExpressions(context);
            }

            initializator.InitializeDependency(context);

            foreach (var provider in postCreation) {
                provider.RegisterExpressions(context);
            }
        }

        private void PushToVariablesProviders(object provider) {
            if (provider is IVariablesRegistrationProvider variablesProvider) {
                variablesProviders ??= new List<IVariablesRegistrationProvider>();
                variablesProviders.Add(variablesProvider);
            }
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

        public IEnumerable<IExpressionsProvider> GetBeforeCreation() {
            var providers = beforeCreationProviders;
            var noProviders = providers == null;

            if (parent == null) {
                return noProviders ? Enumerable.Empty<IExpressionsProvider>() : providers;
            }

            var parentProviders = parent.GetBeforeCreation();
            return noProviders ? parentProviders : parentProviders.Concat(providers);
        }

        public IEnumerable<IExpressionsProvider> GetPostCreation() {
            var providers = postCreationProviders;
            var noProviders = providers == null;

            if (parent == null) {
                return noProviders ? Enumerable.Empty<IExpressionsProvider>() : providers;
            }

            var parentProviders = parent.GetPostCreation();
            return noProviders ? parentProviders : parentProviders.Concat(providers);
        }
    }
}

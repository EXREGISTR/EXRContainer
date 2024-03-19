using EXRContainer.CodeGeneration;
using EXRContainer.Core;
using UnityEngine;

namespace EXRContainer {
    public abstract class EntityContainer : MonoBehaviour {
        private DIContainer source;

        internal void Install(DIContainer parent, EntityContainerSettings settings, 
            CodeGenerationConfiguration codeGenerationConfiguration) {
            var dependenciesConfig = new DependenciesConfiguration(settings.DefaultLifeTime, settings.NonLazyCreation);
            var codeGenerationConfig = CreateCodeGenerationConfig();
            var builder = new ContainerBuilder(parent, dependenciesConfig, codeGenerationConfig);
            Install(builder);

            source = builder.Build();
        }

        protected abstract void Install(ContainerBuilder builder);

        private CodeGenerationConfiguration CreateCodeGenerationConfig() {
            var factoryCreator = new FactoryLambdaCreator();
            var finalizationCreator = new FinalizationLambdaCreator();

            ConfigurateLambdaCreators(factoryCreator, finalizationCreator);

            var data = new CodeGenerationConfiguration(factoryCreator, finalizationCreator);
            return data;
        }

        // ЗАКОНЧИТЬ БАЛЯ
        private void ConfigurateLambdaCreators(
            FactoryLambdaCreator factoryCreator,
            FinalizationLambdaCreator finalizationCreator) {

        }
    }
}
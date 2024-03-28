using EXRContainer.CodeGeneration;
using EXRContainer.Core;
using UnityEngine;

namespace EXRContainer {
    public abstract class EntityContainer : MonoBehaviour {
        [SerializeField] private GameObject prefab;
        private DIContainer source;

        internal void Install(DIContainer parent, EntityContainerSettings settings, 
            CodeGenerationConfiguration codeGenerationConfiguration) {
            if (source != null) return;

            var dependenciesConfig = new DependenciesConfiguration(settings.DefaultLifeTime, settings.NonLazyCreation);
            var codeGenerationConfig = CreateCodeGenerationConfig(codeGenerationConfiguration, settings);
            var builder = new ContainerBuilder(parent, dependenciesConfig, codeGenerationConfig);
            InstallDependencies(builder);
            Install(builder);

            source = builder.Build();
            return;
        }

        private void OnDestroy() => source?.Dispose();

        private void InstallDependencies(ContainerBuilder builder) {
            builder.Register<GameObject>().FromPrefab(prefab).AsScoped();
            builder.Register<Entity>().FromNewComponentOnGameObject().AsScoped();
        }

        protected abstract void Install(ContainerBuilder builder);

        private CodeGenerationConfiguration CreateCodeGenerationConfig(CodeGenerationConfiguration other, 
            EntityContainerSettings settings) {
            var factoryCreator = other.CreateFactoryCreator();
            var finalizationCreator = other.CreateFinalizatorCreator();

            ConfigurateLambdaCreators(factoryCreator, finalizationCreator, settings);

            var data = new CodeGenerationConfiguration(factoryCreator, finalizationCreator);
            return data;
        }

        // ЗАКОНЧИТЬ БАЛЯ
        private void ConfigurateLambdaCreators(
            FactoryLambdaCreator factoryCreator,
            FinalizationLambdaCreator finalizationCreator,
            EntityContainerSettings settings) {
            if (settings.TriggerCallbacks) {
                // factoryCreator.PostCreationProvider();
                // finalizationCreator.LastProvider();
            }

            if (settings.CollisionCallbacks) {
                // factoryCreator.PostCreationProvider();
                // finalizationCreator.LastProvider();
            }
        }
    }
}
using EXRContainer.LambdaGeneration;
using EXRContainer.Core;
using UnityEngine;

namespace EXRContainer {
    public abstract class EntityContainer : MonoBehaviour {
        [SerializeField] private GameObject prefab;
        private DIContainer source;

        internal void Install(DIContainer parent, EntityContainerSettings settings, 
            LambdasGenerationConfiguration codeGenerationConfiguration) {
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
            builder.Register<Entity>().FromNewComponentOnPrefab(prefab)
                .PostInstantiate(CreateDetectors)
                .NonLazy().AsScoped();
        }

        // а может не надо
        protected virtual void CreateDetectors(IDIContext context, Entity entity) {
            // entity.CreateDetector<>
        }

        protected abstract void Install(ContainerBuilder builder);

        private LambdasGenerationConfiguration CreateCodeGenerationConfig(LambdasGenerationConfiguration other, 
            EntityContainerSettings settings) {
            var factoryCreator = other.CreateFactoryExecutor();
            var finalizationCreator = other.CreateFinalizatorExecutor();

            ConfigurateLambdaCreators(factoryCreator, finalizationCreator, settings);

            var data = new LambdasGenerationConfiguration(factoryCreator, finalizationCreator, null);
            return data;
        }

        private void ConfigurateLambdaCreators(
            FactoryGenerationExecutor factoryCreator,
            LambdaGenerationExecutor finalizationCreator,
            EntityContainerSettings settings) {
            if (settings.TriggerCallbacks) {
                // factoryCreator.PostCreationProvider(new SubscribeOnTriggerCallbacks());
                // finalizationCreator.LastProvider(new UnsubscribeOfTriggerCallbacks());
            }

            if (settings.CollisionCallbacks) {
                // factoryCreator.PostCreationProvider(new SubscribeOnCollisionCallbacks());
                // finalizationCreator.LastProvider(new UnsubscribeOfCollisionCallbacks());
            }
        }
    }
}
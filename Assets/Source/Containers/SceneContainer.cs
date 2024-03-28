using EXRContainer.Core;
using UnityEngine;

namespace EXRContainer {
    [DefaultExecutionOrder(int.MinValue + 1)]
    public sealed class SceneContainer : MonoBehaviour {
        [SerializeField] private Installer[] installers;
        [SerializeField] private EntityContainer[] entityContainers;

        private static DIContainer source;

        private void Awake() {
            if (source != null) {
                Debug.LogError("Scene container can only be one on the scene!");
                Destroy(gameObject);
                return;
            }

            Initialize();
        }

        private void OnDestroy() {
            Debug.Log("The main dependency source is being cleaned up...");
            source.Dispose();
            source = null;
        }

        private void Initialize() {
            var settingsProvider = ProjectContainer.Resolve<ContainersSettingsProvider>();
            var codeGenerationConfig = ProjectContainer.Resolve<CodeGenerationConfiguration>();
            var dependenciesConfig = new DependenciesConfiguration(
                settingsProvider.GlobalContainerSettings.DefaultLifeTime, settingsProvider.GlobalContainerSettings.NonLazyCreation);

            var builder = CreateBuilder(dependenciesConfig, codeGenerationConfig);

            InstallDependencies(builder);

            source = builder.Build();
            InstallEntityContainers(settingsProvider.EntityContainerSettings, codeGenerationConfig);
        }

        private void InstallDependencies(ContainerBuilder builder) {
            builder.Register<SceneContainer>().FromInstance(this);

            if (installers == null) return;

            foreach (var monoInstaller in installers) {
                monoInstaller.Install(builder);
            }
        }

        private void InstallEntityContainers(EntityContainerSettings entityContainerSettings, CodeGenerationConfiguration codeGenerationConfig) {
            foreach (var entityContainer in entityContainers) {
                entityContainer.Install(source, entityContainerSettings, codeGenerationConfig);
            }
        }

        private ContainerBuilder CreateBuilder(DependenciesConfiguration dependenciesConfig, 
            CodeGenerationConfiguration codeGenerationConfig) {
            var parent = ProjectContainer.Resolve<DIContainer>();
            var builder = new ContainerBuilder(parent, dependenciesConfig, codeGenerationConfig);

            return builder;
        }
    }
}
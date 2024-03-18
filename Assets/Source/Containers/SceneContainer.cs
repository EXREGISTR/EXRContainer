using EXRContainer.Core;
using UnityEngine;

namespace EXRContainer {
    public sealed class SceneContainer : MonoBehaviour {
        [SerializeField] private MonoInstaller[] monoInstallers;
        [SerializeField] private ScriptableInstaller[] scriptableInstallers;
        [SerializeField] private EntityContainer[] entityContainers;

        private static DIContainer source;

        private void Awake() {
            var configuration = ProjectContainer.Resolve<ContainersConfiguration>();
            var codeGenerationConfig = ProjectContainer.Resolve<CodeGenerationConfiguration>();
            
            var builder = CreateBuilder(configuration, codeGenerationConfig);

            InstallDependencies(builder);

            source = builder.Build();
            InstallEntityContainers(codeGenerationConfig);
        }

        private void InstallDependencies(ContainerBuilder builder) {
            builder.Register<SceneContainer>().FromInstance(this);

            foreach (var monoInstaller in monoInstallers) {
                monoInstaller.Install(builder);
            }

            foreach (var scriptableInstaller in scriptableInstallers) {
                scriptableInstaller.Install(builder);
            }
        }

        private void InstallEntityContainers(CodeGenerationConfiguration codeGenerationConfig) {
            foreach (var entityContainer in entityContainers) {
                entityContainer.Install(source, codeGenerationConfig);
            }
        }

        private ContainerBuilder CreateBuilder(ContainersConfiguration configuration, CodeGenerationConfiguration codeGenerationConfig) {
            var dependenciesConfig = new DependenciesConfiguration(
                configuration.DefaultLifeTime, configuration.NonLazyCreation);
            
            var parent = ProjectContainer.Resolve<DIContainer>();
            var builder = new ContainerBuilder(parent, dependenciesConfig, codeGenerationConfig);

            return builder;
        }
    }
}
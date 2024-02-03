using System.Threading.Tasks;
using EXRContainer.Core;
using UnityEngine;

namespace EXRContainer {
    public sealed class SceneContainer : GlobalContainerProvider {
        private static DIContainer source;

        public override Task Initialize() {
            if (source != null) {
                Debug.LogError("Scene source already exist on this scene");
                Destroy(this);
                return Task.CompletedTask;
            }

            var task = Task.Run(InstallDependencies);

            Debug.Log("The scene container has been initialized!");
            return task;
        }

        private void InstallDependencies() {
            var builder = ProjectContainer.CreateBuilder();

            source = builder.Build();
        }

        private void OnDestroy() {
            Debug.Log("The main dependency source is being cleaned up...");
            source.Dispose();
            source = null;
        }

        public static ContainerBuilder CreateBuilder() => new ContainerBuilder(source);
    }
}
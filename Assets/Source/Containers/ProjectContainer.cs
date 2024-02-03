using System.Threading.Tasks;
using EXRContainer.Core;
using UnityEngine;

namespace EXRContainer {
    public sealed class ProjectContainer : GlobalContainerProvider {
        private static DIContainer source;

        public override Task Initialize() {
            if (source != null) {
                Debug.LogError("Project container can be only single!");
                Destroy(gameObject);
                return Task.CompletedTask;
            }

            DontDestroyOnLoad(gameObject);
            var task = Task.Run(InstallDependencies);
            Debug.Log("The project container has been initialized!");
            return task;
        }

        private void InstallDependencies() {
            var builder = new ContainerBuilder();
            builder.Register<ProjectContainer>().FromInstance(this);

            foreach (var installer in GetInstallers()) {
                installer.Install(builder);
            }

            source = builder.Build();
        }

        private void OnApplicationQuit() {
            Debug.Log("The main dependency source is being cleaned up...");
            source.Dispose();
            Debug.Log("The source has been cleaned!");
        }

        public static ContainerBuilder CreateBuilder() => new ContainerBuilder(source);
    }
}
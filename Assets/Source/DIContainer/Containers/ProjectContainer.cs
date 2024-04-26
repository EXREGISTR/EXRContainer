using EXRContainer.LambdaGeneration;
using EXRContainer.Core;
using System;
using UnityEngine;

namespace EXRContainer {
    [DefaultExecutionOrder(int.MinValue)]
    public sealed class ProjectContainer : MonoBehaviour {
        [SerializeField] private Installer[] installers;

        [SerializeField] private ContainersSettingsProvider settingsProvider;

        private static DIContainer source;

        static ProjectContainer() {
            DependencyTypeValidator.MakeInvalid<GameObject>();
        }

        private void Awake() {
            if (source != null) {
                Debug.LogError("Project container can only be one!");
                Destroy(gameObject);
                return;
            }

            Initialize();
            DontDestroyOnLoad(gameObject);
        }

        private void Initialize() {
            var codeGenerationConfig = CreateCodeGenerationConfig();
            var builder = CreateBuilder(codeGenerationConfig);

            InstallDependencies(builder, codeGenerationConfig);

            foreach (var monoInstaller in installers) {
                monoInstaller.Install(builder);
            }

            source = builder.Build();
        }

        private void OnDestroy() {
            Debug.Log("The main dependency source is being cleaned up...");
            source.Dispose();
            source = null;
        }

        private void InstallDependencies(ContainerBuilder builder, LambdasGenerationConfiguration codeGenerationConfig) {
            builder.Register<LambdasGenerationConfiguration>().FromInstance(codeGenerationConfig);
            builder.Register<ContainersSettingsProvider>().FromInstance(settingsProvider);
            builder.Register<ProjectContainer>().FromInstance(this);
        }

        public static T Resolve<T>() {
            if (source == null) {
                throw new NullReferenceException("Project container has not been created yet!");
            }

            return (T)source.Resolve(typeof(T));
        }

        private ContainerBuilder CreateBuilder(LambdasGenerationConfiguration codeGenerationConfig) {
            var dependenciesConfig = new DependenciesConfiguration(
                settingsProvider.GlobalContainerSettings.DefaultLifeTime, settingsProvider.GlobalContainerSettings.NonLazyCreation);
            var builder = new ContainerBuilder(null, dependenciesConfig, codeGenerationConfig);

            return builder;
        }

        private LambdasGenerationConfiguration CreateCodeGenerationConfig() {
            var factoryCreator = new FactoryGenerator(null);
            var finalizationCreator = new LambdaGenerator(null);

            ConfigurateLambdaCreators(factoryCreator, finalizationCreator);

            var data = new CodeGenerationConfiguration(factoryCreator, finalizationCreator);
            return data;
        }

        // ЗАКОНЧИТЬ БАЛЯ
        private void ConfigurateLambdaCreators(
            FactoryGenerator factoryCreator, 
            LambdaGenerator finalizationCreator) {

            var globalSettings = settingsProvider.GlobalContainerSettings;

            if (globalSettings.EventBus) {
                // factoryCreator.PostCreationProvider(new SubscribeOnEvents());
                // finalizationCreator.LastProvider(new UnsubscribeFromEvents());
            }

            if (globalSettings.UpdateCallbacks) {
                // factoryCreator.WithSuccessor(new UpdateSubscriber());
                // finalizationCreator.LastProvider(new UpdateUnsubscriber);
            }

            // finalizationCreator.LastProvider(new DisposeInvokation());
        }
    }
}
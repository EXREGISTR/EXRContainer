using EXRContainer.CodeGeneration;
using EXRContainer.CodeGeneration.Providers;
using EXRContainer.Core;
using System;
using UnityEngine;

namespace EXRContainer {
    [DefaultExecutionOrder(int.MinValue)]
    public sealed class ProjectContainer : MonoBehaviour {
        [SerializeField] private Installer[] installers;

        [SerializeField] private ContainersSettingsProvider settingsProvider;

        private static DIContainer source;

        [RuntimeInitializeOnLoadMethod]
        private static void BeforeInitialization() {
            DependencyTypeValidator.MakeInvalid<DependenciesContext>();
            DependencyTypeValidator.MakeInvalid<IDIContext>();
            DependencyTypeValidator.MakeInvalid<DIContainer>();
            DependencyTypeValidator.MakeInvalid<IDIContainer>();
            DependencyTypeValidator.MakeInvalid<SinglesStack>();
            DependencyTypeValidator.MakeInvalid<IDependency>();
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

        private void InstallDependencies(ContainerBuilder builder, CodeGenerationConfiguration codeGenerationConfig) {
            builder.Register<CodeGenerationConfiguration>().FromInstance(codeGenerationConfig);
            builder.Register<ContainersSettingsProvider>().FromInstance(settingsProvider);
            builder.Register<ProjectContainer>().FromInstance(this);
        }

        public static T Resolve<T>() {
            if (source == null) {
                throw new NullReferenceException("Project container has not been created yet!");
            }

            return (T)source.Resolve(typeof(T));
        }

        private ContainerBuilder CreateBuilder(CodeGenerationConfiguration codeGenerationConfig) {
            var dependenciesConfig = new DependenciesConfiguration(
                settingsProvider.GlobalContainerSettings.DefaultLifeTime, settingsProvider.GlobalContainerSettings.NonLazyCreation);
            var builder = new ContainerBuilder(null, dependenciesConfig, codeGenerationConfig);

            return builder;
        }

        private CodeGenerationConfiguration CreateCodeGenerationConfig() {
            var factoryCreator = new FactoryLambdaCreator(new CreationByConstructor());
            var finalizationCreator = new FinalizationLambdaCreator();

            ConfigurateLambdaCreators(factoryCreator, finalizationCreator);

            var data = new CodeGenerationConfiguration(factoryCreator, finalizationCreator);
            return data;
        }

        // ЗАКОНЧИТЬ БАЛЯ
        private void ConfigurateLambdaCreators(
            FactoryLambdaCreator factoryCreator, 
            FinalizationLambdaCreator finalizationCreator) {

            var globalSettings = settingsProvider.GlobalContainerSettings;

            if (globalSettings.EventBus) {
                factoryCreator.PostCreationProvider(new SubscribeOnEvents());
                finalizationCreator.LastProvider(new UnsubscribeFromEvents());
            }

            if (globalSettings.UpdateCallbacks) {
                // factoryCreator.WithSuccessor(new UpdateSubscriber());
                // finalizationCreator.LastProvider(new UpdateUnsubscriber);
            }

            // finalizationCreator.LastProvider(new DisposeInvokation());
        }
    }
}
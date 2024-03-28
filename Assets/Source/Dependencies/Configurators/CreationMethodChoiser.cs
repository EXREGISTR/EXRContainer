using EXRContainer.CodeGeneration.Providers;
using EXRContainer.Core;
using System;

namespace EXRContainer.Dependencies {
    public partial class DependencyConfigurator<TService> : ICreationMethodChoiser<TService> {
        public IWithoutCreationCallbacksChoiser<TService> FromComponentOnGameObject(bool inChildren = false) {
            var factoryCreator = codeGenerationData.CreateFactoryCreator(new CreationFromComponentOnGameObject());
            data.Factory = factoryCreator.Create();
        }

        public IArgumentsChoiser<TService> FromConstructor() {

            throw new NotImplementedException();
        }

        public IWithoutCreationCallbacksChoiser<TService> FromFactory(Factory<TService> factory) {
            data.Factory = factory;

            data.CreatorProvider = static data => {
                var lambdaCreator = data.CodeGenerationConfiguration.CreateFactoryCreator(
                    new CreationByCallback<TService>(data.Factory));
                return new FactoryDependencyCreator(data.LifeTime, data.ConcreteType, lambdaCreator);
            };

            return this;
        }

        public IWithoutCreationCallbacksCompleteChoiser<TService> FromInstance(TService service) {
            data.Instance = service;
            data.CreatorProvider = static data => new InstanceDependencyCreator(data.Instance);
            return this;
        }

        public IWithoutCreationCallbacksChoiser<TService> FromNewComponentOnGameObject() {
            throw new NotImplementedException();
        }

        public IWithoutCreationCallbacksChoiser<TService> FromPrefab(TService service) {
            throw new NotImplementedException();
        }
    }
}
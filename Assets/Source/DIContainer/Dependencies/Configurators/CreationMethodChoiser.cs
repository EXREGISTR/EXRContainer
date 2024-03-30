using EXRContainer.CodeGeneration.Providers;
using EXRContainer.Core;
using System;
using UnityEngine;

namespace EXRContainer.Dependencies {
    public partial class DependencyConfigurator<TService> : ICreationMethodChoiser<TService> {
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

        public ICallbacksChoiser<TService> FromComponentOnPrefab(GameObject prefab, bool findInChildren = false) {
            data.CreatorProvider = data => {
                var lambdaCreator = data.CodeGenerationConfiguration.CreateFactoryCreator(
                    new CreationFromComponentOnPrefab(prefab, createNewComponent: false, findInChildren));

                return new FactoryDependencyCreator(data.LifeTime, data.ConcreteType, lambdaCreator);
            };

            return this;
        }

        public ICallbacksChoiser<TService> FromNewComponentOnPrefab(GameObject prefab) {
            data.CreatorProvider = data => {
                var lambdaCreator = data.CodeGenerationConfiguration.CreateFactoryCreator(
                    new CreationFromComponentOnPrefab(prefab, createNewComponent: true, shouldFindInChildren: false));

                return new FactoryDependencyCreator(data.LifeTime, data.ConcreteType, lambdaCreator);
            };

            return this;
        }
    }
}
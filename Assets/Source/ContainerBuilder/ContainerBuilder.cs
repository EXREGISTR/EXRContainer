﻿using System;
using System.Collections.Generic;
using EXRContainer.Core;
using EXRContainer.Dependencies;

namespace EXRContainer {
    public sealed class ContainerBuilder {
        private readonly DIContainer parent;
        private readonly ContainerConfiguration config;

        private readonly List<IDependencyCreationData> dependenciesData;

        internal ContainerBuilder(DIContainer parent, ContainerConfiguration config) {
            this.parent = parent;
            this.config = config;
        }
        
        public IContractTypeChoiser<TService> Register<TService>() where TService : class {
            DependencyTypeValidator.ValidateServiceType(typeof(TService));

            var data = CreateDependencyData<TService>();
            var configurator = CreateConfigurator(data);
            configurator.ForSelf();
            return configurator;
        }

        #region DependencyRegistrationAPI

        public ISelfContractTypeChoiser<TService> Register<T1, TService>() where TService : class, T1 
            => Register<TService>(typeof(T1));

        public ISelfContractTypeChoiser<TService> Register<T1, T2, TService>() 
            where TService : class, T1, T2 
            => Register<TService>(typeof(T1), typeof(T2));

        public ISelfContractTypeChoiser<TService> Register<T1, T2, T3, TService>() 
            where TService : class, T1, T2, T3
            => Register<TService>(typeof(T1), typeof(T2), typeof(T3));

        public ISelfContractTypeChoiser<TService> Register<T1, T2, T3, T4, TService>() 
            where TService : class, T1, T2, T3, T4
            => Register<TService>(typeof(T1), typeof(T2), typeof(T3), typeof(T4));


        public ISelfContractTypeChoiser<TService> Register<T1, T2, T3, T4, T5, TService>() 
            where TService : class, T1, T2, T3, T4, T5
            => Register<TService>(typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5));


        public ISelfContractTypeChoiser<TService> Register<T1, T2, T3, T4, T5, T6, TService>() 
            where TService : class, T1, T2, T3, T4, T5, T6
            => Register<TService>(typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6));
        #endregion

        public DIContainer Build() {
            List<DependencyProvider> nonLazySingletons = null;
            List<DependencyProvider> scopedNonLazy = null;

            foreach (var data in dependenciesData) {
                var provider = CreateDependencyProvider(data);
                PlaceNonLazy(provider);
            }

            var container = new DIContainer();

            return container;

            void PlaceNonLazy(DependencyProvider provider) {
                if (provider.LifeTime == LifeTime.Singleton) {
                    nonLazySingletons ??= new List<DependencyProvider>();
                    nonLazySingletons.Add(provider);
                } else if (provider.LifeTime == LifeTime.Scoped) {
                    scopedNonLazy ??= new List<DependencyProvider>();
                    scopedNonLazy.Add(provider);
                }
            }
        }

        private DependencyProvider CreateDependencyProvider(IDependencyCreationData data) {
            var dependency = data.Creator.Create();

            var finalizator = data.Finalizator == null ?
                config.CodeGenerationData.DefaultFinalizatorCreator : 
                config.CodeGenerationData.CopyFinalizatorCreator.FirstProvider();

            var provider = new DependencyProvider(dependency, data.ContractTypes, data.LifeTime,
                data.OnResolveCallback, finalizator);
        }

        private DependencyConfigurator<TService> Register<TService>(params Type[] contractTypes) where TService : class {
            DependencyTypeValidator.ValidateServiceType(typeof(TService));
            DependencyTypeValidator.ValidateContractTypes(contractTypes);

            var data = CreateDependencyData<TService>();
            data.ContractTypes = new HashSet<Type>(contractTypes);
            return CreateConfigurator(data);
        }

        private DependencyConfigurator<TService> CreateConfigurator<TService>(DependencyCreationData<TService> data) where TService : class {
            return new DependencyConfigurator<TService>(data);
        }

        private DependencyCreationData<TService> CreateDependencyData<TService>() where TService : class {
            var data = new DependencyCreationData<TService> {
                LifeTime = config.DefaultLifeTime,
                NonLazy = config.NonLazyCreationByDefault,
            };

            return data;
        }
    }
}
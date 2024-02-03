using System;
using System.Collections.Generic;
using EXRContainer.Dependencies;
using EXRContainer.Utils;

namespace EXRContainer {
    public sealed class ContainerBuilder {
        private readonly ContainerConfiguration config;

        private readonly List<DependencyCreationData> dependenciesData;

        #region DependencyRegistrationAPI
        public ICreationMethodChoiser<TService> Register<TService>() where TService : class {
            ValidateServiceType<TService>();

            var data = GetDependencyData();
            return CreateConfigurator<TService>(data);
        }

        public ICreationMethodChoiser<TService> Register<T1, TService>() where TService : class, T1 
            => Register<TService>(typeof(T1));

        public ICreationMethodChoiser<TService> Register<T1, T2, TService>() 
            where TService : class, T1, T2 
            => Register<TService>(typeof(T1), typeof(T2));

        public ICreationMethodChoiser<TService> Register<T1, T2, T3, TService>() 
            where TService : class, T1, T2, T3
            => Register<TService>(typeof(T1), typeof(T2), typeof(T3));

        public ICreationMethodChoiser<TService> Register<T1, T2, T3, T4, TService>() 
            where TService : class, T1, T2, T3, T4
            => Register<TService>(typeof(T1), typeof(T2), typeof(T3), typeof(T4));


        public ICreationMethodChoiser<TService> Register<T1, T2, T3, T4, T5, TService>() 
            where TService : class, T1, T2, T3, T4, T5
            => Register<TService>(typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5));


        public ICreationMethodChoiser<TService> Register<T1, T2, T3, T4, T5, T6, TService>() 
            where TService : class, T1, T2, T3, T4, T5, T6
            => Register<TService>(typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6));
        #endregion

        private DependencyConfigurator<TService> Register<TService>(params Type[] contractTypes) where TService : class {
            ValidateServiceType<TService>();
            ValidateContractTypes(contractTypes);

            var data = GetDependencyData();
            data.ContractTypes.UnionWith(contractTypes);
            return CreateConfigurator<TService>(data);
        }

        private DependencyConfigurator<TService> CreateConfigurator<TService>(DependencyCreationData data) where TService : class {
            return new DependencyConfigurator<TService>(data, this);
        }

        private DependencyCreationData GetDependencyData() {
            
            
        }
        
        #region Validation
        private static void ValidateContractType(Type contract) {
            if (!EXRTypeHelper.IsAbstract(contract)) {
                throw new ArgumentException($"{contract.Name} type can not be contract, because it's not abstract class or interface!");
            }
        }

        private static void ValidateContractTypes(params Type[] types) {
            foreach (var contract in types) {
                ValidateContractType(contract);
            }
        }

        private static void ValidateServiceType<TService>() {
            
        }
        #endregion
    }
}
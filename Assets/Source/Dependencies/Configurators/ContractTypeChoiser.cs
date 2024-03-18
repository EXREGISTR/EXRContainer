using System;
using System.Collections.Generic;

namespace EXRContainer.Dependencies {
    public partial class DependencyConfigurator<TService> : IContractTypeChoiser<TService>, ISelfContractTypeChoiser<TService> {
        public ICreationMethodChoiser<TService> ForSelf() {
            data.ContractTypes ??= new HashSet<Type>();
            data.ContractTypes.Add(typeof(TService));
            return this;
        }

        public ICreationMethodChoiser<TService> ForInterfaces() {
            data.ContractTypes ??= new HashSet<Type>();
            data.ContractTypes.Clear();
            var interfaces = data.ConcreteType.GetInterfaces();

            for (int i = 0; i < interfaces.Length; i++) {
                var contract = interfaces[i];
                if (DependencyTypeValidator.TypeIsUsable(contract)) {
                    data.ContractTypes.Add(contract);
                }
            }

            return this;
        }

        public ICreationMethodChoiser<TService> ForInterfacesAndSelf() {
            ForInterfaces();
            ForSelf();
            return this;
        }
    }
}
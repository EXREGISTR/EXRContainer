using EXRContainer.Core;
using EXRContainer.Utils;

namespace EXRContainer.Dependencies {
    public partial class DependencyConfigurator<TService> : IContractTypeChoiser<TService>, ISelfContractTypeChoiser<TService> {
        public ICreationMethodChoiser<TService> ForSelf() {
            data.ContractTypes.Add(typeof(TService));
            return this;
        }

        public ICreationMethodChoiser<TService> ForInterfaces() {
            data.ContractTypes.Clear();
            var interfaces = data.ConcreteType.GetInterfaces();

            for (int i = 0; i < interfaces.Length; i++) {
                
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
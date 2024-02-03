namespace EXRContainer.Dependencies {
    public partial class DependencyConfigurator<TService> : IContractTypeChoiser<TService> {
        public ICreationMethodChoiser<TService> ForInterfaces() {
            
            return this;
        }

        public ICreationMethodChoiser<TService> ForInterfacesAndSelf() {
            return this;
        }
    }
}
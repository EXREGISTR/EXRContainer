namespace EXRContainer.Dependencies {
    public interface IContractTypeChoiser<TService> : ICreationMethodChoiser<TService> where TService : class {
        public ICreationMethodChoiser<TService> ForInterfaces();
        public ICreationMethodChoiser<TService> ForInterfacesAndSelf();
    }
}
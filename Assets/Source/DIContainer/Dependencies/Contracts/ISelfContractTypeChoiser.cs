namespace EXRContainer.Dependencies {
    public interface ISelfContractTypeChoiser<TService> : ICreationMethodChoiser<TService> where TService : class {
        public ICreationMethodChoiser<TService> ForSelf();
    }
}
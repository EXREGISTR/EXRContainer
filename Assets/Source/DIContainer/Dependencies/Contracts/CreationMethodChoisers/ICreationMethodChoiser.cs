using EXRContainer.Core;

namespace EXRContainer.Dependencies {
    public partial interface ICreationMethodChoiser<TService> : IArgumentsChoiser<TService> where TService : class {
        public IWithoutCreationCallbacksChoiser<TService> FromFactory(Factory<TService> factory);
        public IWithoutCreationCallbacksCompleteChoiser<TService> FromInstance(TService service);
    }
}
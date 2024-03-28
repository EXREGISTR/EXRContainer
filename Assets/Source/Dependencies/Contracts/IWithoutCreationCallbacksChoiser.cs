using EXRContainer.Core;

namespace EXRContainer.Dependencies {
    public interface IWithoutCreationCallbacksChoiser<TService> : ILazyCreationChoiser {
        public IWithoutCreationCallbacksChoiser<TService> OnResolve(OnResolve<TService> callback);
        public IWithoutCreationCallbacksChoiser<TService> OnFinalize(Finalizator<TService> callback);
    }

    public interface IWithoutCreationCallbacksCompleteChoiser<TService> {
        public IWithoutCreationCallbacksCompleteChoiser<TService> OnResolve(OnResolve<TService> callback);
        public IWithoutCreationCallbacksCompleteChoiser<TService> OnFinalize(Finalizator<TService> callback);
    }
}
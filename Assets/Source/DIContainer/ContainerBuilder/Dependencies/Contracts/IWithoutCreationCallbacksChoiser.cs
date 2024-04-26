using EXRContainer.Core;

namespace EXRContainer.Dependencies {
    public interface IWithoutCreationCallbacksChoiser<TService> : ILazyCreationChoiser {
        public IWithoutCreationCallbacksChoiser<TService> OnResolve(OnResolveCallback<TService> callback);
        public IWithoutCreationCallbacksChoiser<TService> OnFinalize(Finalizator<TService> callback);
    }

    public interface IWithoutCreationCallbacksCompleteChoiser<TService> {
        public IWithoutCreationCallbacksCompleteChoiser<TService> OnResolve(OnResolveCallback<TService> callback);
        public IWithoutCreationCallbacksCompleteChoiser<TService> OnFinalize(Finalizator<TService> callback);
    }
}
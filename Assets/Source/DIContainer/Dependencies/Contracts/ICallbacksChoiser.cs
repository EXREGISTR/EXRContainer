using EXRContainer.Core;

namespace EXRContainer.Dependencies {
    public interface ICallbacksChoiser<TService> : ILazyCreationChoiser {
        public ICallbacksChoiser<TService> PreInstantiate(PreCreationCallback callback);
        public ICallbacksChoiser<TService> PostInstantiate(PostCreationCallback<TService> callback);

        public ICallbacksChoiser<TService> OnResolve(OnResolve<TService> callback);
        public ICallbacksChoiser<TService> OnFinalize(Finalizator<TService> callback);
    }
}
using EXRContainer.Core;

namespace EXRContainer.Dependencies {
    public partial class DependencyConfigurator<TService> : ICallbacksChoiser<TService>, 
        IWithoutCreationCallbacksChoiser<TService>, IWithoutCreationCallbacksCompleteChoiser<TService> {
        
        public ICallbacksChoiser<TService> PostInstantiate(PostCreationCallback<TService> callback) {
            return this;
        }

        public ICallbacksChoiser<TService> PreInstantiate(PreCreationCallback callback) {
            return this;
        }

        public ICallbacksChoiser<TService> OnResolve(OnResolve<TService> callback) {
            return this;
        }

        public ICallbacksChoiser<TService> OnFinalize(Finalizator<TService> callback) {
            return this;
        }

        IWithoutCreationCallbacksChoiser<TService> IWithoutCreationCallbacksChoiser<TService>.OnResolve(OnResolve<TService> callback) {
            OnResolve(callback);
            return this;
        }

        IWithoutCreationCallbacksChoiser<TService> IWithoutCreationCallbacksChoiser<TService>.OnFinalize(Finalizator<TService> callback) {
            OnFinalize(callback);
            return this;
        }

        IWithoutCreationCallbacksCompleteChoiser<TService> IWithoutCreationCallbacksCompleteChoiser<TService>.OnResolve(OnResolve<TService> callback) {
            OnResolve(callback);
            return this;
        }

        IWithoutCreationCallbacksCompleteChoiser<TService> IWithoutCreationCallbacksCompleteChoiser<TService>.OnFinalize(Finalizator<TService> callback) {
            OnFinalize(callback);
            return this;
        }
    }
}
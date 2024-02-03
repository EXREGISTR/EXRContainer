using EXRContainer.Core;

namespace EXRContainer.Dependencies {
    public partial class DependencyConfigurator<TService> : ICallbacksChoiser<TService>, 
        IOnlyFinalizatorChoiser<TService>, IWithoutCreationCallbacksChoiser<TService> {
        public ICallbacksChoiser<TService> OnFinalize(Finalizator<TService> callback) {
            return this;
        }

        public ICallbacksChoiser<TService> PostInstantiate(OnInstantiatedCallback<TService> callback) {
            return this;
        }

        public ICallbacksChoiser<TService> PreInstantiate(PreCreationCallback callback) {
            return this;
        }

        ContainerBuilder IOnlyFinalizatorChoiser<TService>.OnFinalize(Finalizator<TService> callback) {

            return builder;
        }
    }
}
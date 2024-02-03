namespace EXRContainer.Core {
    internal class FactoryDependency : IDependency {
        private readonly PreCreationCallback preCreationCallback;
        private readonly Factory<object> factory;
        private readonly OnInstantiatedCallback<object> onInstantiatedCallback;

        public FactoryDependency(
            PreCreationCallback preCreationCallback,
            Factory<object> factory,
            OnInstantiatedCallback<object> onInstantiatedCallback) {
            this.preCreationCallback = preCreationCallback;
            this.factory = factory;
            this.onInstantiatedCallback = onInstantiatedCallback;
        }

        public object Resolve(IDIContext context) {
            preCreationCallback?.Invoke(context);
            var instance = factory(context);
            onInstantiatedCallback?.Invoke(context, instance);

            return instance;
        }
    }
}
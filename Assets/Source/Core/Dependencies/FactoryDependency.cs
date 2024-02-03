namespace EXRContainer.Core {
    internal class FactoryDependency : IDependency {
        private readonly PreCreationCallback preCreationCallback;
        private readonly Factory<object> factory;

        public FactoryDependency(
            PreCreationCallback preCreationCallback,
            Factory<object> factory) {
            this.preCreationCallback = preCreationCallback;
            this.factory = factory;
        }

        public object Resolve(IDIContext context) {
            preCreationCallback?.Invoke(context);
            var instance = factory(context);

            return instance;
        }
    }
}
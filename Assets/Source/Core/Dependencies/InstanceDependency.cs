namespace EXRContainer.Core {
    internal class InstanceDependency : IDependency {
        private readonly object instance;

        public InstanceDependency(object instance) {
            this.instance = instance;
        }

        public object Resolve(IDIContext _) => instance;
    }
}
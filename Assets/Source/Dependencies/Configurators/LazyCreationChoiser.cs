namespace EXRContainer.Dependencies {
    public partial class DependencyConfigurator<TService> : ILazyCreationChoiser {
        public ILifeTimeChoiser Lazy() {
            data.NonLazy = false;
            return this;
        }

        public INonTransientLifetimeChoiser NonLazy() {
            data.NonLazy = true;
            return this;
        }
    }
}
namespace EXRContainer.Dependencies {
    public partial class DependencyConfigurator<TService> : ILazyCreationChoiser {
        public ILifeTimeChoiser Lazy() {
            return this;
        }

        public INonTransientLifetimeChoiser NonLazy() {
            return this;
        }
    }
}
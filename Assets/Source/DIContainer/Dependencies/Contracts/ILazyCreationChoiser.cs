namespace EXRContainer.Dependencies {
    public interface ILazyCreationChoiser : ILifeTimeChoiser {
        public INonTransientLifetimeChoiser NonLazy();
        public ILifeTimeChoiser Lazy();
    }
}
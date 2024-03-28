namespace EXRContainer.Dependencies {
    public interface ILifeTimeChoiser : INonTransientLifetimeChoiser, ITransientLifetimeCompleteChoiser { }

    public interface INonTransientLifetimeChoiser 
        : IScopedLifetimeCompleteChoiser, ISingletonLifetimeCompleteChoiser { }

    public interface ISingletonLifetimeCompleteChoiser {
        public void AsSingleton();
    }

    public interface IScopedLifetimeCompleteChoiser {
        public void AsScoped();
    }

    public interface ITransientLifetimeCompleteChoiser {
        public void AsTransient();
    }
}
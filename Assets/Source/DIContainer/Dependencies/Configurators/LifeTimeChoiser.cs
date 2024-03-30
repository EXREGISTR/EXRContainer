using EXRContainer.Core;

namespace EXRContainer.Dependencies {
    public partial class DependencyConfigurator<TService> : ILifeTimeChoiser {
        public void AsScoped() {
            data.LifeTime = LifeTime.Scoped;
        }

        public void AsSingleton() {
            data.LifeTime = LifeTime.Singleton;
        }

        public void AsTransient() {
            data.LifeTime = LifeTime.Transient;
        }
    }
}
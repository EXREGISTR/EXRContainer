namespace EXRContainer.Dependencies {
    public partial class DependencyConfigurator<TService> : ILifeTimeChoiser {
        public ContainerBuilder AsScoped() {

            return builder;
        }

        public ContainerBuilder AsSingleton() {
            return builder;
        }

        public ContainerBuilder AsTransient() {
            return builder;
        }
    }
}
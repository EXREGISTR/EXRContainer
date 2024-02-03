namespace EXRContainer.Dependencies {
    public partial class DependencyConfigurator<TService> where TService : class {
        private readonly DependencyCreationData data;
        private readonly ContainerBuilder builder;

        internal DependencyConfigurator(DependencyCreationData data, ContainerBuilder builder) {
            this.data = data;
            this.builder = builder;
        }
    }
}
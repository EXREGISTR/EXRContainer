namespace EXRContainer.Dependencies {
    public partial class DependencyConfigurator<TService> where TService : class {
        private readonly DependencyCreationData data;

        internal DependencyConfigurator(DependencyCreationData data) {
            this.data = data;
        }
    }
}
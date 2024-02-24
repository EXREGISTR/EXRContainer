namespace EXRContainer.Dependencies {
    public partial class DependencyConfigurator<TService> where TService : class {
        private readonly DependencyCreationData<TService> data;

        internal DependencyConfigurator(DependencyCreationData<TService> data) {
            this.data = data;
        }
    }
}
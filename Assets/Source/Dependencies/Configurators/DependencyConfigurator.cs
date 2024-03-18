namespace EXRContainer.Dependencies {
    public partial class DependencyConfigurator<TService> where TService : class {
        private readonly DependencyCreationData<TService> data;
        private readonly CodeGenerationConfiguration codeGenerationData;

        internal DependencyConfigurator(DependencyCreationData<TService> data, CodeGenerationConfiguration codeGenerationData) {
            this.data = data;
            this.codeGenerationData = codeGenerationData;
        }
    }
}
namespace EXRContainer.Dependencies {
    internal partial class DependencyConfigurator<TService> where TService : class {
        private readonly DependencyCreationData<TService> data;
        private readonly LambdasGenerationConfiguration codeGenerationData;

        internal DependencyConfigurator(DependencyCreationData<TService> data, LambdasGenerationConfiguration codeGenerationData) {
            this.data = data;
            this.codeGenerationData = codeGenerationData;
        }
    }
}
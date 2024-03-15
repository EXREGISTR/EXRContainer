using EXRContainer.Core;

namespace EXRContainer.Dependencies {
    internal interface IDependencyCreator {
        public IDependency Create(CodeGenerationData data);
    }
}
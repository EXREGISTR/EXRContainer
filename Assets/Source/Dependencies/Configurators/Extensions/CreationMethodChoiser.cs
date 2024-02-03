using EXRContainer.Core;

namespace EXRContainer.Dependencies {
    public partial class DependencyConfigurator<TService> : ICreationMethodChoiser<TService> {
        public IArgumentsChoiser<TService> FromConstructor() {
            
            return this;
        }

        public IWithoutCreationCallbacksChoiser<TService> FromFactory(Factory<TService> factory) {


            return this;
        }

        public IWithoutCreationCallbacksCompleteChoiser<TService> FromInstance(TService service) {
            return this;
        }
    }
}
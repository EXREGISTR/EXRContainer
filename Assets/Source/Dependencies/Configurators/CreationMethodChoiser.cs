using EXRContainer.Core;
using System;

namespace EXRContainer.Dependencies {
    public partial class DependencyConfigurator<TService> : ICreationMethodChoiser<TService> {
        public IArgumentsChoiser<TService> FromConstructor() {

            throw new NotImplementedException();
        }

        public IWithoutCreationCallbacksChoiser<TService> FromFactory(Factory<TService> factory) {

            throw new NotImplementedException();
        }

        public IWithoutCreationCallbacksCompleteChoiser<TService> FromInstance(TService service) {
            throw new NotImplementedException();
        }
    }
}
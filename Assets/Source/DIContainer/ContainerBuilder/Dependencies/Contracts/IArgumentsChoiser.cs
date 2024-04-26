using System;
using System.Collections.Generic;

namespace EXRContainer.Dependencies {
    public interface IArgumentsChoiser<TService> : ICallbacksChoiser<TService> {
        internal ICallbacksChoiser<TService> RegisterArguments(params KeyValuePair<Type, object>[] arguments);
    }
}
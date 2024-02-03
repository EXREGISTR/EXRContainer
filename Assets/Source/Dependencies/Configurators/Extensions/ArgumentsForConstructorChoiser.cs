namespace EXRContainer.Dependencies {
    public partial class DependencyConfigurator<TService> : IArgumentsChoiser<TService> {
        public ICallbacksChoiser<TService> WithArguments<T>(T argument) {
            var type = typeof(T);


            return this;
        }

        public ICallbacksChoiser<TService> WithArguments<T1, T2>(T1 arg1, T2 arg2) {
            WithArguments(arg1);
            WithArguments(arg2);
            return this;
        }

        public ICallbacksChoiser<TService> WithArguments<T1, T2, T3>(T1 arg1, T2 arg2, T3 arg3) {
            WithArguments(arg1, arg2);
            WithArguments(arg3);
            return this;
        }

        public ICallbacksChoiser<TService> WithArguments<T1, T2, T3, T4>(T1 arg1, T2 arg2, T3 arg3, T4 arg4) {
            WithArguments(arg1, arg2, arg3);
            WithArguments(arg4);
            return this;
        }

        public ICallbacksChoiser<TService> WithArguments<T1, T2, T3, T4, T5>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5) {
            WithArguments(arg1, arg2, arg3, arg4);
            WithArguments(arg5);
            return this;
        }

        public ICallbacksChoiser<TService> WithArguments<T1, T2, T3, T4, T5, T6>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6) {
            WithArguments(arg1, arg2, arg3, arg4, arg5);
            WithArguments(arg6);
            return this;
        }
    }
}
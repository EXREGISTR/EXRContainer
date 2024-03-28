namespace EXRContainer.Dependencies {
    public interface IArgumentsChoiser<TService> : ICallbacksChoiser<TService> {
        public ICallbacksChoiser<TService> WithArguments<T>(T arg);
        public ICallbacksChoiser<TService> WithArguments<T1, T2>(T1 arg1, T2 arg2);
        public ICallbacksChoiser<TService> WithArguments<T1, T2, T3>(T1 arg1, T2 arg2, T3 arg3);
        public ICallbacksChoiser<TService> WithArguments<T1, T2, T3, T4>(T1 arg1, T2 arg2, T3 arg3, T4 arg4);
        public ICallbacksChoiser<TService> WithArguments<T1, T2, T3, T4, T5>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);
        public ICallbacksChoiser<TService> WithArguments<T1, T2, T3, T4, T5, T6>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6);
    }
}
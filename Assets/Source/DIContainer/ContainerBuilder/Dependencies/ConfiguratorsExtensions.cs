using System;
using System.Collections.Generic;

namespace EXRContainer.Dependencies {
    public static class ConfiguratorsExtensions {
        public static ICallbacksChoiser<TService> WithArguments<TService, T>(
            this IArgumentsChoiser<TService> source, T arg) 
            => source.RegisterArguments(new KeyValuePair<Type, object>(typeof(T), arg));

        public static ICallbacksChoiser<TService> WithArguments<TService, T1, T2>(
            this IArgumentsChoiser<TService> source, T1 arg1, T2 arg2)
            => source.RegisterArguments(
                new KeyValuePair<Type, object>(typeof(T1), arg1),
                new KeyValuePair<Type, object>(typeof(T1), arg2)
                );

        public static ICallbacksChoiser<TService> WithArguments<TService, T1, T2, T3>(
            this IArgumentsChoiser<TService> source, T1 arg1, T2 arg2, T3 arg3)
            => source.RegisterArguments(
                new KeyValuePair<Type, object>(typeof(T1), arg1),
                new KeyValuePair<Type, object>(typeof(T1), arg2),
                new KeyValuePair<Type, object>(typeof(T1), arg3)
                );

        public static ICallbacksChoiser<TService> WithArguments<TService, T1, T2, T3, T4>(
            this IArgumentsChoiser<TService> source, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
            => source.RegisterArguments(
                new KeyValuePair<Type, object>(typeof(T1), arg1),
                new KeyValuePair<Type, object>(typeof(T1), arg2),
                new KeyValuePair<Type, object>(typeof(T1), arg3),
                new KeyValuePair<Type, object>(typeof(T1), arg4)
                );

        public static ICallbacksChoiser<TService> WithArguments<TService, T1, T2, T3, T4, T5>(
            this IArgumentsChoiser<TService> source, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
            => source.RegisterArguments(
                new KeyValuePair<Type, object>(typeof(T1), arg1),
                new KeyValuePair<Type, object>(typeof(T1), arg2),
                new KeyValuePair<Type, object>(typeof(T1), arg3),
                new KeyValuePair<Type, object>(typeof(T1), arg4),
                new KeyValuePair<Type, object>(typeof(T1), arg5)
                );

        public static ICallbacksChoiser<TService> WithArguments<TService, T1, T2, T3, T4, T5, T6>(
            this IArgumentsChoiser<TService> source, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
            => source.RegisterArguments(
                new KeyValuePair<Type, object>(typeof(T1), arg1),
                new KeyValuePair<Type, object>(typeof(T1), arg2),
                new KeyValuePair<Type, object>(typeof(T1), arg3),
                new KeyValuePair<Type, object>(typeof(T1), arg4),
                new KeyValuePair<Type, object>(typeof(T1), arg5),
                new KeyValuePair<Type, object>(typeof(T1), arg6)
                );
    }
}
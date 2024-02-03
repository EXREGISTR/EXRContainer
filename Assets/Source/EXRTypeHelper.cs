using System;
using System.Runtime.CompilerServices;

namespace EXRContainer.Utils {
    public static class EXRTypeHelper {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsClass<T>() => IsDefaultClass(typeof(T));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsDefaultClass(Type type) => type.IsClass && !type.IsAbstract;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsAbstract<T>() => IsAbstract(typeof(T));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsAbstract(Type type) => type.IsInterface || type.IsAbstract;
    } 
}
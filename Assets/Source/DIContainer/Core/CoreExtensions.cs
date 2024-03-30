using System;
using EXRContainer.Core;

namespace EXRContainer {
    public static class CoreExtensions {
        public static void AddDependency<T>(this IDIContext source, T instance) {
            source.AddDependency(instance, null);
        }

        public static void AddDependency<T, TI1>(this IDIContext source, T instance) where T : TI1 {
            source.AddDependencyInternal(instance, typeof(T), typeof(TI1));
        }

        public static void AddDependency<T, TI1, TI2>(this IDIContext source, T instance) where T : TI1, TI2 {
            source.AddDependencyInternal(instance, typeof(T), typeof(TI1), typeof(TI2));
        }

        public static void AddDependency<T, TI1, TI2, TI3>(this IDIContext source, T instance) where T : TI1, TI2, TI3 {
            source.AddDependencyInternal(instance, typeof(T), typeof(TI1), typeof(TI2), typeof(TI3));
        }

        public static void AddDependency<T, TI1, TI2, TI3, TI4>(this IDIContext source, T instance) where T : TI1, TI2, TI3, TI4 {
            source.AddDependencyInternal(instance, typeof(T), typeof(TI1), typeof(TI2), typeof(TI3), typeof(TI4));
        }

        private static void AddDependencyInternal(this IDIContext source, object instance, params Type[] contractTypes) {
            source.AddDependency(instance, contractTypes);
        }
    }
}

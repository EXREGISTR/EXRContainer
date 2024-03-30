using System;
using System.Collections.Generic;

namespace EXRContainer {
    internal static class DependencyTypeValidator {
        private static readonly HashSet<Type> invalidTypes = new();

        public static void MakeInvalid<T>() where T : class => invalidTypes.Add(typeof(T));

        public static void ValidateContractType(Type contract) {
            AssertTypeForUsable(contract);

            if (!IsAbstract(contract)) {
                throw new ArgumentException(
                    $"{contract.Name} type can not be contract, because it's not abstract type!");
            }
        }

        public static void ValidateContractTypes(params Type[] types) {
            foreach (var contract in types) {
                ValidateContractType(contract);
            }
        }

        public static void ValidateServiceType(Type type) {
            AssertTypeForUsable(type);
            if (IsAbstract(type)) {
                throw new ArgumentException($"{type.Name} can not be service type, because it's abstract type!");
            }
        }

        public static void AssertTypeForUsable(Type type) {
            if (invalidTypes.Contains(type)) {
                throw new ArgumentException($"{type.Name} can not be used, because it's invalid type!");
            }
        }

        public static bool TypeIsUsable(Type type) => !invalidTypes.Contains(type);

        private static bool IsAbstract(Type type) => type.IsInterface || type.IsAbstract;
    }
}
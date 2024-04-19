using System.Reflection;
using System;
using UnityEngine;
using System.Linq.Expressions;
using System.Linq;

namespace EXRContainer.LambdaGeneration {
    public class ConstructorCreation : IDependencyInitializationProvider {
        public void InitializeDependency(IDependencyInitializator provider) {
            var dependencyType = provider.DependencyData.Type;
            ValidateType(dependencyType);

            var constructor = GetConstructor(dependencyType);

            var argumentsResolving
                = ExpressionsHelper.DescribeResolving(provider.ContextParameter, constructor.GetParameters());

            var newExpression = Expression.New(constructor, argumentsResolving);
            provider.AssignDependency(newExpression);
        }

        private ConstructorInfo GetConstructor(Type dependencyType) {
            var constructors = dependencyType.GetConstructors(BindingFlags.Public | BindingFlags.Instance);

            ConstructorInfo constructor = constructors
                .Where(constructor => constructor.GetCustomAttribute<EXRInitializatorAttribute>(inherit: false) != null)
                .FirstOrDefault();

            constructor ??= constructors.First();
            return constructor;
        }

        private void ValidateType(Type type) {
            if (type.IsAssignableFrom(typeof(MonoBehaviour))) {
                throw new ArgumentException(
                    $"Impossible generate creation from constructor for MonoBehaviour type {type}");
            }

            if (type.IsAssignableFrom(typeof(ScriptableObject))) {
                throw new ArgumentException(
                    $"Impossible generate creation from constructor for ScriptableObject type {type}");
            }
        }
    }
}

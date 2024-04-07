using System.Reflection;
using System;
using System.Linq.Expressions;
using System.Linq;

namespace EXRContainer.LambdaGeneration {
    public class InitializationMethodInvokation : IExpressionsProvider {
        public void Execute(IGenerationContext context) {
            var dependencyType = context.DependencyData.Type;
            var method = FindMethod(dependencyType);

            var argumentsResolving 
                = ExpressionsHelper.DescribeResolving(context.ContextParameter, method.GetParameters());
            var invokationExpression = Expression.Invoke(Expression.Constant(method), argumentsResolving);

            context.AppendExpression(invokationExpression);
        }

        private MethodInfo FindMethod(Type dependencyType) {
            var methods = dependencyType.GetMethods(BindingFlags.Instance);
            var method = methods.FirstOrDefault(x => x.GetCustomAttribute<EXRConstructorAttribute>() != null)
                ?? throw new Exception($"Not found initialization method for type {dependencyType}");
            
            return method;
        }

        public void RegisterVariables(IContextVariablesRegistrator registrator) { }
    }
}

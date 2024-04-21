using EXRContainer.LambdaGeneration;
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace EXRContainer.LambdaGeneration {
    internal class DisposeInvokation : IExpressionsProvider {
        private static readonly MethodInfo disposeMethod = typeof(IDisposable).GetMethod("Dispose");

        public void RegisterExpressions(IGenerationContext context) {
            if (context.DependencyData.Type.GetInterface(nameof(IDisposable)) != null) {
                context.AppendExpression(Expression.Call(context.DependencyInstance, disposeMethod));
            }
        }
    }
}
using EXRContainer.Core;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace EXRContainer.CodeGeneration.Providers {
    internal class PreCreationInvokation : IExpressionsProvider {
        private readonly PreCreationCallback callback;

        public PreCreationInvokation(PreCreationCallback callback) {
            this.callback = callback;
        }

        public IEnumerable<Expression> GenerateCode(ExecutionContext context) {
            var expression = Expression.Constant(callback);
            var invokationExpression = Expression.Invoke(expression, context.ContextParameter);
            yield return invokationExpression;
        }

        public IEnumerable<ParameterExpression> GetVariables() => Enumerable.Empty<ParameterExpression>();
    }
}

using EXRContainer.Core;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace EXRContainer.CodeGeneration.Providers {
    public class CreationByCallback<TService> : ICreationExpressionsProvider where TService : class {
        private readonly Factory<TService> factory;

        public CreationByCallback(Factory<TService> factory) {
            this.factory = factory;
        }

        public IEnumerable<Expression> GenerateCode(ExecutionContext context) {
            var factoryInvokeExpression = Expression.Invoke(Expression.Constant(factory), context.ContextParameter); 
            var instantiateExpression = Expression.Assign(context.TypedInstanceVariable, factoryInvokeExpression);

            // generated:
            // preCreationCallback(context);
            // instance = factory(context);

            yield return instantiateExpression;
        }

        public IEnumerable<ParameterExpression> GetVariables() => Enumerable.Empty<ParameterExpression>();
    }
}
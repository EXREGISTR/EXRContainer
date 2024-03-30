using System.Collections.Generic;
using System.Linq.Expressions;

namespace EXRContainer.CodeGeneration.Providers {
    internal class SubscribeOnEvents : IExpressionsProvider {
        public IEnumerable<Expression> GenerateCode(ExecutionContext context) {
            throw new System.NotImplementedException();
        }

        public IEnumerable<ParameterExpression> GetVariables() {
            throw new System.NotImplementedException();
        }
    }

    internal class UnsubscribeFromEvents : IExpressionsProvider {
        public IEnumerable<Expression> GenerateCode(ExecutionContext context) {
            throw new System.NotImplementedException();
        }

        public IEnumerable<ParameterExpression> GetVariables() {
            throw new System.NotImplementedException();
        }
    }
}
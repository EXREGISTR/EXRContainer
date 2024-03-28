using EXRContainer.CodeGeneration;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace EXRContainer.Dependencies {
    internal class CreationFromComponentOnGameObject : ICreationExpressionsProvider {
        public IEnumerable<Expression> GenerateCode(ExecutionContext context) {
            throw new System.NotImplementedException();
        }

        public IEnumerable<ParameterExpression> GetVariables() {
            throw new System.NotImplementedException();
        }
    }
}
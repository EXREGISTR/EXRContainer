using System.Collections.Generic;
using System.Linq.Expressions;

namespace EXRContainer.CodeGeneration {
    public interface ICreationExpressionsProvider : IExpressionsProvider { }

    public interface IExpressionsProvider {
        public IEnumerable<ParameterExpression> GetVariables();
        public IEnumerable<Expression> GenerateCode(ExecutionContext context);
    }
}
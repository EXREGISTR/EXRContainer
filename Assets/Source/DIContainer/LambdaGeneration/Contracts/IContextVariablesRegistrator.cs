using System.Linq.Expressions;

namespace EXRContainer.LambdaGeneration {
    public interface IContextVariablesRegistrator {
        public void WithVariableFromContext(ParameterExpression variable);
        public void WithVariable(ParameterExpression variable, Expression assignExpression);
    }
}

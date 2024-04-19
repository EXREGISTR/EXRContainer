using System.Linq.Expressions;

namespace EXRContainer.LambdaGeneration {
    public interface IVariablesRegistrator {
        public void RegisterVariableFromContext(ParameterExpression variable);
        public void RegisterVariable(ParameterExpression variable, Expression assignExpression);
    }
}

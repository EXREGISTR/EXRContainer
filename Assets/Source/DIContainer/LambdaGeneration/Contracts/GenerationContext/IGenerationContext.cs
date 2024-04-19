using System;
using System.Linq.Expressions;

namespace EXRContainer.LambdaGeneration {
    public interface IGenerationContext {
        public ParameterExpression ContextParameter { get; }
        public ParameterExpression DependencyInstance { get; }
        public DependencyGenerationData DependencyData { get; }

        public ParameterExpression Find(Predicate<ParameterExpression> predicate);
        public void AppendExpression(Expression expression);
    }
}

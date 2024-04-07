using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace EXRContainer.LambdaGeneration {
    public class GenerationContext : IGenerationContext, IContextVariablesRegistrator {
        private readonly List<Expression> expressions;
        private readonly List<ParameterExpression> variables;

        public ParameterExpression ContextParameter { get; }
        public ParameterExpression DependencyInstance { get; }
        public DependencyGenerationData DependencyData { get; }

        public GenerationContext(
            List<Expression> expressions, 
            List<ParameterExpression> variables,
            ParameterExpression contextParameter,
            ParameterExpression dependencyInstance,
            in DependencyGenerationData data) {
            this.expressions = expressions;
            this.variables = variables;

            ContextParameter = contextParameter;
            DependencyInstance = dependencyInstance;
            DependencyData = data;
        }

        public ParameterExpression Find(Predicate<ParameterExpression> predicate) => variables.Find(predicate);

        public void AppendExpression(Expression expression) => expressions.Add(expression);

        public void WithVariableFromContext(ParameterExpression variable)
            => WithVariable(variable, ExpressionsHelper.DescribeResolving(ContextParameter, variable.Type));

        public void WithVariable(ParameterExpression variable, Expression assignExpression) {
            variables.Add(variable);
            expressions.Add(Expression.Assign(variable, assignExpression));
        }
    }
}

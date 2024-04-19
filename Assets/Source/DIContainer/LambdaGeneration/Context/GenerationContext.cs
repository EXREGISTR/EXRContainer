using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace EXRContainer.LambdaGeneration {
    public class GenerationContext : IGenerationContext, IVariablesRegistrator, IDependencyInitializator {
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

        public void AppendExpression(Expression expression) {
            if (expression.NodeType == ExpressionType.Assign && expression is BinaryExpression binary) {
                if (binary.Left is MemberExpression member) {
                    if (member.Member.Name == DependencyInstance.Name) {
                        throw new Exception("You cannot assign a dependency value if the invoker is not an initializator");
                    }
                }
            }

            expressions.Add(expression);
        }

        public void RegisterVariableFromContext(ParameterExpression variable)
            => RegisterVariable(variable, ExpressionsHelper.DescribeResolving(ContextParameter, variable.Type));

        public void RegisterVariable(ParameterExpression variable, Expression assignExpression) {
            variables.Add(variable);
            expressions.Add(Expression.Assign(variable, assignExpression));
        }

        public void AssignDependency(Expression assignExpression) => expressions.Add(Expression.Assign(DependencyInstance, assignExpression));
    }
}

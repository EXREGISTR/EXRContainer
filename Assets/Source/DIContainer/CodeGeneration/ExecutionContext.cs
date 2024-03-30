using System;
using System.Linq.Expressions;
using EXRContainer.Core;

namespace EXRContainer.CodeGeneration {
    public readonly struct ExecutionContext {
        public ParameterExpression ContextParameter { get; }
        public ParameterExpression TypedInstanceVariable { get; }
        public Type DependencyType { get; }
        public LifeTime LifeTime { get; }

        public ExecutionContext(
            ParameterExpression contextParameter,
            ParameterExpression typedInstanceVariable, 
            Type dependencyType,
            LifeTime lifeTime) {
            ContextParameter = contextParameter;
            TypedInstanceVariable = typedInstanceVariable;
            DependencyType = dependencyType;
            LifeTime = lifeTime;
        }
    }
}
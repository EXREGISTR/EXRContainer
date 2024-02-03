﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using EXRContainer.Core;

namespace EXRContainer.CodeGeneration {
    internal class InstantiationLambdaCreator {
        private readonly IEnumerable<IExpressionsProvider> providers;

        public InstantiationLambdaCreator(IEnumerable<IExpressionsProvider> providers) {
            this.providers = providers;
        }

        public Factory<object> Create(Type dependencyType, LifeTime lifeTime) {
            var instanceVariable = Expression.Variable(dependencyType, $"{dependencyType.Name}");

            var variables = new List<ParameterExpression> {
                instanceVariable
            };

            var contextParameter = ExpressionsCache.ContextParameter;
            var expressions = new List<Expression>();

            var context = new ExecutionContext(contextParameter, instanceVariable, dependencyType, lifeTime);

            foreach (var provider in providers) {
                variables.AddRange(provider.GetVariables());
                expressions.AddRange(provider.GenerateCode(context));
            }

            expressions.Add(instanceVariable);
            // generated:
            // return instance;

            var block = Expression.Block(variables, expressions);
            var lambda = Expression.Lambda<Factory<object>>(block, contextParameter);

            return lambda.Compile();
        }
    }
}
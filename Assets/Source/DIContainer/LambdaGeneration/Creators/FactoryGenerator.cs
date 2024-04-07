using EXRContainer.Core;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace EXRContainer.LambdaGeneration {
    public class FactoryGenerator : ILambdaCreator<Factory<object>> {
        private readonly LinkedList<IExpressionsProvider> providers;
        private readonly LinkedListNode<IExpressionsProvider> creationProviderNode;

        public FactoryGenerator(IDependencyInitializator provider) {
            providers = new LinkedList<IExpressionsProvider>();
            creationProviderNode = providers.AddFirst(provider);
        }

        public FactoryGenerator(IDependencyInitializator initializatorProvider, FactoryGenerator other) {
            providers = new LinkedList<IExpressionsProvider>();

            foreach (var provider in other.providers) {
                if (creationProviderNode == null && provider == other.creationProviderNode.Value) {
                    creationProviderNode = providers.AddLast(initializatorProvider);
                } else {
                    providers.AddLast(provider);
                }
            }
        }

        public FactoryGenerator(FactoryGenerator other) {
            providers = new LinkedList<IExpressionsProvider>();

            foreach (var provider in other.providers) {
                var node = providers.AddLast(provider);

                if (creationProviderNode == null && provider == other.creationProviderNode.Value) {
                    creationProviderNode = node;
                }
            }
        }

        public void BeforeCreationProvider(IExpressionsProvider provider) 
            => providers.AddBefore(creationProviderNode, provider);

        public void PostCreationProvider(IExpressionsProvider provider) 
            => providers.AddAfter(creationProviderNode, provider);

        public Factory<object> Execute(DependencyGenerationData data) {
            Type dependencyType = data.Type;

            ParameterExpression contextParameter = ExpressionsCache.ContextParameter;
            ParameterExpression dependencyInstance = Expression.Variable(dependencyType, $"{dependencyType.Name}");

            var expressions = new List<Expression>();
            var variables = new List<ParameterExpression> { dependencyInstance };

            GenerationContext context = new GenerationContext(expressions, variables, contextParameter, dependencyInstance, data);
            
            RegisterVariables(context);
            ExecuteGeneration(context);

            return GenerateLambda(expressions, variables, contextParameter);
        }

        private Factory<object> GenerateLambda(IEnumerable<Expression> expressions, 
            IEnumerable<ParameterExpression> variables, ParameterExpression contextParameter) {
            var block = Expression.Block(variables, expressions);
            var lambda = Expression.Lambda<Factory<object>>(block, contextParameter);
            return lambda.Compile();
        }

        private void ExecuteGeneration(IGenerationContext context) {
            foreach (var provider in providers) {
                provider.Execute(context);
            }

            // generated:
            // return dependency;
            context.AppendExpression(context.DependencyInstance);
        }

        private void RegisterVariables(IContextVariablesRegistrator registrator) {
            foreach (var provider in providers) {
                provider.RegisterVariables(registrator);
            }
        }
    }
}

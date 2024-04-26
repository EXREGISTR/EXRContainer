using EXRContainer.Core;
using EXRContainer.LambdaGeneration;
using System;

namespace EXRContainer {
    internal class LambdasService : ILambdasContainer {
        private readonly LambdasGenerationConfiguration config;

        private FactoryGenerationExecutor factoryExecutor;
        private LambdaGenerationExecutor finalizatorExecutor;
        private LambdaGenerationExecutor onResolveCallbackExecutor;

        public LambdasService(LambdasGenerationConfiguration config) {
            this.config = config;
        }

        public void EditFactoryGeneration(Action<FactoryGenerationExecutor> editor) {
            factoryExecutor ??= config.CreateFactoryExecutor();
            editor(factoryExecutor);
        }

        public void EditFinalizatorGeneration(Action<LambdaGenerationExecutor> editor) {
            finalizatorExecutor ??= config.CreateFinalizatorExecutor();
            editor(finalizatorExecutor);
        }
        
        public void EditOnResolveGeneration(Action<LambdaGenerationExecutor> editor) {
            onResolveCallbackExecutor ??= config.CreateOnResolveCallbackExecutor();
            editor(onResolveCallbackExecutor);
        }

        public Factory<object> GetFactory(in DependencyGenerationData data) {
            var executor = factoryExecutor;
            executor ??= config.CreateFactoryExecutor();
            return FactoryGenerator.Execute(executor, data);
        }

        public Finalizator<object> GetFinalizator(in DependencyGenerationData data) {
            var executor = finalizatorExecutor;
            executor ??= config.CreateFinalizatorExecutor();
            return LambdaGenerator<Finalizator<object>>.Execute(executor, data);
        }

        public OnResolveCallback<object> GetOnResolveCallback(in DependencyGenerationData data) {
            var executor = onResolveCallbackExecutor;
            executor ??= config.CreateOnResolveCallbackExecutor();
            return LambdaGenerator<OnResolveCallback<object>>.Execute(executor, data);
        }
    }
}
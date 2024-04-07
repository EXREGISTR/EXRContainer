using System.Linq.Expressions;
using EXRContainer.Events;

namespace EXRContainer.LambdaGeneration {
    public class SubscribeToEventsService : IExpressionsProvider {
        private static readonly ParameterExpression eventBus 
            = Expression.Parameter(typeof(EventsService), "eventsService");

        public void Execute(IGenerationContext context) {
        }

        public void RegisterVariables(IContextVariablesRegistrator registrator) 
            => registrator.WithVariableFromContext(eventBus);
    }
}

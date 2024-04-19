using System.Linq.Expressions;
using EXRContainer.Events;

namespace EXRContainer.LambdaGeneration {
    public class SubscribeToEventsService : IExpressionsProvider, IVariablesRegistrationProvider {
        private static readonly ParameterExpression eventBus 
            = Expression.Parameter(typeof(EventsService), "eventsService");

        public void RegisterExpressions(IGenerationContext context) {
        }

        public void RegisterVariables(IVariablesRegistrator registrator) 
            => registrator.RegisterVariableFromContext(eventBus);
    }
}

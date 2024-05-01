using System;

namespace EXRContainer.Events {
    public interface IProcessorsContainer {
        public void Subscribe<T>(IMessageProcessor<T> processor, Predicate<T> condition = null) where T : IMessage;
        public void Subscribe<T>(ICommandProcessor<T> processor) where T : ICommand;
        public void Subscribe<TRequest, TResult>(IRequestProcessor<TRequest, TResult> processor) 
            where TRequest : IRequest<TResult>;
        
        public void Unsubscribe<T>(IMessageProcessor<T> processor) where T : IMessage;
        public void Unsubscribe<T>(ICommandProcessor<T> processor) where T : ICommand; 
        public void Unsubscribe<TRequest, TResult>(IRequestProcessor<TRequest, TResult> processor) 
            where TRequest : IRequest<TResult>;
    }
}

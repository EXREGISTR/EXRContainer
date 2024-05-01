using System;
using System.Collections.Generic;

namespace EXRContainer.Events {
    internal class MessagingService : IMediator, IProcessorsContainer, IDisposable {
        private Dictionary<Type, IProcessor> processors;
        private Dictionary<Type, IProcessorsCollection> messageProcessorsCollections;

        #region ProcessorsRegistration
        public void Subscribe<T>(IMessageProcessor<T> processor, Predicate<T> condition = null) where T : IMessage {
            var messageType = typeof(T);
            messageProcessorsCollections ??= new();
            var wrapper = new MessageProcessors<T>.ProcessorWrapper(processor, condition ?? (_ => true));

            MessageProcessors<T> processors = messageProcessorsCollections.TryGetValue(messageType, out var collection)
                ? (MessageProcessors<T>)collection
                : new MessageProcessors<T>();

            processors.Push(wrapper);
        }

        public void Subscribe<T>(ICommandProcessor<T> processor) where T : ICommand
            => SubscribeInternal(typeof(T), processor);

        public void Subscribe<TMessage, TResult>(IRequestProcessor<TMessage, TResult> processor) where TMessage : IRequest<TResult> 
            => SubscribeInternal(typeof(TMessage), processor);

        public void Unsubscribe<T>(IMessageProcessor<T> processor) where T : IMessage {
            var messageType = typeof(T);
            if (!messageProcessorsCollections.TryGetValue(messageType, out var processors)) return;
            ((MessageProcessors<T>)processors).Delete(processor);
        }

        public void Unsubscribe<T>(ICommandProcessor<T> processor) where T : ICommand 
            => UnsubscribeInternal(typeof(T), processor);

        public void Unsubscribe<TRequest, TResult>(IRequestProcessor<TRequest, TResult> processor) where TRequest : IRequest<TResult>
            => UnsubscribeInternal(typeof(TRequest), processor);

        private void SubscribeInternal(Type messageType, IProcessor processor) {
            processors ??= new Dictionary<Type, IProcessor>();

            if (processors.TryGetValue(messageType, out var other)) {
                throw new AlreadyRegisteredProcessorForMessage(messageType, other);
            }

            processors[messageType] = processor;
        }

        private void UnsubscribeInternal(Type messageType, IProcessor processor) {
            if (!processors.TryGetValue(messageType, out var other)) return;
            if (other != processor) {
                throw new InvalidOperationException(
                    $"You can't delete the handler {other} because the passed link {processor} does not match it");
            }

            processors[messageType] = null;
        }
        #endregion

        #region Notifications
        public void Notify<T>(T message) where T : IMessage {
            var messageType = typeof(T);
            if (!messageProcessorsCollections.TryGetValue(messageType, out var processors)) {
                throw new NoProcessorsException(messageType);
            }

            ((MessageProcessors<T>)processors).Notify(message);
        }

        public void Execute<T>(T command) where T : ICommand {
            var commandType = typeof(T);
            if (!processors.TryGetValue(commandType, out var processor)) {
                throw new NoProcessorsException(commandType);
            }

            ((ICommandProcessor<T>)processor).Execute(command);
        }

        public TResult Send<TRequest, TResult>(TRequest request) where TRequest : IRequest<TResult> {
            var requestType = typeof(TRequest);
            if (!processors.TryGetValue(requestType, out var processor)) {
                throw new NoProcessorsException(requestType);
            }

            return ((IRequestProcessor<TRequest, TResult>)processor).Execute(request);
        }
        #endregion

        public void Dispose() {
            if (messageProcessorsCollections != null) {
                foreach (var processors in messageProcessorsCollections.Values) {
                    processors.Dispose();
                }

                messageProcessorsCollections.Clear();
                messageProcessorsCollections = null;
            }

            if (processors != null) {
                processors.Clear();
                processors = null;
            }
        }
    }
}

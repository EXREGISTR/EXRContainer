using System;
using System.Collections.Generic;

namespace EXRContainer.Events {
    public class MessageHasNotHandlers : Exception {
        public MessageHasNotHandlers(Type messageType) : 
            base($"Message {messageType} has not registered handlers!") { }
    }

    public sealed class EventsService : IDisposable {
        private class HandlersCollectionWrapper {
            public IMessageHandlersCollection Collection { get; }
            
        }

        private readonly Dictionary<Type, IMessageHandlersCollection> messageHandlers = new();

        public void Subscribe<T>(IMessageHandler<T> handler) where T : IMessage {
            var messageType = typeof(T);
            if (messageHandlers.TryGetValue(messageType, out var list)) {
                ((MessageHandlersCollection<T>)list).Push(handler);
                return;
            }

            var handlers = new MessageHandlersCollection<T>();
            handlers.Push(handler);

            messageHandlers[messageType] = handlers;
        }

        public void Unsubscribe<T>(IMessageHandler<T> handler) where T : IMessage {
            var messageType = typeof(T);
            if (!messageHandlers.TryGetValue(messageType, out var list)) return;

            ((MessageHandlersCollection<T>)list).Delete(handler);
        }

        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        /// <exception cref="MessageHasNotHandlers"></exception>
        public void Notify<T>(T message, bool throwIfNoHandlers = true) where T : IMessage {
            var messageHandlers = GetMessageHandlers<T>(throwIfNoHandlers);
            if (messageHandlers == null) return;

            messageHandlers.Notify(message);
        }

        public IMessageHandlersCollection<T> GetMessageHandlers<T>(bool createIfNoHandlers = false,
            bool throwIfNoHandlers = true) where T : IMessage {
            var messageType = typeof(T);
            if (!messageHandlers.TryGetValue(messageType, out var list)) {
                if (createIfNoHandlers) {
                    var handlers = new MessageHandlersCollection<T>();
                    messageHandlers[messageType] = handlers;
                    return handlers;
                }

                return throwIfNoHandlers ? throw new MessageHasNotHandlers(messageType) : null;
            }

            return (IMessageHandlersCollection<T>)list;
        }

        public void Dispose() {
            foreach (var collection in messageHandlers.Values) {
                collection.Clear();
            }
        }
    }
}
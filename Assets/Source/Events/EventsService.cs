using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace EXRContainer.Events {
    public class MessageHasNotHandlers : Exception {
        public MessageHasNotHandlers(Type messageType) : 
            base($"Message {messageType} has not registered handlers!") { }
    }

    public sealed class EventsService : IDisposable {
        private readonly Dictionary<Type, IMessageHandlersList> messageHandlers;

        public void RegisterHandler<TMessage>(IMessageHandler<TMessage> handler) {
            var messageType = typeof(TMessage);
            if (messageHandlers.TryGetValue(messageType, out var list)) {
                ConvertCollection<TMessage>(list).Push(handler);
                return;
            }

            var handlers = new MessageHandlersList<TMessage>();
            handlers.Push(handler);

            messageHandlers[messageType] = handlers;
        }

        public void UnregisterHandler<TMessage>(IMessageHandler<TMessage> handler) {
            var messageType = typeof(TMessage);
            if (!messageHandlers.TryGetValue(messageType, out var list)) return;

            ConvertCollection<TMessage>(list).Delete(handler);
        }

        /// <typeparam name="TMessage"></typeparam>
        /// <param name="message"></param>
        /// <exception cref="MessageHasNotHandlers"></exception>
        public void SendMessage<TMessage>(TMessage message) {
            var messageType = typeof(TMessage);
            if (!messageHandlers.TryGetValue(messageType, out var list)) {
                throw new MessageHasNotHandlers(messageType);
            }

            ConvertCollection<TMessage>(list).Send(message);
        }

        public void Dispose() {
            foreach (var collection in messageHandlers.Values) {
                collection.Clear();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static MessageHandlersList<T> ConvertCollection<T>(IMessageHandlersList list) 
            => (MessageHandlersList<T>)list;
    }
}
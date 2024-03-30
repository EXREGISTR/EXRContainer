using System.Collections.Generic;

namespace EXRContainer.Events {
    internal class MessageHandlersList<TMessage> : IMessageHandlersList {
        private readonly HashSet<IMessageHandler<TMessage>> handlers = new();

        public void Push(IMessageHandler<TMessage> handler) => handlers.Add(handler);

        public void Delete(IMessageHandler<TMessage> handler) => handlers.Remove(handler);

        public void Send(TMessage message) {
            foreach (var handler in handlers) {
                handler.OnReceiveMessage(message);
            }
        }

        public void Clear() => handlers.Clear();
    }
}
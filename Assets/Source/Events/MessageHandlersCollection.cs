using System.Collections;
using System.Collections.Generic;

namespace EXRContainer.Events {
    internal class MessageHandlersCollection<T> 
        : IMessageHandlersCollection<T>, IMessageHandlersCollection where T : IMessage {
        private readonly HashSet<IMessageHandler<T>> handlers = new();

        public bool IsEmpty => handlers.Count == 0;

        public void Push(IMessageHandler<T> handler) => handlers.Add(handler);

        public void Notify(T message) {
            foreach (var handler in handlers) {
                handler.Notify(message);
            }
        }

        public void Delete(IMessageHandler<T> handler) => handlers.Remove(handler);

        public void Clear() => handlers.Clear();

        public IEnumerator<IMessageHandler<T>> GetEnumerator() => handlers.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    }
}
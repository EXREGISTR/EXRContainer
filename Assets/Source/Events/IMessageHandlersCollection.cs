using System.Collections.Generic;

namespace EXRContainer.Events {
    public interface IMessageHandlersCollection {
        public void Clear();
    }

    public interface IMessageHandlersCollection<T> : IEnumerable<IMessageHandler<T>> where T : IMessage {
        public void Notify(T message);
        public bool IsEmpty { get; }
    }
}
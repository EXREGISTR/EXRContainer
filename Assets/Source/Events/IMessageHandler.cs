namespace EXRContainer.Events {
    public interface IMessageHandler<T> {
        public void OnReceiveMessage(T message);
    }
}

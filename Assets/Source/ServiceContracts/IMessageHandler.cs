namespace EXRContainer {
    public interface IMessageHandler<T> {
        public void OnReceiveMessage(T message);
    }
}

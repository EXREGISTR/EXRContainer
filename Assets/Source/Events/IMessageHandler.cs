namespace EXRContainer.Events {

    public interface IMessageHandler<T> {
        public void Notify(T message);
    }
}

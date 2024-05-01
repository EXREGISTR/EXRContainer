namespace EXRContainer.Events {
    public interface IMessageProcessor<TMessage> : IProcessor where TMessage : IMessage {
        public void Process(TMessage message);
    }

    public interface IMessage { }
}
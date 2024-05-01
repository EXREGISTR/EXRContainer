namespace EXRContainer.Events {
    public interface IMediator {
        public void Notify<T>(T message) where T : IMessage;
        public void Execute<T>(T command) where T : ICommand;
        public TResult Send<TRequest, TResult>(TRequest request) where TRequest : IRequest<TResult>;
    }  
}
namespace EXRContainer.Events {
    public interface IRequestProcessor<TRequest, TResult> : IProcessor where TRequest : IRequest<TResult> {
        public TResult Execute(TRequest request);
    }

    public interface IRequest<TResult> { }
}

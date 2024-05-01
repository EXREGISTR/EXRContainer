namespace EXRContainer.Events {
    public interface ICommandProcessor<TCommand> : IProcessor where TCommand : ICommand {
        public void Execute(TCommand command);
    }

    public interface ICommand { }
}
namespace EXRContainer.LambdaGeneration {
    public interface IExpressionsProvider {
        public void Execute(IGenerationContext context);
        public void RegisterVariables(IContextVariablesRegistrator registrator);
    }
}

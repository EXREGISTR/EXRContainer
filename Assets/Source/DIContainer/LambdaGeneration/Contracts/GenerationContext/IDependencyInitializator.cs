using System.Linq.Expressions;

namespace EXRContainer.LambdaGeneration {
    public interface IDependencyInitializator {
        public DependencyGenerationData DependencyData { get; }
        public ParameterExpression ContextParameter { get; }
        public void AssignDependency(Expression assignExpression);
    }
}
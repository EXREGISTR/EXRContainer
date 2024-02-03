namespace EXRContainer.Core {
    public interface IDependency {
        public object Resolve(IDIContext context);
    }
}

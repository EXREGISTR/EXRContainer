namespace EXRContainer.Dependencies {
    public partial interface ICreationMethodChoiser<TService> where TService : class {
        public IWithoutCreationCallbacksChoiser<TService> FromNewComponentOnGameObject();
        public IWithoutCreationCallbacksChoiser<TService> FromComponentOnGameObject(bool inChildren = false);
        public IWithoutCreationCallbacksChoiser<TService> FromPrefab(TService service);
    }
}
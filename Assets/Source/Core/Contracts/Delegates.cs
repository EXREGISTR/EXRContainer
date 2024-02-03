namespace EXRContainer.Core {
    public delegate TService Factory<out TService>(IDIContext context) where TService : class;
    public delegate void Finalizator<in TService>(IDIContext context, TService service);

    public delegate void OnResolve<TService>(TService service);
    public delegate void OnInstantiatedCallback<in TService>(IDIContext context, TService service);
    public delegate void PreCreationCallback(IDIContext context);
    
}
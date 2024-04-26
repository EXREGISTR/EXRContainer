namespace EXRContainer.Core {
    public delegate TService Factory<out TService>(IDIContext context) where TService : class;
    public delegate void Finalizator<in TService>(IDIContext context, TService service);

    public delegate void OnResolveCallback<in TService>(IDIContext context, TService service);
    public delegate void PostCreationCallback<in TService>(IDIContext context, TService service);
    public delegate void PreCreationCallback(IDIContext context);
    
}
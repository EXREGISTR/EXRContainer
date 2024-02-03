using EXRContainer.Core;

namespace EXRContainer.Dependencies {
    // If the method returns ContainerBuilder, then the configuration is complete.

    public interface ILifeTimeChoiser : INonTransientLifetimeChoiser, ITransientLifetimeCompleteChoiser { }

    public interface INonTransientLifetimeChoiser : IScopedLifetimeCompleteChoiser, ISingletonLifetimeCompleteChoiser { }

    public interface ISingletonLifetimeCompleteChoiser {
        public ContainerBuilder AsSingleton();
    }

    public interface IScopedLifetimeCompleteChoiser {
        public ContainerBuilder AsScoped();
    }

    public interface ITransientLifetimeCompleteChoiser {
        public ContainerBuilder AsTransient();
    }

    public interface ILazyCreationChoiser : ILifeTimeChoiser {
        public INonTransientLifetimeChoiser NonLazy();
        public ILifeTimeChoiser Lazy();
    }

    public interface IWithoutCreationCallbacksChoiser<TService> : IOnlyFinalizatorChoiser<TService>, ILazyCreationChoiser { 
    
    }

    public interface IWithoutCreationCallbacksCompleteChoiser<TService> {
        public IWithoutCreationCallbacksCompleteChoiser<TService> OnResolve(OnResolve<TService> callback);
        public IWithoutCreationCallbacksCompleteChoiser<TService> OnFinalize(Finalizator<TService> callback);
    }

    public interface ICallbacksChoiser<TService> : ILazyCreationChoiser {
        public ICallbacksChoiser<TService> PreInstantiate(PreCreationCallback callback);
        public ICallbacksChoiser<TService> PostInstantiate(OnInstantiatedCallback<TService> callback);

        public ICallbacksChoiser<TService> OnResolve(OnResolve<TService> callback);
        public ICallbacksChoiser<TService> OnFinalize(Finalizator<TService> callback);
    }

    public interface IOnlyResolveCallbackChoiser<TService> {
        
    }

    public interface IOnlyFinalizatorChoiser<TService> {
    }

    public interface IArgumentsChoiser<TService> : ICallbacksChoiser<TService> {
        public ICallbacksChoiser<TService> WithArguments<T>(T arg);
        public ICallbacksChoiser<TService> WithArguments<T1, T2>(T1 arg1, T2 arg2);
        public ICallbacksChoiser<TService> WithArguments<T1, T2, T3>(T1 arg1, T2 arg2, T3 arg3);
        public ICallbacksChoiser<TService> WithArguments<T1, T2, T3, T4>(T1 arg1, T2 arg2, T3 arg3, T4 arg4);
        public ICallbacksChoiser<TService> WithArguments<T1, T2, T3, T4, T5>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);
        public ICallbacksChoiser<TService> WithArguments<T1, T2, T3, T4, T5, T6>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6);

    }

    public interface ICreationMethodChoiser<TService> : IArgumentsChoiser<TService> where TService : class {
        public IArgumentsChoiser<TService> FromConstructor();
        public IWithoutCreationCallbacksChoiser<TService> FromFactory(Factory<TService> factory);
        public IOnlyFinalizatorChoiser<TService> FromInstance(TService service);
    }

    public interface IContractTypeChoiser<TService> : ICreationMethodChoiser<TService> where TService : class {
        public ICreationMethodChoiser<TService> ForInterfaces();
        public ICreationMethodChoiser<TService> ForInterfacesAndSelf();
    }
}
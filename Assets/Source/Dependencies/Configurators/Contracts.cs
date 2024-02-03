using EXRContainer.Core;

namespace EXRContainer.Dependencies {
    public interface ILifeTimeChoiser : INonTransientLifetimeChoiser, ITransientLifetimeCompleteChoiser { }

    public interface INonTransientLifetimeChoiser : IScopedLifetimeCompleteChoiser, ISingletonLifetimeCompleteChoiser { }

    public interface ISingletonLifetimeCompleteChoiser {
        public void AsSingleton();
    }

    public interface IScopedLifetimeCompleteChoiser {
        public void AsScoped();
    }

    public interface ITransientLifetimeCompleteChoiser {
        public void AsTransient();
    }

    public interface ILazyCreationChoiser : ILifeTimeChoiser {
        public INonTransientLifetimeChoiser NonLazy();
        public ILifeTimeChoiser Lazy();
    }

    public interface IWithoutCreationCallbacksChoiser<TService> : ILazyCreationChoiser {
        public IWithoutCreationCallbacksChoiser<TService> OnResolve(OnResolve<TService> callback);
        public IWithoutCreationCallbacksChoiser<TService> OnFinalize(Finalizator<TService> callback);
    }

    public interface ICallbacksChoiser<TService> : ILazyCreationChoiser {
        public ICallbacksChoiser<TService> PreInstantiate(PreCreationCallback callback);
        public ICallbacksChoiser<TService> PostInstantiate(PostCreationCallback<TService> callback);

        public ICallbacksChoiser<TService> OnResolve(OnResolve<TService> callback);
        public ICallbacksChoiser<TService> OnFinalize(Finalizator<TService> callback);
    }

    public interface IWithoutCreationCallbacksCompleteChoiser<TService> {
        public IWithoutCreationCallbacksCompleteChoiser<TService> OnResolve(OnResolve<TService> callback);
        public IWithoutCreationCallbacksCompleteChoiser<TService> OnFinalize(Finalizator<TService> callback);
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
        public IWithoutCreationCallbacksCompleteChoiser<TService> FromInstance(TService service);
    }

    public interface ISelfContractTypeChoiser<TService> : ICreationMethodChoiser<TService> where TService : class {
        public ICreationMethodChoiser<TService> ForSelf();
    }

    public interface IContractTypeChoiser<TService> : ICreationMethodChoiser<TService> where TService : class {
        public ICreationMethodChoiser<TService> ForInterfaces();
        public ICreationMethodChoiser<TService> ForInterfacesAndSelf();
    }
}
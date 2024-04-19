using EXRContainer.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EXRContainer.Core {
    public class DIContainer : IDIContainer, IDIContext {
        private readonly Dictionary<Type, DependencyProvider> dependencies;
        private readonly SinglesStack singletons;
        private readonly Dictionary<Type, object> dynamicAddedObjects;
        private readonly IEnumerable<DependencyProvider> nonLazyScopedDependencies;

        private Dictionary<IDIContext, SinglesStack> contexts;
        private bool disposedValue;

        static DIContainer() {
            DependencyTypeValidator.MakeInvalid<DependenciesContext>();
            DependencyTypeValidator.MakeInvalid<IDIContext>();
            DependencyTypeValidator.MakeInvalid<DIContainer>();
            DependencyTypeValidator.MakeInvalid<IDIContainer>();
            DependencyTypeValidator.MakeInvalid<IDependency>();
        }

        internal DIContainer(Dictionary<Type, DependencyProvider> dependencies, 
            IEnumerable<DependencyProvider> nonLazySingletons,
            IEnumerable<DependencyProvider> nonLazyScopedDependencies,
            DIContainer parent = null) {
            if (parent == null) {
                this.dependencies = dependencies;
            } else {
                var mergedDependencies = parent.dependencies.Union(dependencies);
                this.dependencies = new Dictionary<Type, DependencyProvider>(mergedDependencies);
            }
            
            this.singletons = new SinglesStack(this);
            this.nonLazyScopedDependencies = nonLazyScopedDependencies;
            this.dynamicAddedObjects = new Dictionary<Type, object>();
            RegisterContainer();

            CreateNonLazySingletons(nonLazySingletons);
        }

        private void RegisterContainer() {
            dynamicAddedObjects[typeof(DIContainer)] = this;
            dynamicAddedObjects[typeof(IDIContext)] = this;
            dynamicAddedObjects[typeof(IDIContainer)] = this;
        }

        private void CreateNonLazySingletons(IEnumerable<DependencyProvider> nonLazySingletons) {
            foreach (var provider in nonLazySingletons) {
                var single = provider.Source.Resolve(this);
                singletons.PushSingle(single, provider.Finalizator, provider.ContractTypes);
            }
        }

        public IDIContext CreateContext() {
            var context = new DependenciesContext(this);
            contexts ??= new Dictionary<IDIContext, SinglesStack>();

            if (nonLazyScopedDependencies == null) {
                contexts[context] = null;
            } else {
                contexts[context] = CreateScopedObjectsStorage(context);
            }

            return context;
        }

        private SinglesStack CreateScopedObjectsStorage(IDIContext context) {
            var scopedObjects = new SinglesStack(context);
            foreach (var provider in nonLazyScopedDependencies) {
                var scoped = provider.Source.Resolve(context);
                scopedObjects.PushSingle(scoped, provider.Finalizator, provider.ContractTypes);
            }

            return scopedObjects;
        }

        public object Resolve(IDIContext context, Type dependencyType) {
            DependencyProvider provider;
            try {
                provider = FindProvider(dependencyType);
            } catch (NoDependencyException exception) {
                if (dynamicAddedObjects.TryGetValue(dependencyType, out object single)) {
                    return single;
                }

                single = singletons.FindSingle(dependencyType);
                if (single != null) return single;

                throw exception;
            }

            object instance = provider.LifeTime switch {
                LifeTime.Singleton => ResolveSingleton(provider),
                LifeTime.Scoped => ResolveScoped(context, provider),
                LifeTime.Transient => provider.Source.Resolve(context),
                _ => throw new ArgumentOutOfRangeException(nameof(provider.LifeTime)),
            };

            provider.OnResolveCallback?.Invoke(instance);
            return instance;
        }

        public void Release(IDIContext context, object instance) {
            var type = instance.GetType();
            DependencyProvider provider = FindProvider(type);

            switch (provider.LifeTime) {
                case LifeTime.Singleton:
                    ReleaseObject(this, singletons, instance, provider);
                    break;
                case LifeTime.Scoped:
                    SinglesStack scopedObjects = FindScopedObjects(context);
                    ReleaseObject(context, scopedObjects, instance, provider);
                    break;
                case LifeTime.Transient:
                    provider.Finalizator?.Invoke(this, instance);
                    break;
                default:
                    break;
            }
        }

        public void AddDependency<T>(T instance, IEnumerable<Type> contractTypes) {
            if (contractTypes == null || !contractTypes.Any()) {
                AddDynamicObject(typeof(T), instance);
                return;
            }

            foreach (var type in contractTypes) {
                AddDynamicObject(type, instance);
            }
        }

        public object Resolve(Type dependencyType) => Resolve(this, dependencyType);

        public void Delete(object instance) => Release(this, instance);

        public void DeleteContext(IDIContext context) {
            if (contexts == null || !contexts.TryGetValue(context, out SinglesStack singles)) {
                throw new NoAccessToContextException(context);
            }

            singles.Dispose();
            contexts.Remove(context);
        }

        internal void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void AddDynamicObject(Type type, object instance) {
            DependencyTypeValidator.AssertTypeForUsable(type);
            var result = dynamicAddedObjects.TryAdd(type, instance);
            if (!result) throw new AlreadyExistSingleException(type);
        }

        private object ResolveScoped(IDIContext context, in DependencyProvider provider) {
            SinglesStack scopedObjects = FindScopedObjects(context);
            
            if (scopedObjects != null) {
                var created = scopedObjects.FindSingle(provider.ContractTypes);
                if (created != null) {
                    return created;
                }
            } else {
                scopedObjects = new SinglesStack(context);
            }

            var scoped = provider.Source.Resolve(context);
            scopedObjects.PushSingle(scoped, provider.Finalizator, provider.ContractTypes);

            return scoped;
        }

        private object ResolveSingleton(in DependencyProvider provider) {
            var singleton = singletons.FindSingle(provider.ContractTypes);
            singleton ??= PushSingleton(provider);

            return singleton;
        }

        private object PushSingleton(in DependencyProvider provider) {
            var singleton = provider.Source.Resolve(this);
            singletons.PushSingle(singleton, provider.Finalizator, provider.ContractTypes);
            return singleton;
        }

        private void ReleaseObject(IDIContext context, SinglesStack singles, object instance, in DependencyProvider provider) {
            if (singles == null) {
                provider.Finalizator?.Invoke(this, instance);
                return;
            }

            var founded = singles.DeleteSingle(provider.ContractTypes);
            if (founded == null) {
                provider.Finalizator?.Invoke(this, instance);
                return;
            }

            // if founded is not instance, so will be finalized founded object 
            object objectToFinalize = founded != instance ? founded : instance;
            provider.Finalizator?.Invoke(context, objectToFinalize);
        }

        private SinglesStack FindScopedObjects(IDIContext context) {
            if (!contexts.TryGetValue(context, out var scopedObjects)) {
                throw new NoAccessToContextException(context);
            }

            return scopedObjects;
        }

        private DependencyProvider FindProvider(Type type) {
            if (!dependencies.TryGetValue(type, out var provider)) {
                throw new NoDependencyException(type);
            }

            return provider;
        }

        private void Dispose(in bool _) {
            if (disposedValue) return;

            singletons.Dispose();

            if (contexts == null) return;

            foreach (var context in contexts.Values) {
                context.Dispose();
            }

            contexts.Clear();
            contexts = null;

            disposedValue = true;
        }
    }
}

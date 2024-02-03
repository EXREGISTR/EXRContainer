using System;
using System.Collections.Generic;
using System.Linq;

namespace EXRContainer.Core {
    public class DIContainer : IDIContainer, IDIContext {
        private readonly Dictionary<Type, DependencyProvider> dependencies;
        private readonly ContainerData data;
        private readonly SinglesStack singletons;

        private Dictionary<IDIContext, SinglesStack> contexts;
        private bool disposedValue;

        internal DIContainer(Dictionary<Type, DependencyProvider> dependencies, 
            ContainerData data,
            DIContainer parent = null) {
            if (parent == null) {
                this.dependencies = dependencies;
            } else {
                var mergedDependencies = parent.dependencies.Union(dependencies);
                this.dependencies = new Dictionary<Type, DependencyProvider>(mergedDependencies);
            }
            
            this.data = data;
            this.singletons = new SinglesStack(this);
            this.AddDependency<DIContainer, IDIContainer, IDIContext>(this);

            CreateNonLazySingletons();
        }

        private void CreateNonLazySingletons() {
            var dependencies = data.NonLazySingletonsDependencies;
            if (dependencies == null) return;

            foreach (var provider in dependencies) {
                var single = provider.Source.Resolve(this);
                singletons.PushSingle(single, provider.Finalizator, provider.ContractTypes);
            }
        }

        public IDIContext CreateContext() {
            var context = new DependenciesContext(this);
            contexts ??= new Dictionary<IDIContext, SinglesStack>();
            IEnumerable<DependencyProvider> dependencies = data.NonLazyScopedDependencies;

            context.AddDependency(context);

            if (dependencies != null) {
                CreateNonLazyScopedDependencies(context, dependencies);
            } else {
                contexts[context] = null;
            }

            return context;
        }

        private void CreateNonLazyScopedDependencies(IDIContext context, IEnumerable<DependencyProvider> dependencies) {
            var scopedObjects = new SinglesStack(this);
            foreach (var provider in dependencies) {
                var scoped = provider.Source.Resolve(context);
                scopedObjects.PushSingle(scoped, provider.Finalizator, provider.ContractTypes);
            }

            contexts[context] = scopedObjects;
        }

        public object Resolve(IDIContext context, Type dependencyType) {
            DependencyProvider provider;
            try {
                provider = FindProvider(dependencyType);
            } catch (NoDependencyException exception) {
                if (singletons != null) {
                    var single = singletons.FindSingle(dependencyType);
                    if (single != null) return single;
                }

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
                singletons.PushSingle(instance, typeof(T));
                return;
            } 

            singletons.PushSingle(instance, null, contractTypes);
        }

        public object Resolve(Type dependencyType) => Resolve(this, dependencyType);

        public void Delete(object instance) => Release(this, instance);

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
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
                provider.Finalizator?.Invoke(null, instance);
                return;
            }

            var founded = singles.DeleteSingle(provider.ContractTypes);
            if (founded == null) {
                provider.Finalizator?.Invoke(null, instance);
                return;
            }

            object objectToFinalize = founded != instance ? founded : instance;
            provider.Finalizator?.Invoke(context, objectToFinalize);
        }

        public void DeleteContext(IDIContext context) {
            if (contexts == null || !contexts.TryGetValue(context, out SinglesStack singles)) {
                throw new NoAccessToContextException(context);
            }

            singles.Dispose();
            contexts.Remove(context);
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

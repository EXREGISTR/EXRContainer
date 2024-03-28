using EXRContainer.CodeGeneration;
using EXRContainer.Core;
using System;

namespace EXRContainer.Dependencies {
    public class FactoryDependencyCreator : IDependencyCreator {
        private readonly LifeTime lifeTime;
        private readonly Type dependencyType;
        private readonly FactoryLambdaCreator lambdaCreator;

        internal FactoryDependencyCreator(LifeTime lifeTime, Type dependencyType, FactoryLambdaCreator lambdaCreator) {
            this.lifeTime = lifeTime;
            this.dependencyType = dependencyType;
            this.lambdaCreator = lambdaCreator;
        }

        public IDependency Create() {
            // lambdaCreator.Create();
            // var dependency = new FactoryDependency();
            return null;
        }
    }

    public class InstanceDependencyCreator : IDependencyCreator {
        private readonly object instance;

        public InstanceDependencyCreator(object instance) {
            this.instance = instance;
        }

        public IDependency Create() {
            return new InstanceDependency(instance);
        }
    }

    internal interface IDependencyCreator {
        public IDependency Create();
    }
}
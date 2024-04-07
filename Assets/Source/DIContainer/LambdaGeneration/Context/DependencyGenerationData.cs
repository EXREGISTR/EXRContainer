using EXRContainer.Core;
using System;

namespace EXRContainer.LambdaGeneration {
    public readonly struct DependencyGenerationData {
        public Type Type { get; }
        public LifeTime LifeTime { get; }

        public DependencyGenerationData(Type type, LifeTime lifeTime) {
            Type = type;
            LifeTime = lifeTime;
        }
    }
}

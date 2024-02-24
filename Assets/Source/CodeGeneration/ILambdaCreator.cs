using System;
using EXRContainer.Core;

namespace EXRContainer.CodeGeneration {
    public interface ILambdaCreator<T> where T : MulticastDelegate {
        public T Create(Type dependencyType, LifeTime lifeTime);
    }
}
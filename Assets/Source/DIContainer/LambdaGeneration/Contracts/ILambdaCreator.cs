using System;

namespace EXRContainer.LambdaGeneration {
    public interface ILambdaCreator<TDelegate> where TDelegate : MulticastDelegate {
        public TDelegate Execute(DependencyGenerationData data);
    }
}
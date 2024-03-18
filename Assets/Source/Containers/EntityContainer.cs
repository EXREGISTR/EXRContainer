using EXRContainer.Core;
using UnityEngine;

namespace EXRContainer {
    public abstract class EntityContainer : MonoBehaviour {
        internal void Install(DIContainer parent, CodeGenerationConfiguration codeGenerationConfiguration) {

        }

        protected abstract void Install(ContainerBuilder builder);
    }
}
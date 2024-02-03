using UnityEngine;

namespace EXRContainer {
    public abstract class MonoInstaller : MonoBehaviour, IInstaller {
        public abstract void Install(ContainerBuilder builder);
    }
}
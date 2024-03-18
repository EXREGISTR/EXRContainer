using UnityEngine;

namespace EXRContainer {
    public abstract class ScriptableInstaller : ScriptableObject {
        public abstract void Install(ContainerBuilder builder);
    }
}
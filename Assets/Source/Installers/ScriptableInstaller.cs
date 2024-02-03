using UnityEngine;

namespace EXRContainer {
    public abstract class ScriptableInstaller : MonoBehaviour {
        public abstract void Install(ContainerBuilder builder);
    }
}
using UnityEngine;

namespace EXRContainer {
    public abstract class Installer : MonoBehaviour {
        public abstract void Install(ContainerBuilder builder);
    }
}
using UnityEngine;

namespace EXRContainer {
    public abstract class Installer : MonoBehaviour {
        internal abstract void Install(ContainerBuilder builder);
    }
}
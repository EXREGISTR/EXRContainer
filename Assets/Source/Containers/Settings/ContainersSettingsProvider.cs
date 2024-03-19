using UnityEngine;

namespace EXRContainer {
    [CreateAssetMenu]
    public class ContainersSettingsProvider : ScriptableObject {
        [field: SerializeField] public GlobalContainerSettings GlobalContainerSettings { get; private set; }
        [field: Space(10)]
        [field: SerializeField] public EntityContainerSettings EntityContainerSettings { get; private set; }
    }
}
using EXRContainer.Core;
using UnityEngine;

namespace EXRContainer {
    [CreateAssetMenu]
    public class ContainersConfiguration : ScriptableObject {
        [field: SerializeField] public GlobalContainerSettings GlobalContainerSettings { get; private set; }
        [field: Space(5)]
        [field: SerializeField] public EntityContainerSettings EntityContainerSettings { get; private set; }
        [field: Space(5)]
        [field: SerializeField] public LifeTime DefaultLifeTime { get; private set; } = LifeTime.Scoped;
        [field: Tooltip("Create dependency non lazy by default?")]
        [field: SerializeField] public bool NonLazyCreation { get; private set; }
    }
}
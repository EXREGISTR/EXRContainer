using EXRContainer.Core;
using UnityEngine;

namespace EXRContainer {
    public abstract class BaseContainerSettings {
        [field: SerializeField] public LifeTime DefaultLifeTime { get; private set; } = LifeTime.Scoped;
        [field: Tooltip("Create dependency non lazy by default?")]
        [field: SerializeField] public bool NonLazyCreation { get; private set; }
    }
}
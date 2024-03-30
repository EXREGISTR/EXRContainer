using System;
using UnityEngine;

namespace EXRContainer {
    [Serializable]
    public class EntityContainerSettings : BaseContainerSettings {
        [field: Header("Auto subscribe on:")]
        [field: SerializeField] public bool TriggerCallbacks { get; private set; } = true;
        [field: SerializeField] public bool CollisionCallbacks { get; private set; } = true;
    }
}
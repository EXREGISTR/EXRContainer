using System;
using UnityEngine;

namespace EXRContainer {
    [Serializable]
    public struct EntityContainerSettings {
        [field: Header("Auto subscribe on:")]
        [field: SerializeField] public bool TriggerCallbacks { get; private set; }
        [field: SerializeField] public bool CollisionCallbacks { get; private set; }
    }
}
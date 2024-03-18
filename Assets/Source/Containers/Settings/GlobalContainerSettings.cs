using System;
using UnityEngine;

namespace EXRContainer {
    [Serializable]
    public struct GlobalContainerSettings {

        [field: Header("Auto subscribe on:")]
        [field: SerializeField] public bool EventBus { get; private set; }
        [field: SerializeField] public bool UpdateCallbacks { get; private set; }
    }
}
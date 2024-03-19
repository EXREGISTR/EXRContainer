using System;
using UnityEngine;

namespace EXRContainer {
    [Serializable]
    public class GlobalContainerSettings : BaseContainerSettings {

        [field: Header("Auto subscribe on:")]
        [field: SerializeField] public bool EventBus { get; private set; } = true;
        [field: SerializeField] public bool UpdateCallbacks { get; private set; } = true;
    }
}
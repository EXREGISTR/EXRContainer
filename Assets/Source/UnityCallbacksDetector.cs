using EXRContainer.EventsOld;
using UnityEngine;

namespace EXRContainer {
    public abstract class UnityCallbacksDetector : MonoBehaviour {
        protected Entity Entity { get; private set; }
        protected ISendersContainer EventsService { get; private set; }

        internal void Initialize(Entity entity, EventsService eventsService) {
            if (Entity != null){
                Debug.LogWarning($"Detector {ToString()} for entity {Entity.name} already initialized!");
                return;
            }

            Entity = entity;
            EventsService = eventsService;
            PostInitialize();
        }

        protected virtual void PostInitialize() { }
    }
}
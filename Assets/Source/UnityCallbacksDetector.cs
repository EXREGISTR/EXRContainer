using EXRContainer.Events;
using UnityEngine;

namespace EXRContainer {
    public abstract class UnityCallbacksDetector : MonoBehaviour {
        protected Entity Entity { get; private set; }
        protected EventsService EventsService { get; private set; }

        public void Initialize(Entity entity, EventsService eventsService) {
            if (Entity != null){
                Debug.LogWarning($"Detector {ToString()} for entity {Entity.name} already initialized!");
                return;
            }

            Entity = entity;
            EventsService = eventsService;
        }
    }
}
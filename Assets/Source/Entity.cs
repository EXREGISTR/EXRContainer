using EXRContainer.Core;
using EXRContainer.Events;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace EXRContainer {
    public class Entity : MonoBehaviour {
        private Dictionary<Type, UnityCallbacksDetector> callbacksDetector;
        private EventsService eventsService;
        private IDIContext context;

        private void Construct(IDIContext context, EventsService eventsService) {
            this.context = context;
            this.eventsService = eventsService;
        }

        private void OnDestroy() {
            context.Dispose();
            eventsService.Dispose();
        }

        public void CreateDetector<TDetector>(Action<IDIContext, TDetector> initializator = null) 
            where TDetector : UnityCallbacksDetector {
            var type = typeof(TDetector);
            callbacksDetector ??= new Dictionary<Type, UnityCallbacksDetector>();

            if (callbacksDetector.ContainsKey(type)) {
                Debug.LogWarning($"Detector {type.Name} already created on entity {gameObject.name}");
                return;
            }

            var detector = gameObject.AddComponent<TDetector>();
            detector.Initialize(this, eventsService);
            initializator?.Invoke(context, detector);
            callbacksDetector[type] = detector;
        }
    }
}
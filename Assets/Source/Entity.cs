using EXRContainer.Core;
using EXRContainer.Events;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace EXRContainer {
    public static class EntityExtensions {

    }

    public class Entity : MonoBehaviour {
        private Dictionary<Type, UnityCallbacksDetector> callbacksDetector;
        private IMediator mediator;
        private DependenciesContext context;

        private void Construct(DependenciesContext context, IMediator mediator) {
            this.context = context;
            this.mediator = mediator;
        }

        private void OnDestroy() {
            context.Dispose();
        }

        public void EnableDetector<TDetector>() where TDetector : UnityCallbacksDetector
            => SetStateForDetector(typeof(TDetector), true);

        public void DisableDetector<TDetector>() where TDetector : UnityCallbacksDetector
            => SetStateForDetector(typeof(TDetector), false);

        private void SetStateForDetector(Type detectorType, bool value) {
            if (callbacksDetector.TryGetValue(detectorType, out var detector)) {
                detector.enabled = value;
            }
        }

        public void CreateDetector<TDetector>(Action<IDIContext, TDetector> initializator = null, bool printWarningIfAlreadyCreated = false) 
            where TDetector : UnityCallbacksDetector {
            var type = typeof(TDetector);
            callbacksDetector ??= new Dictionary<Type, UnityCallbacksDetector>();

            if (callbacksDetector.ContainsKey(type)) {
                if (printWarningIfAlreadyCreated) {
                    Debug.LogWarning($"Detector {type.Name} already created on entity {gameObject.name}");
                }

                return;
            }

            var detector = gameObject.AddComponent<TDetector>();
            detector.Initialize(this, mediator);
            initializator?.Invoke(context, detector);
            callbacksDetector[type] = detector;
        }
    }
}
using System.Collections.Generic;
using UnityEngine;

namespace EXRContainer {
    public class TriggerStayDetector : MonoBehaviour {
        private HashSet<ITriggerStayHandler> handlers;

        private void Awake() {
            handlers = new HashSet<ITriggerStayHandler>();
        }

        public void Subscribe(ICollisionStayHandler handler) => handlers.Add(handler);

        private void OnTriggerEnter(Collider other) {
            foreach (var handler in handlers) {
                handler.OnTriggerStay(other);
            }
        }
    }
}
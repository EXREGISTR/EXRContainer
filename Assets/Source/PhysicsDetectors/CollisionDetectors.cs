using EXRContainer.Events;
using UnityEngine;

namespace EXRContainer {
    public class CollisionDetector : UnityCallbacksDetector {
        private void OnCollisionEnter(Collision collision) {
            var message = new CollisionEnterMessage(collision, Entity);
            EventsService.SendMessage(message);
        }

        private void OnCollisionExit(Collision collision) {
            var message = new CollisionExitMessage(collision, Entity);
            EventsService.SendMessage(message);
        }
    }

    public class CollisionStayDetector : UnityCallbacksDetector {
        private void OnCollisionStay(Collision collision) {

            var message = new CollisionStayMessage(collision, Entity);
            EventsService.SendMessage(message);
        }
    }
}
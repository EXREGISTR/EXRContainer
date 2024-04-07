using EXRContainer.Events;
using System;
using UnityEngine;

namespace EXRContainer {
    public class CollisionDetector : UnityCallbacksDetector {
        private IMessageHandlersCollection<CollisionEnterMessage> enterHandlers;
        private IMessageHandlersCollection<CollisionExitMessage> exitHandlers;
        private Predicate<Collision> condition;

        public void WithCondition(Predicate<Collision> condition) => this.condition += condition;

        protected override void PostInitialize() {
            enterHandlers = EventsService.GetMessageHandlers<CollisionEnterMessage>(createIfNoHandlers: true);
            exitHandlers = EventsService.GetMessageHandlers<CollisionExitMessage>(createIfNoHandlers: true);
        }

        private void OnCollisionEnter(Collision collision) {
            if (enterHandlers.IsEmpty) return;
            if (condition != null && !condition(collision)) return;

            var message = new CollisionEnterMessage(collision, Entity);

            enterHandlers.Notify(message);
        }

        private void OnCollisionExit(Collision collision) {
            if (exitHandlers.IsEmpty) return;
            if (condition != null && !condition(collision)) return;

            var message = new CollisionExitMessage(collision, Entity);

            exitHandlers.Notify(message);
        }
    }

    public class CollisionStayDetector : UnityCallbacksDetector {
        private IMessageHandlersCollection<CollisionStayMessage> handlers;
        private Predicate<Collision> condition;

        protected override void PostInitialize() {
            handlers = EventsService.GetMessageHandlers<CollisionStayMessage>();
        }

        public void WithCondition(Predicate<Collision> condition) => this.condition += condition;

        private void OnCollisionStay(Collision collision) {
            if (handlers.IsEmpty) return;
            if (condition != null && !condition(collision)) return;

            var message = new CollisionStayMessage(collision, Entity);
            EventsService.SendMessage(message);
        }
    }
}
using UnityEngine;

namespace EXRContainer.Events {
    public readonly struct CollisionEnterMessage {
        public Collision Collision { get; }
        public Entity Sender { get; }

        public CollisionEnterMessage(Collision collision, Entity sender) {
            Collision = collision;
            Sender = sender;
        }
    }

    public readonly struct CollisionStayMessage {
        public Collision Collision { get; }
        public Entity Sender { get; }

        public CollisionStayMessage(Collision collision, Entity sender) {
            Collision = collision;
            Sender = sender;
        }
    }

    public readonly struct CollisionExitMessage {
        public Collision Collision { get; }
        public Entity Sender { get; }

        public CollisionExitMessage(Collision collision, Entity sender) {
            Collision = collision;
            Sender = sender;
        }
    }
}

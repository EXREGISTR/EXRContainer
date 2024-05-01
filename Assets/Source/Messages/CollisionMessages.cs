using UnityEngine;

namespace EXRContainer.EventsOld {
    public readonly struct CollisionEnterMessage : IMessage {
        public Collision Collision { get; }
        public Entity Sender { get; }

        public CollisionEnterMessage(Collision collision, Entity sender) {
            Collision = collision;
            Sender = sender;
        }
    }

    public readonly struct CollisionStayMessage : IMessage {
        public Collision Collision { get; }
        public Entity Sender { get; }

        public CollisionStayMessage(Collision collision, Entity sender) {
            Collision = collision;
            Sender = sender;
        }
    }

    public readonly struct CollisionExitMessage : IMessage {
        public Collision Collision { get; }
        public Entity Sender { get; }

        public CollisionExitMessage(Collision collision, Entity sender) {
            Collision = collision;
            Sender = sender;
        }
    }
}

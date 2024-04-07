using UnityEngine;

namespace EXRContainer.Events {
    public readonly struct TriggerEnterMessage : IMessage {
        public Collider Collider { get; }
        public Entity Sender { get; }

        public TriggerEnterMessage(Collider collider, Entity sender) {
            Collider = collider;
            Sender = sender;
        }
    }

    public readonly struct TriggerStayMessage : IMessage {
        public Collider Collider { get; }
        public Entity Sender { get; }

        public TriggerStayMessage(Collider collider, Entity sender) {
            Collider = collider;
            Sender = sender;
        }
    }

    public readonly struct TriggerExitMessage : IMessage {
        public Collider Collider { get; }
        public Entity Sender { get; }

        public TriggerExitMessage(Collider collider, Entity sender) {
            Collider = collider;
            Sender = sender;
        }
    }
}

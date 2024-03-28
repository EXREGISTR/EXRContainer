using UnityEngine;

namespace EXRContainer {
    public interface ICollisionEnterHandler {
        public void OnCollisionEnter(Collision collision);
    }

    public interface ICollisionStayHandler {
        public void OnCollisionStay(Collision collision);
    }

    public interface ICollisionExitHandler {
        public void OnCollisionExit(Collision collision);
    }
}

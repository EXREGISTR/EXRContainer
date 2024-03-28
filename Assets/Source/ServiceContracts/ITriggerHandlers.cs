using UnityEngine;

namespace EXRContainer {
    public interface ITriggerEnterHandler {
        public void OnTriggerEnter(Collider other);
    }

    public interface ITriggerStayHandler {
        public void OnTriggerStay(Collider other);
    }

    public interface ITriggerExitHandler {
        public void OnTriggerExit(Collider other); 
    }
}

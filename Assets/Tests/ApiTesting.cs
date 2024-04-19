using EXRContainer;
using EXRContainer.Core;
using UnityEngine;

namespace Tests {
    public class ApiTesting : MonoBehaviour {
    }

    internal class PlayerController {
        public PlayerController(IDIContext context) {
            // context.Dispose();
        }

        [EXRInitializator]
        public PlayerController() {
            
        }

        [EXRInitializator]
        private void Construct() {

        }

        public void Meow() { }
    }
}
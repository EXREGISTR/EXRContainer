using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace EXRContainer {
    public abstract class GlobalContainerProvider : MonoBehaviour {
        [SerializeField] private MonoInstaller[] monoInstallers;
        [SerializeField] private ScriptableInstaller[] scriptableInstallers;
        [SerializeField] private bool initializeOnLoad;

        private async void Awake() {
            if (!initializeOnLoad) return;

            await Initialize();
        }

        public abstract Task Initialize();

        protected IEnumerable<IInstaller> GetInstallers() {
            IEnumerable<IInstaller> installers = monoInstallers;
            installers.Union((IEnumerable<IInstaller>)scriptableInstallers);
            return installers;
        }
    }
}
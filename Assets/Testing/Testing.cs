using EXRContainer.Core;
using EXRContainer.LambdaGeneration;
using UnityEngine;

namespace EXRContainer {
    public class Testing : MonoBehaviour {
        private void Awake() {
            
        }
        private Factory<object> CreatePlayerControllerFactory(FactoryExpressionsContainer other) {
            var container = new FactoryExpressionsContainer(other);
            container.
            var creator = new FactoryGenerator();

            // creator.BeforeCreationProvider(new PreCreationInvokation(context => Debug.Log("Ща чета будет")));

            /*creator.PostCreationProvider(new PostCreationInvokation<PlayerController>(
                (context, controller) => {
                    controller.Meow();
                    new GameObject(controller.GetType().Name + controller.GetHashCode());
                }));*/

            return creator.Execute(new(typeof(PlayerController), LifeTime.Scoped));
        }
    }
}
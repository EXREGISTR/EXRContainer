using EXRContainer;
using EXRContainer.CodeGeneration;
using EXRContainer.CodeGeneration.Providers;
using EXRContainer.Core;
using UnityEngine;

namespace Tests {
    public class CodeGenerationTests : MonoBehaviour {
        [ContextMenu("Create player")]
        public void CreatePlayer() {
            var factory = CreatePlayerControllerFactory(null);
            factory(GetContext());
        }

        private Factory<object> CreatePlayerControllerFactory(FactoryLambdaCreator other) {
            var creator = new FactoryLambdaCreator(
                new CreationByConstructor()
            );

            creator.BeforeCreationProvider(new PreCreationInvokation(context => Debug.Log("Ща чета будет")));

            creator.PostCreationProvider(new PostCreationInvokation<PlayerController>(
                (context, controller) => {
                    controller.Meow();
                    new GameObject(controller.GetType().Name + controller.GetHashCode());
                }));

            return creator.Create(typeof(PlayerController), LifeTime.Scoped);
        }

        private IDIContext GetContext() {
            var context = new DependenciesContext(null);
            return context;
        }
    }

    internal class PlayerController {

        public PlayerController(IDIContext context) {
            context.Dispose();
        }

        [EXRConstructor]
        public PlayerController() {
            
        }

        [EXRConstructor]
        private void Construct() {

        }

        public void Meow() { }
    }
}
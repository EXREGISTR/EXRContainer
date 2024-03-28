using EXRContainer.CodeGeneration;
using EXRContainer.CodeGeneration.Providers;
using EXRContainer.Core;
using UnityEngine;

namespace Tests {
    public class CodeGenerationTests : MonoBehaviour {
        private Factory<object> CreatePlayerControllerFactory(FactoryLambdaCreator other) {
            var creator = new FactoryLambdaCreator(
                new CreationByCallback<PlayerController>(context => new PlayerController())
            );

            PreCreationCallback callback = context => {
                new GameObject();
                Debug.Log("Ща чета будет");
            };

            creator.BeforeCreationProvider(new PreCreationInvokation(callback));

            return creator.Create(typeof(PlayerController), LifeTime.Scoped);
        }

        private IDIContext GetContext() {
            var context = new DependenciesContext(null);
            return context;
        }
    }

    internal class PlayerController {

    }
}
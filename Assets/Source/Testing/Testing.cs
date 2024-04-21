using EXRContainer.Core;
using EXRContainer.LambdaGeneration;
using System;
using System.Linq.Expressions;
using UnityEngine;

namespace EXRContainer {
    public class CallbackInvokation : IExpressionsProvider {
        private readonly Action callback;

        public CallbackInvokation(Action callback) {
            this.callback = callback;
        }

        public void RegisterExpressions(IGenerationContext context) {
            context.AppendExpression(Expression.Invoke(Expression.Constant(callback)));
        }
    }

    public class Testing : MonoBehaviour {
        private PlayerController controller;

        [ContextMenu("Create player")]
        public void CreatePlayerController() {
            var container = new FactoryGenerationExecutor(new ConstructorCreation());
            container.PostCreation(new CallbackInvokation(() => Debug.Log("ЕЕЕЕЕЕЕ ОНО СОЗДАЛООООСЬ")));

            var container2 = new FactoryGenerationExecutor(container);
            container2.PostCreation(new CallbackInvokation(() => Debug.Log("meeeeeee")));

            var factory = new FactoryGenerator(container2).Execute(new(typeof(PlayerController), LifeTime.Scoped));
            controller = (PlayerController)factory(null);
        }

        [ContextMenu("Finalize Player")]
        public void FinalizePlayerController() {
            var container = new LambdaGenerationExecutor();
            container.Push(new DisposeInvokation());
            container.Push(new CallbackInvokation(() => Debug.Log("уииии")));

            var container2 = new LambdaGenerationExecutor(container);
            container2.Push(new CallbackInvokation(() => {
                gameObject.layer = LayerMask.NameToLayer("Water");
                Debug.Log("АХААХХАХ а не, не смешно");
            }));

            var finalizator = new FinalizatorGenerator(container2).Execute(new(typeof(PlayerController), LifeTime.Scoped));
            finalizator(null, controller);
        }
    }

    public class PlayerController : IDisposable {
        public void Dispose() => Debug.Log("Finalize");
    }
}
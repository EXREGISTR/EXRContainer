using EXRContainer.Core;
using EXRContainer.LambdaGeneration;
using System;
using System.Linq.Expressions;
using System.Reflection;
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

    public class DisposeInvokation : IExpressionsProvider {
        private static readonly MethodInfo disposeMethod = typeof(IDisposable).GetMethod("Dispose");

        public void RegisterExpressions(IGenerationContext context) {
            if (context.DependencyData.Type.GetInterface(nameof(IDisposable)) != null) {
                context.AppendExpression(Expression.Call(context.DependencyInstance, disposeMethod));
            }
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
            var container = new FinalizatorGenerationExecutor();
            container.Push(new DisposeInvokation());
            container.Push(new CallbackInvokation(() => Debug.Log("уииии")));

            var container2 = new FinalizatorGenerationExecutor(container);
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
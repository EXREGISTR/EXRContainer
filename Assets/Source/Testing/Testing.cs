using EXRContainer.Core;
using EXRContainer.EventsOld;
using EXRContainer.LambdaGeneration;
using System;
using System.Linq.Expressions;
using UnityEngine;

namespace EXRContainer {
    public class PostCreationCallbackInvokation<TService> : IExpressionsProvider {
        private readonly Action<IDIContext, TService> callback;

        public PostCreationCallbackInvokation(Action<IDIContext, TService> callback) {
            this.callback = callback;
        }

        public void RegisterExpressions(IGenerationContext context) {
            var invokation = Expression.Invoke(Expression.Constant(callback), 
                context.ContextParameter, context.DependencyInstance);
            context.AppendExpression(invokation);
        }
    }

    public class CallbackInvokation : IExpressionsProvider {
        private readonly Action callback;

        public CallbackInvokation(Action callback) {
            this.callback = callback;
        }

        public void RegisterExpressions(IGenerationContext context) {
            context.AppendExpression(Expression.Invoke(Expression.Constant(callback)));
        }
    }

    public readonly struct MeowMessage : IMessage {
        public readonly string Message;

        public MeowMessage(string message) {
            Message = message;
        }
    }

    public class MeowHandler : IMessageHandler<MeowMessage> {
        public void Notify(MeowMessage message) {
            Debug.Log(message.Message);
        }
    }

    public class Testing : MonoBehaviour {
        private PlayerController controller;

        [ContextMenu("Check meow set")]
        public void CheckHashSet() {
            var coll = new MessageProcessors<MeowMessage>();
            var handler = new MeowHandler();
            coll.Push(new(handler, x => true));
            var result1 = coll.Notify(new("meememm"));
            coll.Delete(handler);
            var result2 = coll.Notify(new("rustaaaam"));
            Debug.Assert(result1 == true);
            Debug.Assert(result2 == false);
        }

        [ContextMenu("Create player")]
        public void CreatePlayerController() {
            var container = new FactoryGenerationExecutor(new ConstructorCreation());
            container.PostCreation(new CallbackInvokation(() => Debug.Log("ЕЕЕЕЕЕЕ ОНО СОЗДАЛООООСЬ")));

            var container2 = new FactoryGenerationExecutor(container);
            container2.PostCreation(new PostCreationCallbackInvokation<PlayerController>(
                (context, controller) => {
                    Debug.Log("meeeeeee");
                    controller.Dispose();
                }));

            var factory = FactoryGenerator.Execute(container2, new(typeof(PlayerController), LifeTime.Scoped));
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

            var finalizator = LambdaGenerator<Finalizator<object>>
                .Execute(container2, new(typeof(PlayerController), LifeTime.Scoped));
            finalizator(null, controller);
        }
    }

    public class PlayerController : IDisposable {
        public void Dispose() => Debug.Log("Finalize");
    }
}
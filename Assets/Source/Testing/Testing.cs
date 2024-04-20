using EXRContainer.Core;
using EXRContainer.LambdaGeneration;
using System.Linq.Expressions;
using UnityEngine;

namespace EXRContainer {
    public class ZloyInitializator : IExpressionsProvider {
        public void RegisterExpressions(IGenerationContext context) {
            context.AppendExpression(Expression.Assign(context.DependencyInstance, Expression.Constant(new PlayerController())));
        }
    }

    public class Testing : MonoBehaviour {

        [ContextMenu("Create player")]
        public void CreatePlayerController() {
            var container = new FactoryGenerationExecutor(new ConstructorCreation());

            var factory = new FactoryGenerator(container).Execute(new(typeof(PlayerController), LifeTime.Scoped));
            var player = (PlayerController)factory(null);
            player.Meow();
        }
    }

    public class PlayerController {
        public void Meow() => Debug.Log("meow");
    }
}
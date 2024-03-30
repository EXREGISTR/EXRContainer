using EXRContainer.CodeGeneration;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

namespace EXRContainer.Dependencies {
    internal class CreationFromComponentOnPrefab : ICreationExpressionsProvider {
        private readonly bool shouldCreateNewComponent;
        private readonly bool shouldFindInChildren;

        public CreationFromComponentOnPrefab(GameObject prefab, bool createNewComponent, bool shouldFindInChildren) {
            this.shouldCreateNewComponent = createNewComponent;
            this.shouldFindInChildren = shouldFindInChildren;
        }

        public IEnumerable<Expression> GenerateCode(ExecutionContext context) {
            throw new System.NotImplementedException();
        }

        public IEnumerable<ParameterExpression> GetVariables() {
            throw new System.NotImplementedException();
        }
    }
}
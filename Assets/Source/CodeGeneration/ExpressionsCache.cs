using System.Linq.Expressions;
using EXRContainer.Core;

namespace EXRContainer.CodeGeneration {
    public static class ExpressionsCache {
        public static readonly ParameterExpression ObjectParameter = Expression.Parameter(typeof(object), "object_exr");
        public static readonly ParameterExpression ContextParameter = Expression.Parameter(typeof(IDIContext), "di_context_exr");
    }
}
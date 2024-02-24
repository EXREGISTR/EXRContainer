using System;

namespace EXRContainer.Core {
    public class AlreadyRegisteredDependencyException : Exception {
        public AlreadyRegisteredDependencyException(Type type, string senderName) 
            : base($"Dependency of source {type} already registered in {senderName}") {

        }
    }

    public class AlreadyExistSingleException : Exception {
        public AlreadyExistSingleException(Type singleType) : base($"Single {singleType} already exist!") {
            
        }
    }

    public class NoDependencyException : Exception {
        public NoDependencyException(Type dependencyType) : base($"No dependency registered on the source {dependencyType}!") {
            
        }
    }

    public class NoAccessToContextException : Exception {
        public NoAccessToContextException(IDIContext context) : base($"No access to context {context}!") {
            
        }
    }
}

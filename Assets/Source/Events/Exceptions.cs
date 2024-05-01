using System;

namespace EXRContainer.Events {
    internal class AlreadyRegisteredProcessorForMessage : Exception {
        public AlreadyRegisteredProcessorForMessage(Type messageType, IProcessor processor) 
            : base($"Message {messageType} already has processor {processor}") { }
    }

    internal class NoProcessorsException : Exception {
        public NoProcessorsException(Type messageType) : base($"Message {messageType} has not processors!") { }
    }
}

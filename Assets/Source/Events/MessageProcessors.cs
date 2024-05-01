using System;
using System.Collections.Generic;

namespace EXRContainer.Events {
    internal class MessageProcessors<T> : IProcessorsCollection where T : IMessage {
        private static readonly ProcessorComparer comparer = new();
        private HashSet<ProcessorWrapper> processors;

        public void Push(ProcessorWrapper wrapper) {
            processors ??= new HashSet<ProcessorWrapper>(comparer);
            processors.Add(wrapper);
        }

        public void Delete(IMessageProcessor<T> processor) {
            if (processors == null) return;

            processors.Remove(new(processor, null));
        }

        public void Notify(T message) {
            if (processors == null || processors.Count == 0) return;

            foreach (var handler in processors) {
                handler.Notify(message);
            }
        }

        public void Dispose() {
            processors?.Clear();
            processors = null;
        }

        internal readonly struct ProcessorWrapper : IEquatable<ProcessorWrapper> {
            private readonly IMessageProcessor<T> processor;
            private readonly Predicate<T> condition;

            public ProcessorWrapper(IMessageProcessor<T> processor, Predicate<T> condition) {
                this.processor = processor;
                this.condition = condition;
            }

            public void Notify(T message) {
                if (condition(message)) processor.Process(message);
            }

            public override bool Equals(object obj) {
                if (obj is not ProcessorWrapper other) return false;
                return Equals(other);
            }

            public override int GetHashCode() => processor.GetHashCode();
            public bool Equals(ProcessorWrapper other) => processor == other.processor;
        }

        private class ProcessorComparer : IEqualityComparer<ProcessorWrapper> {
            public bool Equals(ProcessorWrapper x, ProcessorWrapper y) => x.Equals(y);
            public int GetHashCode(ProcessorWrapper obj) => obj.GetHashCode();
        }
    }
}

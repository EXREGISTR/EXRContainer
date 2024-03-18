using System;
using System.Collections.Generic;
using System.Linq;

namespace EXRContainer.Core {
    internal class SinglesStack : IDisposable {
        private readonly IDIContext context;

        private Dictionary<Type, Single> singles;
        private List<Single> orderToFinalize;

        private bool disposedValue;

        public SinglesStack(IDIContext context) {
            this.context = context;
        }

        public object FindSingle(Type contract) {
            return singles != null && singles.TryGetValue(contract, out var single) ? single.Source : null;
        }

        public object FindSingle(IEnumerable<Type> contractTypes) {
            if (singles == null) {
                return null;
            }

            foreach (var contract in contractTypes) {
                if (singles.TryGetValue(contract, out var single)) {
                    return single.Source;
                }
            }

            return null;
        }

        public void PushSingle(object single, Finalizator<object> finalizator, IEnumerable<Type> contractTypes) { 
            orderToFinalize ??= new List<Single>();
            var wrapper = new Single(single, finalizator);

            if (contractTypes == null || !contractTypes.Any()) {
                PushSingle(single.GetType(), wrapper);
                return;
            }

            foreach (var contract in contractTypes) {
                PushSingle(contract, wrapper);
            }

            orderToFinalize.Add(wrapper);
        }

        public void PushSingle(object single, Type dependencyType, Finalizator<object> finalizator) {
            var wrapper = new Single(single, finalizator);
            PushSingle(dependencyType, wrapper);
        }

        private void PushSingle(Type type, in Single wrapper) {
            var hasSingle = singles.ContainsKey(type);

            if (hasSingle) {
                throw new AlreadyExistSingleException(type);
            }

            singles[type] = wrapper;
            return;
        }

        public object DeleteSingle(IEnumerable<Type> contractTypes) {
            if (singles == null) return null;

            Single single = default;
            bool founded = false;

            foreach (var contract in contractTypes) {
                if (singles.TryGetValue(contract, out single)) {
                    founded = true;
                    break;
                }
            }

            if (!founded) return null;

            foreach (var contract in contractTypes) {
                singles.Remove(contract);
            }

            var index = orderToFinalize.BinarySearch(single);
            orderToFinalize.RemoveAt(index);
            return single.Source;
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(in bool _) {
            if (disposedValue) return;

            if (orderToFinalize == null) return;

            for (int index = orderToFinalize.Count - 1; index >= 0; index--) {
                var single = orderToFinalize[index];
                single.Finalizator?.Invoke(context, single.Source);
            }

            singles.Clear();
            singles = null;

            orderToFinalize.Clear();
            orderToFinalize = null;

            disposedValue = true;
        }

        private readonly struct Single {
            public object Source { get; }
            public Finalizator<object> Finalizator { get; }

            public Single(object source, Finalizator<object> finalizator) {
                Source = source;
                Finalizator = finalizator;
            }

            public static bool operator ==(Single a, Single b) => ReferenceEquals(a.Source, b.Source);
            public static bool operator !=(Single a, Single b) => !ReferenceEquals(a.Source, b.Source);

            public override bool Equals(object obj) {
                if (obj is not Single other) return false;

                return this == other;
            }

            public override int GetHashCode() => Source.GetHashCode();
        }
    }
}

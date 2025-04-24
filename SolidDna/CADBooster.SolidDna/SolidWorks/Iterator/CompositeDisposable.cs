using System;
using System.Collections.Generic;

namespace CADBooster.SolidDna
{
    /// <summary>Container for multiple IDisposable objects that can be disposed together.</summary> 
    public sealed class CompositeDisposable : ICompositeDisposable
    {
        /// <summary>True if this instance has been disposed.</summary>
        public bool IsDisposed => _disposables is null;

        private List<IDisposable> _disposables = new List<IDisposable>();

        /// <summary>Adds a disposable to the collection.</summary>
        /// <param name="disposable">Disposable object to add</param>
        /// <exception cref="ObjectDisposedException">Thrown if already disposed</exception>
        public void Add(IDisposable disposable)
        {
            if (IsDisposed)
                throw new ObjectDisposedException(nameof(CompositeDisposable));

            _disposables.Add(disposable);
        }

        /// <summary>
        /// Disposes all contained disposables in order and clears the collection.
        /// </summary>
        public void Dispose()
        {
            if (IsDisposed)
                return;

            foreach (var obj in _disposables)
                obj.Dispose();

            // Do not handle collection, just allow it to be garbage collected
            _disposables = null;
        }
    }
}

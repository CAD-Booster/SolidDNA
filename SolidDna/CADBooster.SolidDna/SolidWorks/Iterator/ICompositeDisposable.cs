using System;

namespace CADBooster.SolidDna
{
    /// <summary>Composite of disposable objects disposed together</summary>
    public interface ICompositeDisposable : IDisposable
    {
        /// <summary>True if this composite has been disposed</summary>
        bool IsDisposed { get; }

        /// <summary>Adds a disposable to the collection</summary>
        /// <param name="disposable">Disposable object to add</param>
        /// <exception cref="ObjectDisposedException">If composite is disposed</exception>
        void Add(IDisposable disposable);
    }
}
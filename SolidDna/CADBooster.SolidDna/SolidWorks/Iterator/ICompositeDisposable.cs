using System;

namespace CADBooster.SolidDna
{
    public interface ICompositeDisposable : IDisposable
    {
        bool IsDisposed { get; }

        void Add(IDisposable disposable);
    }
}
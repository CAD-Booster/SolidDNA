using System;

namespace CADBooster.SolidDna
{
    internal readonly struct DummyCompositeDisposable : ICompositeDisposable
    {
        public bool IsDisposed => false;

        public static readonly DummyCompositeDisposable Default = new DummyCompositeDisposable();

        public void Add(IDisposable disposable)
        {

        }

        public void Dispose() => throw new InvalidOperationException();
    }
}

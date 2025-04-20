using System;
using System.Collections.Generic;

namespace CADBooster.SolidDna
{
    public sealed class CompositeDisposable : ICompositeDisposable
    {
        public bool IsDisposed => _disposables is null;

        private List<IDisposable> _disposables = new List<IDisposable>();

        public void Add(IDisposable disposable)
            => _disposables.Add(disposable);

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

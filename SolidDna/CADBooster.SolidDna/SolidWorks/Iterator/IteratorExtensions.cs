using System;
using System.Collections.Generic;
using System.Linq;

namespace CADBooster.SolidDna
{
    public static class IteratorExtensions
    {
        public static IEnumerable<SolidDnaObject<T>> WrapDnaObject<T>(this IEnumerable<T> dnaObjects)
            => dnaObjects.Select(x => new SolidDnaObject<T>(x));

        public static SolidDnaObject<T> ToDnaObject<T>(this T unsafeObject)
            => new SolidDnaObject<T>(unsafeObject);

        public static IEnumerable<SolidDnaObject<T>> WrapDnaObjects<T>(this IEnumerable<T> dnaObjects, ICompositeDisposable disposable)
            => dnaObjects
                .Select(x => x.ToDnaObject())
                .DisposeEachWith(disposable);

        public static void DisposeEach<T>(this IEnumerable<T> dnaObjects) where T : SolidDnaObject, IDisposable
        {
            foreach (var obj in dnaObjects)
                obj.Dispose();
        }

        public static IEnumerable<T> DisposeEachWith<T>(this IEnumerable<T> dnaObjects, ICompositeDisposable disposable) where T : SolidDnaObject, IDisposable
            => dnaObjects.Select(x =>
            {
                if (disposable.IsDisposed)
                    throw new ObjectDisposedException(nameof(disposable));

                disposable.Add(x);
                return x;
            });

        public static ICompositeDisposable GetDummyIfNull(this ICompositeDisposable disposable)
            => disposable is null ? DummyCompositeDisposable.Default : disposable;
    }
}

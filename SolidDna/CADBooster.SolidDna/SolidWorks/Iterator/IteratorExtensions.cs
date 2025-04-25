using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace CADBooster.SolidDna
{
    /// <summary>
    /// Extension methods for working with disposable objects and collections
    /// </summary>
    public static class IteratorExtensions
    {
        /// <summary>
        /// Wraps each item in a new SolidDnaObject
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<SolidDnaObject<T>> WrapDnaObject<T>(this IEnumerable<T> dnaObjects)
            => dnaObjects.Select(x => new SolidDnaObject<T>(x));

        /// <summary>
        /// Wraps a single object in a SolidDnaObject
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SolidDnaObject<T> ToDnaObject<T>(this T unsafeObject)
            => new SolidDnaObject<T>(unsafeObject);

        /// <summary>
        /// Wraps objects and registers them with a disposable container
        /// </summary>
        /// <exception cref="ObjectDisposedException">If container is disposed</exception>
        public static IEnumerable<SolidDnaObject<T>> WrapDnaObjects<T>(this IEnumerable<T> dnaObjects, ICompositeDisposable disposable)
            => dnaObjects
                .Select(x => x.ToDnaObject())
                .DisposeEachWith(disposable);

        /// <summary>
        /// Wraps objects using a custom factory and registers them with a disposable container
        /// </summary>
        /// <typeparam name="TIn">The input type</typeparam>
        /// <typeparam name="TOut">The output wrapper type</typeparam>
        /// <param name="dnaObjects">The collection of objects to wrap</param>
        /// <param name="disposable">The container to register disposables with</param>
        /// <param name="wrapObjectFactory">Factory method to create wrappers</param>
        /// <returns>An enumerable of wrapped objects</returns>
        /// <exception cref="ObjectDisposedException">Thrown if the container is already disposed</exception>
        public static IEnumerable<TOut> WrapDnaObjects<TIn, TOut>(this IEnumerable<TIn> dnaObjects, ICompositeDisposable disposable, Func<TIn, TOut> wrapObjectFactory) where TOut : SolidDnaObject, IDisposable
            => dnaObjects
                .Select(x => wrapObjectFactory.Invoke(x))
                .DisposeEachWith(disposable);

        /// <summary>
        /// Disposes all items in the collection
        /// </summary>
        public static void DisposeEach<T>(this IEnumerable<T> dnaObjects) where T : IDisposable
        {
            foreach (var obj in dnaObjects)
                obj.Dispose();
        }

        /// <summary>
        /// Registers items with a disposable container
        /// </summary>
        /// <exception cref="ObjectDisposedException">If container is disposed</exception>
        public static IEnumerable<T> DisposeEachWith<T>(this IEnumerable<T> dnaObjects, ICompositeDisposable disposable) where T : SolidDnaObject, IDisposable
            => dnaObjects.Select(x =>
            {
                if (disposable.IsDisposed)
                    throw new ObjectDisposedException(nameof(disposable));

                disposable.Add(x);
                return x;
            });

        /// <summary>
        /// Returns a dummy disposable if input is null
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static ICompositeDisposable GetDummyIfNull(this ICompositeDisposable disposable)
            => disposable is null ? DummyCompositeDisposable.Default : disposable;
    }
}

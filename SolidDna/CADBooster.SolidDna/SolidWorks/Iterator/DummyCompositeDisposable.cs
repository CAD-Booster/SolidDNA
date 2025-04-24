using System;

namespace CADBooster.SolidDna
{
    /// <summary>Placeholder implementation of <see cref="ICompositeDisposable"/> for when disposal tracking isn't required.</summary>
    /// <remarks>
    /// <para><b>Behavior:</b></para>
    /// <list type="bullet">
    /// <item>
    /// <description>Does not store any <see cref="IDisposable"/> instances</description>
    /// </item>
    /// <item>
    /// <description>Always reports <see cref="IsDisposed"/> as <see langword="false"/></description>
    /// </item>
    /// <item>
    /// <description>Throws <see cref="InvalidOperationException"/> when <see cref="Dispose"/> is called</description>
    /// </item>
    /// </list>
    /// <para><b>Usage:</b> Access via <see cref="Default"/> singleton instance.</para>
    /// </remarks>
    internal readonly struct DummyCompositeDisposable : ICompositeDisposable
    {
        /// <summary>
        /// Always returns false since this dummy implementation doesn't track disposal state.
        /// </summary>
        public bool IsDisposed => false;

        /// <summary>
        /// The singleton instance of the dummy disposable.
        /// </summary>
        public static readonly DummyCompositeDisposable Default = new DummyCompositeDisposable();

        /// <summary>
        /// No-op method that doesn't store the disposable.
        /// </summary>
        /// <param name="disposable">Ignored parameter</param>
        public void Add(IDisposable disposable)
        {
            // Intentionally empty - this is a dummy implementation
        }

        /// <summary>
        /// Always throws <see cref="InvalidOperationException"/> since disposal isn't supported.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Always thrown when called, as this is a dummy implementation.
        /// </exception>
        public void Dispose() => throw new InvalidOperationException();
    }
}
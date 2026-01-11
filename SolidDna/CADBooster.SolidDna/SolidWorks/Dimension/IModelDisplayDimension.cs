using SolidWorks.Interop.sldworks;
using System;

namespace CADBooster.SolidDna
{
    /// <summary>
    /// Interface for a SolidWorks Display Dimension.
    /// Enables mocking for unit testing code that consumes display dimensions.
    /// </summary>
    public interface IModelDisplayDimension : IDisposable
    {
        #region Properties

        /// <summary>
        /// The selection name for this dimension that can be used to select it.
        /// For example D1@Sketch1.
        /// </summary>
        string SelectionName { get; }

        /// <summary>
        /// The raw underlying COM object.
        /// WARNING: Use with caution. You must handle all disposal from this point on.
        /// </summary>
        IDisplayDimension UnsafeObject { get; }

        #endregion
    }
}

using SolidWorks.Interop.sldworks;
using System;

namespace CADBooster.SolidDna
{
    /// <summary>
    /// Interface for a SolidWorks drawing sheet.
    /// Enables mocking for unit testing code that consumes drawing sheets.
    /// </summary>
    public interface IDrawingSheet : IDisposable
    {
        #region Properties

        /// <summary>
        /// The sheet name.
        /// </summary>
        string SheetName { get; }

        /// <summary>
        /// The raw underlying COM object.
        /// WARNING: Use with caution. You must handle all disposal from this point on.
        /// </summary>
        Sheet UnsafeObject { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Activate this sheet.
        /// </summary>
        /// <returns>True if successful</returns>
        bool Activate();

        #endregion
    }
}

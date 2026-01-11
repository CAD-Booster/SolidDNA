using SolidWorks.Interop.sldworks;
using System;

namespace CADBooster.SolidDna
{
    /// <summary>
    /// Interface for a SolidWorks model extension.
    /// Enables mocking for unit testing code that consumes model extensions.
    /// </summary>
    public interface IModelExtension : IDisposable
    {
        #region Properties

        /// <summary>
        /// The parent Model for this extension
        /// </summary>
        Model Parent { get; set; }

        /// <summary>
        /// The raw underlying COM object.
        /// WARNING: Use with caution. You must handle all disposal from this point on.
        /// </summary>
        ModelDocExtension UnsafeObject { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Gets a configuration-specific custom property editor for the specified configuration.
        /// If no configuration is specified the default custom property manager is returned.
        /// NOTE: Custom Property Editor must be disposed of once finished.
        /// </summary>
        /// <param name="configuration">The configuration name, or null for the default</param>
        /// <returns>A custom property editor for the specified configuration</returns>
        CustomPropertyEditor CustomPropertyEditor(string configuration = null);

        /// <summary>
        /// Gets the mass properties of a part/assembly.
        /// </summary>
        /// <param name="doNotThrowOnError">If true, don't throw on errors, just return empty mass</param>
        /// <returns>The mass properties of the model</returns>
        MassProperties GetMassProperties(bool doNotThrowOnError = true);

        #endregion
    }
}

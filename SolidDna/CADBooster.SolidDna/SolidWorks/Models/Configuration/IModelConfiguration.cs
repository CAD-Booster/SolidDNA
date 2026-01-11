using SolidWorks.Interop.sldworks;
using System;
using System.Collections.Generic;

namespace CADBooster.SolidDna
{
    /// <summary>
    /// Interface for a SolidWorks model configuration.
    /// Enables mocking for unit testing code that consumes configurations.
    /// </summary>
    public interface IModelConfiguration : IDisposable
    {
        #region Properties

        /// <summary>
        /// The list of child configurations. Is an empty list when this configuration does not have any children.
        /// </summary>
        List<ModelConfiguration> Children { get; }

        /// <summary>
        /// Comments value of the configuration properties.
        /// </summary>
        string Comment { get; set; }

        /// <summary>
        /// User-friendly description text.
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// The ID number of this configuration. Is unique within this model and never changes.
        /// </summary>
        int Id { get; }

        /// <summary>
        /// Whether this configuration is a child configuration and has a parent.
        /// </summary>
        bool IsDerived { get; }

        /// <summary>
        /// The name of the configuration. The name is the main identifier and is used in most methods that have a configuration argument.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// The parent configuration of this configuration. Is null when this is not a derived configuration.
        /// </summary>
        ModelConfiguration Parent { get; }

        /// <summary>
        /// The type of configuration, for example standard, flat pattern or one of two weldment options.
        /// </summary>
        ConfigurationTypes Type { get; }

        /// <summary>
        /// The raw underlying COM object.
        /// WARNING: Use with caution. You must handle all disposal from this point on.
        /// </summary>
        Configuration UnsafeObject { get; }

        #endregion
    }
}

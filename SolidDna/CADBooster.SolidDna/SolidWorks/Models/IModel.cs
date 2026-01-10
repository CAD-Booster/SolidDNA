using SolidWorks.Interop.sldworks;
using System;
using System.Collections.Generic;

namespace CADBooster.SolidDna
{
    /// <summary>
    /// Interface for a SolidWorks model.
    /// Enables mocking for unit testing code that consumes models.
    /// </summary>
    public interface IModel : IDisposable
    {
        #region Properties

        /// <summary>
        /// Contains the current active configuration information
        /// </summary>
        ModelConfiguration ActiveConfiguration { get; }

        /// <summary>
        /// Get the number of configurations. Returns zero for drawings.
        /// </summary>
        int ConfigurationCount { get; }

        /// <summary>
        /// Get the configuration names. Returns an empty list for drawings.
        /// </summary>
        List<string> ConfigurationNames { get; }

        /// <summary>
        /// The absolute file path of this model if it has been saved
        /// </summary>
        string FilePath { get; }

        /// <summary>
        /// Indicates if this file has been saved (so exists on disk).
        /// </summary>
        bool HasBeenSaved { get; }

        /// <summary>
        /// True if this model is a part
        /// </summary>
        bool IsPart { get; }

        /// <summary>
        /// True if this model is an assembly
        /// </summary>
        bool IsAssembly { get; }

        /// <summary>
        /// True if this model is a drawing
        /// </summary>
        bool IsDrawing { get; }

        /// <summary>
        /// Contains extended information about the model
        /// </summary>
        ModelExtension Extension { get; }

        /// <summary>
        /// The mass properties of the part
        /// </summary>
        MassProperties MassProperties { get; }

        /// <summary>
        /// The type of document such as a part, assembly or drawing
        /// </summary>
        ModelType ModelType { get; }

        /// <summary>
        /// Indicates if this file needs saving (has file changes).
        /// </summary>
        bool NeedsSaving { get; }

        /// <summary>
        /// The selection manager for this model
        /// </summary>
        SelectionManager SelectionManager { get; }

        /// <summary>
        /// The raw underlying COM object.
        /// WARNING: Use with caution.
        /// </summary>
        ModelDoc2 UnsafeObject { get; }

        #endregion

        #region Events

        /// <summary>
        /// Called when the active configuration has changed
        /// </summary>
        event Action ActiveConfigurationChanged;

        /// <summary>
        /// Called as the model is about to be closed
        /// </summary>
        event Action ModelClosing;

        /// <summary>
        /// Called when any of the model properties changes
        /// </summary>
        event Action ModelInformationChanged;

        /// <summary>
        /// Called as the model has been saved
        /// </summary>
        event Action ModelSaved;

        /// <summary>
        /// Called when the selected objects in the model have changed
        /// </summary>
        event Action SelectionChanged;

        #endregion

        #region Methods

        /// <summary>
        /// Close this model.
        /// </summary>
        void Close();

        /// <summary>
        /// Make another configuration the active configuration.
        /// </summary>
        bool ActivateConfiguration(string configurationName);

        /// <summary>
        /// Get a configuration by its name.
        /// </summary>
        ModelConfiguration GetConfiguration(string configurationName);

        /// <summary>
        /// Gets a custom property by the given name
        /// </summary>
        string GetCustomProperty(string name, string configuration = null, bool resolved = false);

        /// <summary>
        /// Sets a custom property to the given value.
        /// </summary>
        void SetCustomProperty(string name, string value, string configuration = null);

        /// <summary>
        /// Deletes a custom property by the given name
        /// </summary>
        void DeleteCustomProperty(string name, string configuration = null);

        /// <summary>
        /// Read the material from the model
        /// </summary>
        Material GetMaterial();

        /// <summary>
        /// Sets the material for the model
        /// </summary>
        void SetMaterial(Material material, string configuration = null);

        /// <summary>
        /// Saves the current model
        /// </summary>
        ModelSaveResult Save(SaveAsOptions options = SaveAsOptions.None);

        /// <summary>
        /// Saves a file to the specified path
        /// </summary>
        ModelSaveResult SaveAs(string savePath, SaveAsVersion version = SaveAsVersion.CurrentVersion, SaveAsOptions options = SaveAsOptions.None, PdfExportData pdfExportData = null);

        /// <summary>
        /// Returns a list of full file paths for all dependencies of this model
        /// </summary>
        List<string> Dependencies(bool includeSelf = true, bool includeDrawings = true);

        /// <summary>
        /// Recurses the model for all of its features and sub-features
        /// </summary>
        void Features(Action<ModelFeature, int> featureAction);

        /// <summary>
        /// Recurses the model for all of its components and subcomponents
        /// </summary>
        IEnumerable<(Component component, int depth)> Components();

        /// <summary>
        /// Packs up the current model into a flattened structure
        /// </summary>
        string PackAndGo(string outputFolder = null, string filenamePrefix = "");

        #endregion
    }
}

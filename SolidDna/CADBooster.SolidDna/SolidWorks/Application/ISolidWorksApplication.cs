using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;

namespace CADBooster.SolidDna;

/// <summary>
/// Interface for the SolidWorks application wrapper.
/// Enables mocking for unit testing.
/// </summary>
public interface ISolidWorksApplication : IDisposable
{
    #region Properties

    /// <summary>
    /// The currently active model
    /// </summary>
    Model ActiveModel { get; }

    /// <summary>
    /// The type of SolidWorks application that is currently running.
    /// </summary>
    SolidWorksApplicationType ApplicationType { get; }

    /// <summary>
    /// The command manager
    /// </summary>
    CommandManager CommandManager { get; }

    /// <summary>
    /// Whether we are currently connected to 3DExperience.
    /// </summary>
    ConnectionStatus3DExperience ConnectionStatus3DExperience { get; }

    /// <summary>
    /// True if the application is disposing
    /// </summary>
    bool Disposing { get; }

    /// <summary>
    /// Various preferences for SolidWorks
    /// </summary>
    SolidWorksApplication.SolidWorksPreferences Preferences { get; }

    /// <summary>
    /// Gets the current SolidWorks version information
    /// </summary>
    SolidWorksVersion SolidWorksVersion { get; }

    /// <summary>
    /// The SolidWorks instance cookie
    /// </summary>
    int SolidWorksCookie { get; }

    /// <summary>
    /// The raw underlying COM object.
    /// WARNING: Use with caution. You must handle all disposal from this point on.
    /// </summary>
    SldWorks UnsafeObject { get; }

    #endregion

    #region Events

    /// <summary>
    /// Called when the currently active file has been saved
    /// </summary>
    event Action<string, Model> ActiveFileSaved;

    /// <summary>
    /// Called when any information about the currently active model has changed
    /// </summary>
    event Action<Model> ActiveModelInformationChanged;

    /// <summary>
    /// Called when a new file has been created
    /// </summary>
    event Action<Model> FileCreated;

    /// <summary>
    /// Called when a file has been opened
    /// </summary>
    event Action<string, Model> FileOpened;

    /// <summary>
    /// Called when SolidWorks is idle
    /// </summary>
    event Action Idle;

    /// <summary>
    /// Called when SolidWorks is about to close.
    /// </summary>
    event Action SolidWorksClosing;

    #endregion

    #region Methods

    /// <summary>
    /// Informs this class that the active model may have changed and it should be reloaded
    /// </summary>
    void RequestActiveModelChanged();

    /// <summary>
    /// Create a new assembly.
    /// </summary>
    Model CreateAssembly(string templatePath = null);

    /// <summary>
    /// Create a new drawing with a standard paper size.
    /// </summary>
    Model CreateDrawing(swDwgPaperSizes_e paperSize, string templatePath = null);

    /// <summary>
    /// Create a new drawing with a custom paper size.
    /// </summary>
    Model CreateDrawing(double width, double height, string templatePath = null);

    /// <summary>
    /// Create a new part.
    /// </summary>
    Model CreatePart(string templatePath = null);

    /// <summary>
    /// Loops all open documents returning a safe Model for each document.
    /// </summary>
    IEnumerable<Model> OpenDocuments();

    /// <summary>
    /// Open a part, assembly or drawing by its file path.
    /// </summary>
    Model OpenFile(string filePath, OpenDocumentOptions options = OpenDocumentOptions.None, string configuration = null);

    /// <summary>
    /// Open a part, assembly or drawing and fully control how the file is opened.
    /// </summary>
    Model OpenFile(IDocumentSpecification documentSpecification);

    /// <summary>
    /// Open a part, assembly or drawing by its PLM ID.
    /// </summary>
    Model OpenFileFrom3DExperience(string plmId);

    /// <summary>
    /// Closes a file
    /// </summary>
    void CloseFile(string filePath);

    /// <summary>
    /// Gets an IExportPdfData object for PDF export
    /// </summary>
    IExportPdfData GetPdfExportData();

    /// <summary>
    /// Gets a list of all materials in SolidWorks
    /// </summary>
    List<Material> GetMaterials(string databasePath = null);

    /// <summary>
    /// Attempts to find the material from a SolidWorks material database file
    /// </summary>
    Material FindMaterial(string databasePath, string materialName);

    /// <summary>
    /// Gets the specified user preference value
    /// </summary>
    double GetUserPreferencesDouble(swUserPreferenceDoubleValue_e preference);

    /// <summary>
    /// Sets the specified user preference value
    /// </summary>
    bool SetUserPreferencesDouble(swUserPreferenceDoubleValue_e preference, double value);

    /// <summary>
    /// Gets the specified user preference value
    /// </summary>
    int GetUserPreferencesInteger(swUserPreferenceIntegerValue_e preference);

    /// <summary>
    /// Sets the specified user preference value
    /// </summary>
    bool SetUserPreferencesInteger(swUserPreferenceIntegerValue_e preference, int value);

    /// <summary>
    /// Gets the specified user preference value
    /// </summary>
    string GetUserPreferencesString(swUserPreferenceStringValue_e preference);

    /// <summary>
    /// Sets the specified user preference value
    /// </summary>
    bool SetUserPreferencesString(swUserPreferenceStringValue_e preference, string value);

    /// <summary>
    /// Gets the specified user preference value
    /// </summary>
    bool GetUserPreferencesToggle(swUserPreferenceToggle_e preference);

    /// <summary>
    /// Sets the specified user preference value
    /// </summary>
    void SetUserPreferencesToggle(swUserPreferenceToggle_e preference, bool value);

    /// <summary>
    /// Get a preview bitmap from the saved version of the specified model file
    /// </summary>
    Bitmap GetPreviewBitmap(string modelFilePath, string configurationName);

    /// <summary>
    /// Save a preview bitmap from the saved version of the specified model file
    /// </summary>
    void SavePreviewBitmap(string modelFilePath, string configurationName, string bitmapFilePath);

    /// <summary>
    /// Attempts to create a task pane with a single icon.
    /// </summary>
    Task<Taskpane> CreateTaskpaneAsync(string iconPath, string toolTip);

    /// <summary>
    /// Attempts to create a task pane with multiple icon sizes.
    /// </summary>
    Task<Taskpane> CreateTaskpaneAsync2(string iconPathFormat, string toolTip);

    /// <summary>
    /// Pops up a message box to the user with the given message
    /// </summary>
    SolidWorksMessageBoxResult ShowMessageBox(string message, SolidWorksMessageBoxIcon icon = SolidWorksMessageBoxIcon.Information,
        SolidWorksMessageBoxButtons buttons = SolidWorksMessageBoxButtons.Ok);

    #endregion
}
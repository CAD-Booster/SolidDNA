using System;

namespace CADBooster.SolidDna;

/// <summary>
/// A base class to implement to become a SolidDna plug-in. 
/// The compiled dll of SolidDna must be in the same location as 
/// the plug-in dll to be discovered
/// </summary>
public abstract class SolidPlugIn
{
    #region Public Properties

    /// <summary>
    /// Gets the desired title to show in the SolidWorks add-in
    /// </summary>
    /// <returns></returns>
    public abstract string AddInTitle { get; }

    /// <summary>
    /// Gets the desired description to show in the SolidWorks add-in
    /// </summary>
    /// <returns></returns>
    public abstract string AddInDescription { get; }

    /// <summary>
    /// The add-in that owns this plugin.
    /// </summary>
    public SolidAddIn ParentAddIn { get; set; }

    #endregion

    #region Public Methods

    /// <summary>
    /// Called when the add-in is loaded into SolidWorks and connected.
    /// This method is called after the add-in's ConnectedToSolidWorks method.
    /// </summary>
    /// <returns></returns>
    public abstract void ConnectedToSolidWorks();

    /// <summary>
    /// Called when the add-in is unloaded from SolidWorks and disconnected.
    /// This method is called after the add-in's DisconnectedFromSolidWorks method.
    /// </summary>
    /// <returns></returns>
    public abstract void DisconnectedFromSolidWorks();

    #endregion
}

/// <summary>
/// A base class to implement to become a SolidDna plug-in. 
/// The compiled dll of SolidDna must be in the same location as 
/// the plug-in dll to be discovered
/// </summary>
[Obsolete("SolidPlugin<T> does not work as spected, so use SolidPlugIn.")]
public abstract class SolidPlugIn<T> : SolidPlugIn
{
    private SolidAddIn mParentAddIn;

    /// <summary>
    /// The add-in that contains this plugin.
    /// We override the default add-in property so we can call <see cref="PlugInIntegration"/> methods and include the generic T.
    /// Unfortunately, setting the add-in must happen before we create the plugin, so this doesn't work.
    /// </summary>
    public new SolidAddIn ParentAddIn
    {
        get => mParentAddIn;
        set
        {
            // If we already have an add-in, do not change it.
            if (mParentAddIn != null) return;
            mParentAddIn = value;

            // Once we have our parent add-in, we can call these methods.

            // Disable discovering plug-in and make it quicker by auto-adding it
            ParentAddIn.PlugInIntegration.AutoDiscoverPlugins = false;

            // Add this plug-in
            ParentAddIn.PlugInIntegration.AddPlugIn<T>();
        }
    }
}
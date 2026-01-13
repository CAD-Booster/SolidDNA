using CADBooster.SolidDna;
using System.IO;
using System.Runtime.InteropServices;

namespace SolidDna.CustomProperties;

/// <summary>
/// Register as a SolidWorks Add-in
/// </summary>
[Guid("1010E01C-C249-421B-9B96-D0849CBCB03B")] // Replace the GUID with your own.
[ComVisible(true)]
public class SolidDnaAddInIntegration : SolidAddIn
{
    // <Inheritdoc />
    public override void PreConnectToSolidWorks()
    {
    }

    // <Inheritdoc />
    public override void PreLoadPlugIns()
    {
    }

    // <Inheritdoc />
    public override void ApplicationStartup()
    {
    }
}

/// <summary>
/// Register as SolidDna Plugin
/// </summary>
[Guid("38BBAACF-95B0-4831-A48A-6C4EE0682B33")] // Replace the GUID with your own.
[ComVisible(true)]
public class CustomPropertiesSolidDnaPlugin : SolidPlugIn
{
    #region Private Members

    /// <summary>
    /// The Taskpane UI for our plug-in
    /// </summary>
    private TaskpaneIntegration<TaskpaneUserControlHost, SolidDnaAddInIntegration> mTaskpane;

    #endregion

    #region Public Properties

    /// <summary>
    /// My Add-in description
    /// </summary>
    public override string AddInDescription => "An example of manipulating Custom Properties inside a SolidWorks model";

    /// <summary>
    /// My Add-in title
    /// </summary>
    public override string AddInTitle => "SolidDNA Custom Properties";

    #endregion

    #region Connect To SolidWorks

    public override void ConnectedToSolidWorks()
    {
        // Create our taskpane
        mTaskpane = new TaskpaneIntegration<TaskpaneUserControlHost, SolidDnaAddInIntegration> { Icon = Path.Combine(this.AssemblyPath(), "logo-small.bmp"), WpfControl = new CustomPropertiesUI() };

        // Add to taskpane
        mTaskpane.AddToTaskpaneAsync();
    }

    public override void DisconnectedFromSolidWorks()
    {
    }

    #endregion
}
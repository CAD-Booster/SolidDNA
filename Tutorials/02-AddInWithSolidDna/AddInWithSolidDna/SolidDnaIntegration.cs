using CADBooster.SolidDna;
using System.IO;
using System.Runtime.InteropServices;

namespace AddInWithSolidDna;

/// <summary>
/// Register as a SolidWorks Add-in
/// </summary>
[Guid("DBD0F7F2-FD55-4512-8C97-28AC70719FEC"), ComVisible(true)]  // Replace the GUID with your own.
public class MyAddinIntegration : SolidAddIn
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
/// My first SolidDna Plug-in
/// </summary>
[Guid("8442EC6F-A261-4392-8459-B9F98A73D4DA"), ComVisible(true)]  // Replace the GUID with your own.
public class MySolidDnaPlugin : SolidPlugIn
{
    #region Private Members

    /// <summary>
    /// The Taskpane UI for our plug-in
    /// </summary>
    private TaskpaneIntegration<MyTaskpaneUI, MyAddinIntegration> mTaskpane;

    #endregion

    #region Public Properties

    /// <summary>
    /// My Add-in description
    /// </summary>
    public override string AddInDescription => "My Addin Description";

    /// <summary>
    /// My Add-in title
    /// </summary>
    public override string AddInTitle => "My Addin Title";

    #endregion

    #region Connect To SolidWorks

    public override void ConnectedToSolidWorks()
    {
        // Create our taskpane
        mTaskpane = new TaskpaneIntegration<MyTaskpaneUI, MyAddinIntegration>
        {
            Icon = Path.Combine(this.AssemblyPath(), "logo-small.bmp"),
            WpfControl = new MyAddinControl()
        };

        mTaskpane.AddToTaskpaneAsync();
    }

    public override void DisconnectedFromSolidWorks()
    {

    }

    #endregion
}
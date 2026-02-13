namespace CADBooster.SolidDna;

/// <summary>
/// A blank AddIn class that is used when registering our add-in with COM.
/// </summary>
public class BlankSolidAddIn : SolidAddIn
{
    #region Constructor

    /// <summary>
    /// Default constructor
    /// </summary>
    public BlankSolidAddIn()
    {
    }

    #endregion

    #region AddIn Methods

    /// <inheritdoc />
    public override void ApplicationStartup()
    {
    }

    /// <inheritdoc />
    public override void PreConnectToSolidWorks()
    {
    }

    /// <inheritdoc />
    public override void PreLoadPlugIns()
    {
    }

    #endregion
}
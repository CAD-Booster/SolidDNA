namespace CADBooster.SolidDna;

/// <summary>
/// Creates a blank AddIn integration class
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
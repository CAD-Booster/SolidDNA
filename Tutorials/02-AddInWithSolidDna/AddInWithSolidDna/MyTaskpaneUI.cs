using CADBooster.SolidDna;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AddInWithSolidDna;

[ProgId(MyProgId)]
public partial class MyTaskpaneUI : UserControl, ITaskpaneControl
{
    #region Private Members

    /// <summary>
    /// Our unique ProgId for SolidWorks to find and load us
    /// </summary>
    private const string MyProgId = "AngelSix.SolidDna.Taskpane";

    #endregion

    #region Public Properties

    public string ProgId => MyProgId;

    #endregion
}
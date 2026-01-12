using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SolidWorks.AddInWithoutDna;

[ProgId(TaskpaneIntegration.SWTASKPANE_PROGID)]
public partial class TaskpaneHostUI : UserControl
{
    public TaskpaneHostUI()
    {
        InitializeComponent();
    }
}
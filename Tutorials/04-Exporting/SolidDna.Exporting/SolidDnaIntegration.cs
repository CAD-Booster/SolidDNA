using CADBooster.SolidDna;
using System.Runtime.InteropServices;
using static CADBooster.SolidDna.SolidWorksEnvironment;

namespace SolidDna.Exporting;

/// <summary>
/// Register as a SolidWorks Add-in
/// </summary>
[Guid("6D769D97-6103-4495-AACD-63CDD0EC396B")] // Replace the GUID with your own.
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
/// Register as SolidDna Plug-in
/// </summary>
[Guid("B94EEB04-83DC-483D-B598-68DC3FC3CAF4")] // Replace the GUID with your own.
[ComVisible(true)]
public class MySolidDnaPlugin : SolidPlugIn
{
    #region Public Properties

    /// <summary>
    /// My Add-in description
    /// </summary>
    public override string AddInDescription => "An example of Command Items and exporting";

    /// <summary>
    /// My Add-in title
    /// </summary>
    public override string AddInTitle => "SolidDNA Exporting";

    #endregion

    #region Connect To SolidWorks

    public override void ConnectedToSolidWorks()
    {
        // Part commands.
        // You don't need to use the return value, but it's there if you want to.
        var partGroup = Application.CommandManager.CreateCommandTab(
            "Export Part",
            120_000,
            [
                new CommandManagerItem
                {
                    Name = "DXF",
                    Tooltip = "DXF",
                    ImageIndex = 0,
                    Hint = "Export part as DXF",
                    VisibleForAssemblies = false,
                    VisibleForDrawings = false,
                    VisibleForParts = true,
                    OnClick = FileExporting.ExportPartAsDxf,
                },

                new CommandManagerItem
                {
                    Name = "STEP",
                    Tooltip = "STEP",
                    ImageIndex = 2,
                    Hint = "Export part as STEP",
                    VisibleForAssemblies = false,
                    VisibleForDrawings = false,
                    VisibleForParts = true,
                    OnClick = FileExporting.ExportModelAsStep,
                },
            ],
            "icons{0}.png",
            "icons{0}.png");

        // Assembly commands
        var assemblyGroup = Application.CommandManager.CreateCommandTab(
            "Export Assembly",
            120_001,
            [
                new CommandManagerItem
                {
                    Name = "STEP",
                    Tooltip = "STEP",
                    ImageIndex = 2,
                    Hint = "Export assembly as STEP",
                    VisibleForAssemblies = true,
                    VisibleForDrawings = false,
                    VisibleForParts = false,
                    OnClick = FileExporting.ExportModelAsStep,
                },
            ],
            "icons{0}.png",
            "icons{0}.png");

        // Drawing commands
        var drawingGroup = Application.CommandManager.CreateCommandTab(
            "Export Drawing",
            120_002,
            [
                new CommandManagerItem
                {
                    Name = "PDF",
                    Tooltip = "PDF",
                    Hint = "Export drawing as PDF",
                    ImageIndex = 1,
                    VisibleForAssemblies = false,
                    VisibleForDrawings = true,
                    VisibleForParts = false,
                    OnClick = FileExporting.ExportDrawingAsPdf,
                },
            ],
            "icons{0}.png",
            "icons{0}.png");
    }

    public override void DisconnectedFromSolidWorks()
    {
    }

    #endregion
}
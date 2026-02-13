using CADBooster.SolidDna;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace SolidDna.Menus;

/// <summary>
/// Register as a SolidWorks Add-in
/// </summary>
[Guid("DF33DFF8-AE92-4DDA-84BF-624869A6A9E9")] // Replace the GUID with your own.
[ComVisible(true)]
public class SolidDnaAddInIntegration : SolidAddIn
{
    /// <summary>
    /// The Command Manager that belongs to this add-in.
    /// </summary>
    public static SolidDnaAddInIntegration Instance { get; private set; }

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
        Instance = this;
    }
}

/// <summary>
/// Register as SolidDna Plug-in
/// </summary>
[Guid("A3B49158-4483-4679-A249-64E7F5FE6042")] // Replace the GUID with your own.
[ComVisible(true)]
public class MySolidDnaPlugin : SolidPlugIn
{
    #region Public Properties

    /// <summary>
    /// My Add-in description
    /// </summary>
    public override string AddInDescription => "An example of Command Items, Flyouts and Menu";

    /// <summary>
    /// My Add-in title
    /// </summary>
    public override string AddInTitle => "SolidDNA CommandItems";

    /// <summary>
    /// Toggle value
    /// </summary>
    private bool mToggle;

    #endregion

    #region Connect To SolidWorks

    public override void ConnectedToSolidWorks() => CreateMenus();

    public override void DisconnectedFromSolidWorks()
    {
    }

    /// <summary>
    /// Create a toolbar with a flyout, separators and other items, plus a Tools menu.
    /// </summary>
    private void CreateMenus()
    {
        // Get the command manager for our add-in
        var commandManager = SolidDnaAddInIntegration.Instance.CommandManager;

        // Create a path to the icons. The {0} will be replaced by the image index specified in the CommandManagerItem.ImageIndex property.
        var imageFormat = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "icons{0}.png");

        // Create a flyout (group) with a list of items
        var flyout = commandManager.CreateFlyoutGroup2(
            title: "CreateFlyoutGroup2 Example",
            CreateCommandItems(),
            mainIconPathFormat: imageFormat,
            iconListsPathFormat: imageFormat,
            tooltip: "CreateFlyoutGroup2 tooltip",
            hint: "CreateFlyoutGroup2 hint",
            CommandManagerItemTabView.IconWithTextBelow,
            CommandManagerFlyoutType.FirstItemVisible
        );

        // Create a command group / command tab / toolbar 
        var commandManagerItems = CreateCommandItemsForToolbar(flyout);

        commandManager.CreateCommandTab(
            title: "CreateCommandTab Example",
            id: 150_000,
            commandManagerItems: commandManagerItems,
            mainIconPathFormat: imageFormat,
            iconListsPathFormat: imageFormat);

        // Create a command menu / Tools menu 
        commandManager.CreateCommandMenu(
            title: "CreateCommandMenu Example",
            id: 150_001,
            commandManagerItems: CreateCommandItems());
    }

    /// <summary>
    /// Create a list of command items for a toolbar, Tools menu and a flyout. Create this list for each menu so its IDs are unique.
    /// </summary>
    public List<CommandManagerItem> CreateCommandItems() =>
    [
        new CommandManagerItem
        {
            Name = "DeselectedDisabled item",
            Tooltip = "DeselectedDisabled item Tooltip",
            ImageIndex = 0,
            Hint = "DeselectedDisabled item Hint",
            VisibleForDrawings = true,
            VisibleForAssemblies = true,
            VisibleForParts = true,
            OnClick = () => System.Windows.MessageBox.Show("CreateCommandTab DeselectedDisabled item clicked!"),
            OnStateCheck = (args) => args.Result = CommandManagerItemState.DeselectedDisabled,
        },

        new CommandManagerItem
        {
            Name = "DeselectedEnabled item",
            Tooltip = "DeselectedEnabled item Tooltip",
            ImageIndex = 1,
            Hint = "DeselectedEnabled item Hint",
            VisibleForDrawings = true,
            VisibleForAssemblies = true,
            VisibleForParts = true,
            OnClick = () => System.Windows.MessageBox.Show("CreateCommandTab DeselectedEnabled item clicked!"),
            OnStateCheck = (args) => args.Result = CommandManagerItemState.DeselectedEnabled,
        },


        new CommandManagerItem
        {
            Name = "SelectedDisabled item",
            Tooltip = "SelectedDisabled item Tooltip",
            ImageIndex = 2,
            Hint = "SelectedDisabled item Hint",
            VisibleForDrawings = true,
            VisibleForAssemblies = true,
            VisibleForParts = true,
            OnClick = () => System.Windows.MessageBox.Show("CreateCommandTab SelectedDisabled item clicked!"),
            OnStateCheck = (args) => args.Result = CommandManagerItemState.SelectedDisabled,
        },

        new CommandManagerItem
        {
            Name = "SelectedEnabled item",
            Tooltip = "SelectedEnabled item Tooltip",
            ImageIndex = 0,
            Hint = "SelectedEnabled item Hint",
            VisibleForDrawings = true,
            VisibleForAssemblies = true,
            VisibleForParts = true,
            OnClick = () => System.Windows.MessageBox.Show("CreateCommandTab SelectedEnabled item clicked!"),
            OnStateCheck = (args) => args.Result = CommandManagerItemState.SelectedEnabled,
        },

        new CommandManagerItem
        {
            Name = "Hidden item",
            Tooltip = "Hidden item Tooltip",
            ImageIndex = 1,
            Hint = "Hidden item Hint",
            VisibleForDrawings = true,
            VisibleForAssemblies = true,
            VisibleForParts = true,
            OnClick = () => System.Windows.MessageBox.Show("CreateCommandTab Hidden item clicked!"),
            OnStateCheck = (args) => args.Result = CommandManagerItemState.Hidden,
        },

        new CommandManagerItem
        {
            Name = "Toggle item",
            Tooltip = "Toggle item Tooltip",
            ImageIndex = 2,
            Hint = "Toggle item Hint",
            VisibleForDrawings = true,
            VisibleForAssemblies = true,
            VisibleForParts = true,
            OnClick = () => mToggle = !mToggle,
            OnStateCheck = (args) =>
                args.Result = mToggle ? CommandManagerItemState.SelectedEnabled : CommandManagerItemState.DeselectedEnabled,
        },
    ];

    /// <summary>
    /// Create a list of command items for a toolbar. A toolbar can hold normal items (buttons), flyouts and separators, all of which implement ICommandManagerItem.
    /// So the reuse the list of command items from this example, we cast them to ICommandManagerItem first.
    /// Add a separator and a flyout
    /// </summary>
    /// <param name="flyout"></param>
    public List<ICommandManagerItem> CreateCommandItemsForToolbar(CommandManagerFlyout flyout) =>
    [
        flyout,

        // Add a separator
        new CommandManagerSeparator(),

        // Add a list of items
        new CommandManagerItem
        {
            Name = "DeselectedDisabled item",
            Tooltip = "DeselectedDisabled item Tooltip",
            ImageIndex = 0,
            Hint = "DeselectedDisabled item Hint",
            VisibleForDrawings = true,
            VisibleForAssemblies = true,
            VisibleForParts = true,
            OnClick = () => System.Windows.MessageBox.Show("CreateCommandTab DeselectedDisabled item clicked!"),
            OnStateCheck = (args) => args.Result = CommandManagerItemState.DeselectedDisabled,
        },

        new CommandManagerItem
        {
            Name = "DeselectedEnabled item",
            Tooltip = "DeselectedEnabled item Tooltip",
            ImageIndex = 1,
            Hint = "DeselectedEnabled item Hint",
            VisibleForDrawings = true,
            VisibleForAssemblies = true,
            VisibleForParts = true,
            OnClick = () => System.Windows.MessageBox.Show("CreateCommandTab DeselectedEnabled item clicked!"),
            OnStateCheck = (args) => args.Result = CommandManagerItemState.DeselectedEnabled,
        },


        new CommandManagerSeparator(),

        new CommandManagerItem
        {
            Name = "SelectedDisabled item",
            Tooltip = "SelectedDisabled item Tooltip",
            ImageIndex = 2,
            Hint = "SelectedDisabled item Hint",
            VisibleForDrawings = true,
            VisibleForAssemblies = true,
            VisibleForParts = true,
            OnClick = () => System.Windows.MessageBox.Show("CreateCommandTab SelectedDisabled item clicked!"),
            OnStateCheck = (args) => args.Result = CommandManagerItemState.SelectedDisabled,
        },

        new CommandManagerItem
        {
            Name = "SelectedEnabled item",
            Tooltip = "SelectedEnabled item Tooltip",
            ImageIndex = 0,
            Hint = "SelectedEnabled item Hint",
            VisibleForDrawings = true,
            VisibleForAssemblies = true,
            VisibleForParts = true,
            OnClick = () => System.Windows.MessageBox.Show("CreateCommandTab SelectedEnabled item clicked!"),
            OnStateCheck = (args) => args.Result = CommandManagerItemState.SelectedEnabled,
        },


        new CommandManagerSeparator(),

        new CommandManagerItem
        {
            Name = "Hidden item",
            Tooltip = "Hidden item Tooltip",
            ImageIndex = 1,
            Hint = "Hidden item Hint",
            VisibleForDrawings = true,
            VisibleForAssemblies = true,
            VisibleForParts = true,
            OnClick = () => System.Windows.MessageBox.Show("CreateCommandTab Hidden item clicked!"),
            OnStateCheck = (args) => args.Result = CommandManagerItemState.Hidden,
        },

        new CommandManagerItem
        {
            Name = "Toggle item",
            Tooltip = "Toggle item Tooltip",
            ImageIndex = 2,
            Hint = "Toggle item Hint",
            VisibleForDrawings = true,
            VisibleForAssemblies = true,
            VisibleForParts = true,
            OnClick = () => mToggle = !mToggle,
            OnStateCheck = (args) =>
                args.Result = mToggle ? CommandManagerItemState.SelectedEnabled : CommandManagerItemState.DeselectedEnabled,
        },
    ];

    #endregion
}
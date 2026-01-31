using CADBooster.SolidDna;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using static CADBooster.SolidDna.SolidWorksEnvironment;

namespace SolidDna.Menus;

/// <summary>
/// Register as a SolidWorks Add-in
/// </summary>
[Guid("DF33DFF8-AE92-4DDA-84BF-624869A6A9E9")] // Replace the GUID with your own.
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
        // Icons for command manager (toolbar, flyout, menu)
        var iconsFormat = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "Icons", "icons{0}.png");

        // Icons for context menu popup items
        var iconBlueFormat = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "Icons", "icon_blue{0}.png");
        var iconGreenFormat = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "Icons", "icon_green{0}.png");
        var iconRedFormat = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "Icons", "icon_red{0}.png");
        var iconDefaultFormat = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "Icons", "icon_default{0}.png");

        // Create a flyout (group) with a list of items
        var flyout = ParentAddIn.CommandManager.CreateFlyoutGroup2(
            title: "CreateFlyoutGroup2 Example",
            CreateCommandItems(),
            mainIconPathFormat: iconsFormat,
            iconListsPathFormat: iconsFormat,
            tooltip: "CreateFlyoutGroup2 tooltip",
            hint: "CreateFlyoutGroup2 hint",
            CommandManagerItemTabView.IconWithTextBelow,
            CommandManagerFlyoutType.FirstItemVisible
        );

        // Create a command group / command tab / toolbar 
        var commandManagerItems = CreateCommandItemsForToolbar(flyout);

        ParentAddIn.CommandManager.CreateCommandTab(
            title: "CreateCommandTab Example",
            id: 150_000,
            commandManagerItems: commandManagerItems.ToList(),
            mainIconPathFormat: iconsFormat,
            iconListsPathFormat: iconsFormat);

        // Create a command menu / Tools menu 
        ParentAddIn.CommandManager.CreateCommandMenu(
            title: "CreateCommandMenu Example",
            id: 150_001,
            commandManagerItems: CreateCommandItems());

        // Create context items that appear when right-clicking on an object
        ParentAddIn.CommandManager.CreateContextMenuItems([
            // Context menu icons: Small icon buttons for specific selection types. 
            // Contained in a menu popup that appears on right click over the context menu
            ..CreateCommandContextIcons(iconBlueFormat, iconGreenFormat, iconRedFormat, iconDefaultFormat),
            
            // Context menu items: Text-based menu items that can be organized in groups and submenus. 
            // Contained in a context menu that appears on right click
            ..CreateCommandContextItems()
        ]);
    }

    #region CreateCommandItems
    /// <summary>
    /// Create a list of command items for a toolbar, Tools menu and a flyout. Create this list for each menu so its IDs are unique.
    /// </summary>
    public List<CommandManagerItem> CreateCommandItems() =>
    [
        // We cant hide item in ToolBar by document type, but it can be disabled manually
        new CommandManagerItem 
        {
            Name = "Item for assembly",
            Tooltip = "Item tool tip",
            ImageIndex = 0,
            Hint = "Item disabled in tool bar by active dock type (Assembly)",
            VisibleForDrawings = false,
            VisibleForAssemblies = false,
            VisibleForParts = false,
            OnClick = () => ShowMessage("CreateCommandTab DeselectedDisabled item clicked!"),
            OnStateCheck = (args) =>
            { 
                if(Application.ActiveModel?.IsAssembly is true)
                    args.Result = CommandManagerItemState.DeselectedDisabled;
            }
        },
        new CommandManagerItem 
        {
            Name = "DeselectedDisabled item",
            Tooltip = "DeselectedDisabled item Tooltip",
            ImageIndex = 0,
            Hint = "DeselectedDisabled item Hint",
            VisibleForDrawings = true,
            VisibleForAssemblies = true,
            VisibleForParts = true,
            OnClick = () => ShowMessage("CreateCommandTab DeselectedDisabled item clicked!"),
            OnStateCheck = (args) => args.Result = CommandManagerItemState.DeselectedDisabled
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
            OnClick = () => ShowMessage("CreateCommandTab DeselectedEnabled item clicked!"),
            OnStateCheck = (args) => args.Result = CommandManagerItemState.DeselectedEnabled
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
            OnClick = () => ShowMessage("CreateCommandTab SelectedDisabled item clicked!"),
            OnStateCheck = (args) => args.Result = CommandManagerItemState.SelectedDisabled
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
            OnClick = () => ShowMessage("CreateCommandTab SelectedEnabled item clicked!"),
            OnStateCheck = (args) => args.Result = CommandManagerItemState.SelectedEnabled
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
            OnClick = () => ShowMessage("CreateCommandTab Hidden item clicked!"),
            OnStateCheck = (args) => args.Result = CommandManagerItemState.Hidden
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
                args.Result = mToggle ? CommandManagerItemState.SelectedEnabled : CommandManagerItemState.DeselectedEnabled
        }
    ];
    #endregion

    #region CreateCommandItemsForToolbar
    /// <summary>
    /// Create a list of command items for a toolbar. A toolbar can hold normal items (buttons), flyouts and separators, all of which implement ICommandManagerItem.
    /// So the reuse the list of command items from this example, we cast them to ICommandManagerItem first.
    /// Add a separator and a flyout
    /// </summary>
    /// <param name="flyout"></param>
    public IEnumerable<ICommandManagerItem> CreateCommandItemsForToolbar(CommandManagerFlyout flyout) 
        => [
            // Add the flyout (group), which also contains a list of items
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
                OnClick = () => ShowMessage("CreateCommandTab DeselectedDisabled item clicked!"),
                OnStateCheck = (args) => args.Result = CommandManagerItemState.DeselectedDisabled
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
                OnClick = () => ShowMessage("CreateCommandTab DeselectedEnabled item clicked!"),
                OnStateCheck = (args) => args.Result = CommandManagerItemState.DeselectedEnabled
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
                OnClick = () => ShowMessage("CreateCommandTab SelectedDisabled item clicked!"),
                OnStateCheck = (args) => args.Result = CommandManagerItemState.SelectedDisabled
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
                OnClick = () => ShowMessage("CreateCommandTab SelectedEnabled item clicked!"),
                OnStateCheck = (args) => args.Result = CommandManagerItemState.SelectedEnabled
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
                OnClick = () => ShowMessage("CreateCommandTab Hidden item clicked!"),
                OnStateCheck = (args) => args.Result = CommandManagerItemState.Hidden
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
                    args.Result = mToggle ? CommandManagerItemState.SelectedEnabled : CommandManagerItemState.DeselectedEnabled
            }
        ];
    #endregion

    #region CreateCommandContextIcons
    /// <summary>
    /// Creates a list of context menu icons for different selection types.
    /// These icons appear in the context menu when specific object are selected.
    /// </summary>
    /// <param name="iconBlueFormat">Path format for blue icons (e.g., "icon_blue{0}.png")</param>
    /// <param name="iconGreenFormat">Path format for green icons (e.g., "icon_green{0}.png")</param>
    /// <param name="iconRedFormat">Path format for red icons (e.g., "icon_red{0}.png")</param>
    /// <param name="iconDefaultFormat">Path format for default icons (e.g., "icon_default{0}.png")</param>
    /// <returns>A collection of command context icons for various selection types</returns>
    private IEnumerable<ICommandCreatable> CreateCommandContextIcons(string iconBlueFormat, string iconGreenFormat, string iconRedFormat, string iconDefaultFormat)
        => [
            // Select a body related Feature (like extrude) in the FeatureManager to see this icon
            new CommandContextIcon
            {
                Hint = "Icon for any body related Feature",
                OnClick = () => ShowMessage("Context icon for Feature clicked"),
                OnStateCheck = args => args.Result = CommandManagerItemState.DeselectedEnabled,
                IconPathFormat = iconDefaultFormat,
                SelectionType = SelectionType.Feature
            },

            // Select a component in the FeatureManager to see this icon
            new CommandContextIcon
            {
                Hint = "Icon for Component",
                OnClick = () => ShowMessage("Context icon for Component clicked"),
                OnStateCheck = args => args.Result = CommandManagerItemState.DeselectedEnabled,
                IconPathFormat = iconRedFormat,
                SelectionType = SelectionType.Component
            },

            // Select an edge in the viewport to see this icon
            new CommandContextIcon
            {
                Hint = "Green Icon for Edge",
                OnClick = () => ShowMessage("Context icon for Edge clicked"),
                OnStateCheck = args => args.Result = CommandManagerItemState.DeselectedEnabled,
                IconPathFormat = iconGreenFormat,
                SelectionType = SelectionType.Edge
            },

            // Select a face in the viewport to see this icon
            new CommandContextIcon
            {
                Hint = "Blue Icon for Face",
                OnClick = () => ShowMessage("Context icon for Face clicked"),
                OnStateCheck = args => args.Result = CommandManagerItemState.DeselectedEnabled,
                IconPathFormat = iconBlueFormat,
                SelectionType = SelectionType.Face
            },

            // Note: You can't see this one
            // This icon isn't visible since no popup is shown when a vertex is selected.
            // Use CommandContextItem instead for vertex selection.
            new CommandContextIcon
            {
                Hint = "Green Icon for Vertex",
                OnClick = () => ShowMessage("Context icon for Vertex clicked"),
                OnStateCheck = args => args.Result = CommandManagerItemState.DeselectedEnabled,
                IconPathFormat = iconGreenFormat,
                SelectionType = SelectionType.Vertex
            },

            // Multiple icons in same menu for Feature Folder
            // These icons appear when selecting a user-created folder that can contain features or components
            // Note: This does not apply to SolidWorks built-in folders like Sensors or Annotations
            new CommandContextIcon
            {
                Hint = "Multiple icons in same menu - Blue Icon for Feature Folder",
                OnClick = () => ShowMessage("Context icon for Feature Folder (Blue) clicked"),
                OnStateCheck = args => args.Result = CommandManagerItemState.DeselectedEnabled,
                IconPathFormat = iconBlueFormat,
                SelectionType = SelectionType.FeatureFolder
            },
            new CommandContextIcon
            {
                Hint = "Multiple icons in same menu - Green Icon for Feature Folder",
                OnClick = () => ShowMessage("Context icon for Feature Folder (Green) clicked"),
                OnStateCheck = args => args.Result = CommandManagerItemState.DeselectedEnabled,
                IconPathFormat = iconGreenFormat,
                SelectionType = SelectionType.FeatureFolder
            },
            new CommandContextIcon
            {
                Hint = "Multiple icons in same menu - Red Icon for Feature Folder",
                OnClick = () => ShowMessage("Context icon for Feature Folder (Red) clicked"),
                OnStateCheck = args => args.Result = CommandManagerItemState.DeselectedEnabled,
                IconPathFormat = iconRedFormat,
                SelectionType = SelectionType.FeatureFolder
            }
        ];
    #endregion

    #region CreateCommandContextItems
    /// <summary>
    /// Creates a list of context menu items for different selection types.
    /// These text-based menu items appear in the context menu when specific objects are selected.
    /// Supports nested groups and items to create hierarchical menu structures.
    /// </summary>
    /// <returns>A collection of command context items and groups for various selection types</returns>
    private IEnumerable<ICommandCreatable> CreateCommandContextItems()
        => [
            // Select Component in feature tree
            new CommandContextItem
            {
                Name = "RootItem",
                Hint = "RootItem Hint",
                OnClick = () => ShowMessage(),
                OnStateCheck = args => args.Result = CommandManagerItemState.SelectedEnabled,
                SelectionType = SelectionType.Component
            },
            // Root group that demonstrates nested context menu items
            new CommandContextGroup
            {
                Name = "RootGroup",
                Items = [..
                    // Reuse command items and convert them into context items
                    CreateCommandItems().AsCommandContextItems(SelectionType.Component),
                    // A specific item for plane selection
                    new CommandContextItem
                    {
                        Name = "PlaneItem",
                        Hint = "PlaneItem Hint",
                        OnClick = () => ShowMessage(),
                        SelectionType = SelectionType.DatumPlane
                    },
                    // Nested group example
                    new CommandContextGroup
                    {
                        Name = "SubGroup",
                        Items = [
                            // Nested item example
                            new CommandContextItem
                            {
                                Name = "SubSubItem",
                                Hint = "SubSubItem Hint",
                                OnClick = () => ShowMessage(),
                                SelectionType = SelectionType.Component
                            },
                            // Nested group within a nested group
                            new CommandContextGroup
                            {
                                Name = "SubSubGroup",
                                Items = [
                                    // Deeply nested item example
                                    new CommandContextItem
                                    {
                                        Name = "SubSubSubItem",
                                        Hint = "SubSubSubItem Hint",
                                        OnClick = () => ShowMessage(),
                                        SelectionType = SelectionType.Component
                                    }
                                ]
                            },
                        ]
                    }
                ]
            }
        ];
    #endregion

    /// <summary>
    /// Shows a message when a click handler is invoked.
    /// </summary>
    /// <param name="message">Optional message to display. Defaults to "Context menu item clicked" if not provided.</param>
    private static void ShowMessage(string? message = null)
        => SolidWorksEnvironment.Application.ShowMessageBox(message ?? "Item clicked");

    #endregion
}
using CADBooster.SolidDna;
using SolidWorks.Interop.swconst;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using static CADBooster.SolidDna.SolidWorksEnvironment;

namespace SolidDna.CommandItems
{
    /// <summary>
    /// Register as a SolidWorks Add-in
    /// </summary>
    [Guid("0BE99D58-34E4-4036-B4CF-EDDD52B1EDAD"), ComVisible(true)]  // Replace the GUID with your own.
    public class SolidDnaAddinIntegration : SolidAddIn
    {
        /// <summary>
        /// Specific application start-up code
        /// </summary>
        public override void ApplicationStartup()
        {

        }

        public override void PreLoadPlugIns()
        {

        }

        public override void PreConnectToSolidWorks()
        {

        }
    }

    /// <summary>
    /// Register as SolidDna Plug-in
    /// </summary>
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

        public override void ConnectedToSolidWorks()
        {
            CreateMenus();
        }

        public override void DisconnectedFromSolidWorks()
        {

        }

        /// <summary>
        /// Create a toolbar with a flyout, separators and other items, plus a Tools menu.
        /// </summary>
        private void CreateMenus()
        {
            var imageFormat = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "icons{0}.png");

            // Create a flyout (group) with a list of items
            var flyout = Application.CommandManager.CreateFlyoutGroup2(
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
            
            Application.CommandManager.CreateCommandTab(
                title: "CreateCommandTab Example",
                id: 150_000,
                commandManagerItems: commandManagerItems,
                mainIconPathFormat: imageFormat,
                iconListsPathFormat: imageFormat);

            // Create a command menu / Tools menu 
            Application.CommandManager.CreateCommandMenu(
                title: "CreateCommandMenu Example",
                id: 150_001,
                commandManagerItems: CreateCommandItems());

            Action onContextMenuItemClick = () => System.Windows.MessageBox.Show("Context menu item clicked");

            Application.CommandManager.CreateContextMenuItems(
            [
                new CommandContextItem
                {
                    Name = "TestTest",
                    Hint = "TestTest Hint",
                    OnClick = () =>
                    {


                        using CompositeDisposable disposable = new();

                        var firstNote = Application.ActiveModel
                            .EnumerateNotes(disposable)
                            .FirstOrDefault(x => x.UnsafeObject.GetName() == "MyAwesomeName");

                        var notEmptyNotes = Application.ActiveModel
                            .EnumerateNotes(disposable)
                            .Select(x => x.Text)
                            .Where(x => !string.IsNullOrWhiteSpace(x))
                            .ToArray();

                        var extractedDigits = Application.ActiveModel
                            .EnumerateNotes(disposable)
                            .Where(x => !x.UnsafeObject.BehindSheet)
                            .Select(x => x.Text)
                            .Where(x => x.Length > 10)
                            .Take(50)
                            .Select(x => Regex.Match(x, @".*My\s+awesome\s+text\s+{(\d+)}$"))
                            .Where(x => x.Success)
                            .Select(x => x.Groups[1])
                            .Select(x => x.Value)
                            .ToArray();


                        return; // When method ends all SolidDnaObjects will be disposed with CompositeDisposable

                    },
                    OnStateCheck = args => args.Result = CommandManagerItemState.SelectedEnabled,
                    SelectionType = swSelectType_e.swSelEVERYTHING
                },
                new CommandContextItem
                {
                    Name = "RootItem",
                    Hint = "RootItem Hint",
                    OnClick = onContextMenuItemClick,
                    OnStateCheck = args => args.Result = CommandManagerItemState.SelectedEnabled,
                    SelectionType = swSelectType_e.swSelCOMPONENTS
                },
                new CommandContextMenuGroup
                {
                    Name = "RootGroup",
                    Items = 
                    [..
                        CreateCommandItems().AsCommandCreatable(x => swSelectType_e.swSelCOMPONENTS),
                        new CommandContextItem
                        {
                            Name = "PlaneItem",
                            Hint = "PlaneItem Hint",
                            OnClick = onContextMenuItemClick,
                            SelectionType = swSelectType_e.swSelDATUMPLANES
                        },
                        new CommandContextMenuGroup
                        {
                            Name = "SubGroup",
                            Items = 
                            [
                                new CommandContextItem
                                {
                                    Name = "SubSubItem",
                                    Hint = "SubSubItem Hint",
                                    OnClick = onContextMenuItemClick,
                                    SelectionType = swSelectType_e.swSelCOMPONENTS
                                },
                                new CommandContextMenuGroup
                                {
                                    Name = "SubSubGroup",
                                    Items = 
                                    [
                                        new CommandContextItem
                                        {
                                            Name = "SubSubSubItem",
                                            Hint = "SubSubSubItem Hint",
                                            OnClick = onContextMenuItemClick,
                                            SelectionType = swSelectType_e.swSelCOMPONENTS
                                        }
                                    ]
                                },
                            ]
                        }
                    ]
                }
            ]);
        }

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
                OnClick = () => System.Windows.MessageBox.Show("CreateCommandTab DeselectedDisabled item clicked!"),
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
                OnClick = () => System.Windows.MessageBox.Show("CreateCommandTab DeselectedDisabled item clicked!"),
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
                OnClick = () => System.Windows.MessageBox.Show("CreateCommandTab DeselectedEnabled item clicked!"),
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
                OnClick = () => System.Windows.MessageBox.Show("CreateCommandTab SelectedDisabled item clicked!"),
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
                OnClick = () => System.Windows.MessageBox.Show("CreateCommandTab SelectedEnabled item clicked!"),
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
                OnClick = () => System.Windows.MessageBox.Show("CreateCommandTab Hidden item clicked!"),
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

        /// <summary>
        /// Create a list of command items for a toolbar. A toolbar can hold normal items (buttons), flyouts and separators, all of which implement ICommandManagerItem.
        /// So the reuse the list of command items from this example, we cast them to ICommandManagerItem first.
        /// Add a separator and a flyout
        /// </summary>
        /// <param name="flyout"></param>
        public List<ICommandManagerItem> CreateCommandItemsForToolbar(CommandManagerFlyout flyout) =>
            new List<ICommandManagerItem>
            {
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
                    OnClick = () => System.Windows.MessageBox.Show("CreateCommandTab DeselectedDisabled item clicked!"),
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
                    OnClick = () => System.Windows.MessageBox.Show("CreateCommandTab DeselectedEnabled item clicked!"),
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
                    OnClick = () => System.Windows.MessageBox.Show("CreateCommandTab SelectedDisabled item clicked!"),
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
                    OnClick = () => System.Windows.MessageBox.Show("CreateCommandTab SelectedEnabled item clicked!"),
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
                    OnClick = () => System.Windows.MessageBox.Show("CreateCommandTab Hidden item clicked!"),
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
            };

        #endregion
    }
}

using CADBooster.SolidDna;
using SolidWorks.Interop.swconst;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
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
            var imageFormat = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "icons{0}.png");

            // FlyoutGroup
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

            // CommandTab
            Application.CommandManager.CreateCommandTab(
                title: "CreateCommandTab Example",
                id: 150_000,
                commandManagerItems: CreateCommandItems()
                    .Cast<ICommandManagerItem>()
                    // Append flyout
                    .Append(flyout)
                    .ToList(),
                mainIconPathFormat: imageFormat,
                iconListsPathFormat: imageFormat);

            // CommandMenu
            Application.CommandManager.CreateCommandMenu(
                title: "CreateCommandMenu Example",
                id: 150_001,
                commandManagerItems: CreateCommandItems());

            Action onContextMenuItemClick = () => System.Windows.MessageBox.Show("Context menu item clicked");

            Application.CommandManager.CreateContextMenuItems([
                new CommandContextItem()
                {
                    Name = "RootItem",
                    Hint = "RootItem Hint",
                    OnClick = onContextMenuItemClick,
                    OnStateCheck = args => args.Result = ItemState.SelectedEnabled,
                    SelectionType = swSelectType_e.swSelCOMPONENTS
                },
                new CommandContextMenuGroup()
                {
                    Name = "RootGroup",
                    Items =
                    [..
                        CreateCommandItems().AsCommandCreatable(x => swSelectType_e.swSelCOMPONENTS),
                        new CommandContextItem()
                        {
                            Name = "PlaneItem",
                            Hint = "PlaneItem Hint",
                            OnClick = onContextMenuItemClick,
                            SelectionType = swSelectType_e.swSelDATUMPLANES
                        },
                        new CommandContextMenuGroup()
                        {
                            Name = "SubGroup",
                            Items = [
                                new CommandContextItem()
                                {
                                    Name = "SubSubItem",
                                    Hint = "SubSubItem Hint",
                                    OnClick = onContextMenuItemClick,
                                    SelectionType = swSelectType_e.swSelCOMPONENTS
                                },
                                new CommandContextMenuGroup()
                                {
                                    Name = "SubSubGroup",
                                    Items = [
                                        new CommandContextItem()
                                        {
                                            Name = "SubSubItem",
                                            Hint = "SubSubItem Hint",
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
        /// Initialize command items to reuse them in example
        /// </summary>
        /// <returns>CommandManagerItems</returns>
        public List<CommandManagerItem> CreateCommandItems() =>
        [
            // We cant hide item in ToolBar by document type, but it can be disabled manually
            new CommandManagerItem {
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
                        args.Result = ItemState.DeselectedDisabled;
                }
            },
            new CommandManagerItem {
                Name = "DeselectedDisabled item",
                Tooltip = "DeselectedDisabled item Tooltip",
                ImageIndex = 0,
                Hint = "DeselectedDisabled item Hint",
                VisibleForDrawings = true,
                VisibleForAssemblies = true,
                VisibleForParts = true,
                OnClick = () => System.Windows.MessageBox.Show("CreateCommandTab DeselectedDisabled item clicked!"),
                OnStateCheck = (args) => args.Result = ItemState.DeselectedDisabled
            },
            new CommandManagerItem {
                Name = "DeselectedEnabled item",
                Tooltip = "DeselectedEnabled item Tooltip",
                ImageIndex = 1,
                Hint = "DeselectedEnabled item Hint",
                VisibleForDrawings = true,
                VisibleForAssemblies = true,
                VisibleForParts = true,
                OnClick = () => System.Windows.MessageBox.Show("CreateCommandTab DeselectedEnabled item clicked!"),
                OnStateCheck = (args) => args.Result = ItemState.DeselectedEnabled
            },
            new CommandManagerItem {
                Name = "SelectedDisabled item",
                Tooltip = "SelectedDisabled item Tooltip",
                ImageIndex = 2,
                Hint = "SelectedDisabled item Hint",
                VisibleForDrawings = true,
                VisibleForAssemblies = true,
                VisibleForParts = true,
                OnClick = () => System.Windows.MessageBox.Show("CreateCommandTab SelectedDisabled item clicked!"),
                OnStateCheck = (args) => args.Result = ItemState.SelectedDisabled
            },
            new CommandManagerItem {
                Name = "SelectedEnabled item",
                Tooltip = "SelectedEnabled item Tooltip",
                ImageIndex = 0,
                Hint = "SelectedEnabled item Hint",
                VisibleForDrawings = true,
                VisibleForAssemblies = true,
                VisibleForParts = true,
                OnClick = () => System.Windows.MessageBox.Show("CreateCommandTab SelectedEnabled item clicked!"),
                OnStateCheck = (args) => args.Result = ItemState.SelectedEnabled
            },
            new CommandManagerItem {
                Name = "Hidden item",
                Tooltip = "Hidden item Tooltip",
                ImageIndex = 1,
                Hint = "Hidden item Hint",
                VisibleForDrawings = true,
                VisibleForAssemblies = true,
                VisibleForParts = true,
                OnClick = () => System.Windows.MessageBox.Show("CreateCommandTab Hidden item clicked!"),
                OnStateCheck = (args) => args.Result = ItemState.Hidden
            },
            new CommandManagerItem {
                Name = "Toggle item",
                Tooltip = "Toggle item Tooltip",
                ImageIndex = 2,
                Hint = "Toggle item Hint",
                VisibleForDrawings = true,
                VisibleForAssemblies = true,
                VisibleForParts = true,
                OnClick = () => mToggle = !mToggle,
                OnStateCheck = (args) =>
                    args.Result = mToggle ?  ItemState.SelectedEnabled : ItemState.DeselectedEnabled
            }
        ];

        public override void DisconnectedFromSolidWorks()
        {
        }

        #endregion
    }
}

using CADBooster.SolidDna;
using System;
using System.Linq;
using System.Runtime.InteropServices;
using static CADBooster.SolidDna.SolidWorksEnvironment;

namespace SolidDna.Selection
{
    /// <summary>
    /// Register as a SolidWorks Add-in
    /// </summary>
    [Guid("CBB4ED3B-4472-4A79-9E90-D8A87C3345C6"), ComVisible(true)]  // Replace the GUID with your own.
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
        public override string AddInDescription => "An example of Select interrupts";

        /// <summary>
        /// My Add-in title
        /// </summary>
        public override string AddInTitle => "SolidDNA Select";

        #endregion

        #region Connect To SolidWorks

        public override void ConnectedToSolidWorks()
        {
            SelectedObject[] previous = [];

            Application.CommandManager.CreateCommandTab(
                title: "SelectCommandTab Example",
                id: 160_001,
                commandManagerItems:
                [
                    new CommandManagerItem
                    {
                        Name = "Remember selected items",
                        Tooltip = "Remember selected items",
                        Hint = "Remember selected items",
                        VisibleForDrawings = true,
                        VisibleForAssemblies = true,
                        VisibleForParts = true,
                        OnClick = () =>
                        {
                            previous.DisposeEach();
                            previous = [];
                            previous = Application.ActiveModel.SelectionManager.EnumerateSelectedObjects().ToArray();
                        }
                    },
                    new CommandManagerItem
                    {
                        Name = "Select previous items",
                        Tooltip = "Select previous items",
                        Hint = "Select previous items",
                        VisibleForDrawings = true,
                        VisibleForAssemblies = true,
                        VisibleForParts = true,
                        OnClick = () =>
                        {
                            Application.ActiveModel.SelectionManager.SelectObjects(previous, false, true);
                        }
                    },
                    new CommandManagerItem
                    {
                        Name = "Remove previous selected items",
                        Tooltip = "Remove previous selected items",
                        Hint = "Remove previous selected items",
                        VisibleForDrawings = true,
                        VisibleForAssemblies = true,
                        VisibleForParts = true,
                        OnClick = () =>
                        {
                            if(previous.Length == 0)
                                return;

                            using var disposable = Application.ActiveModel.SelectionManager.TemporarySelectObjects(previous);

                            // Do something with selected items, as example remove selected. 
                            Application.ActiveModel.UnsafeObject.DeleteSelection(true);

                            return; //Then it will be rollback to previous selected list 
                        }
                    }

                ]);
        }

        public override void DisconnectedFromSolidWorks()
        {

        }
        #endregion
    }
}

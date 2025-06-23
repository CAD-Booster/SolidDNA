using SolidWorks.Interop.sldworks;
using System.Runtime.InteropServices;

namespace CADBooster.SolidDna
{
    /// <summary>
    /// Represents a created command context icon in the SolidWorks
    /// </summary>
    internal class CommandContextIconCreated : CommandContextCreatedBase
    {
        /// <summary>
        /// Gets the name of this command context item
        /// </summary>
        public sealed override string Name => Hint;

        /// <summary>
        /// Initializes a new command context icon in the SolidWorks UI
        /// </summary>
        /// <param name="commandContextIcon">The icon configuration</param>
        /// <param name="documentType">The document type this icon applies to</param>
        public CommandContextIconCreated(CommandContextIcon commandContextIcon,
                                         DocumentType documentType) : base(commandContextIcon, documentType)
        {
            // The list of icons. There should be a one multi sized icon.
            var icons = Icons.GetArrayFromDictionary(Icons.GetFormattedPathDictionary(commandContextIcon.Icon));

            // Get the SolidWorks frame and add the menu icon
            var frame = (IFrame)AddInIntegration.SolidWorks.UnsafeObject.Frame();

            _ = frame.AddMenuPopupIcon3(
                (int)DocumentType,
                (int)SelectionType,
                Hint,
                SolidWorksEnvironment.Application.SolidWorksCookie,
                $"{nameof(SolidAddIn.Callback)}({CallbackId})",
                $"{nameof(SolidAddIn.ItemStateCheck)}({CallbackId})",
                string.Empty,
                icons);

            _ = Marshal.ReleaseComObject(frame);
        }
    }
}

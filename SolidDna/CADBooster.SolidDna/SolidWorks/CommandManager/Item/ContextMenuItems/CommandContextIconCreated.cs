using SolidWorks.Interop.sldworks;
using System.Runtime.InteropServices;

namespace CADBooster.SolidDna;

/// <summary>
/// Represents a created command context icon in the SolidWorks
/// </summary>
internal class CommandContextIconCreated : CommandContextCreatedBase
{
    /// <summary>
    /// Text displayed in the SolidWorks status bar and as a tooltip when the user moves the pointer over the icon
    /// </summary>
    public string Hint { get; }

    /// <summary>
    /// The name for identification of this created context icon. This is the Hint text displayed as a tooltip on mouse hover.
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
        Hint = commandContextIcon.Hint;

        // The list of icons. There should be a one multi sized icon.
        var icons = Icons.GetArrayFromDictionary(Icons.GetFormattedPathDictionary(commandContextIcon.IconPathFormat));

        // Get the SolidWorks frame and add the menu icon
        using var frame = new SolidDnaObject<IFrame>((IFrame)AddInIntegration.SolidWorks.UnsafeObject.Frame());

        // AddMenuPopupIcon3 documentation:
        // https://help.solidworks.com/2025/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.ISldWorks~AddItemToThirdPartyPopupMenu2.html

        // Adds an icon to a context-sensitive menu of a SOLIDWORKS UI.
        // Returns: True if the context-sensitive menu icon is added, false if not
        var iconAdded = frame.UnsafeObject.AddMenuPopupIcon3(
            // Document type whose context-sensitive menus display the icon
            (int)DocumentType,
            // Selection type whose context-sensitive menus display the icon
            SelectionType,
            // Text displayed in the SOLIDWORKS status bar and as a tooltip when the user moves the pointer over the icon
            Hint,
            // ID of the add-in; value of the Cookie argument passed by ISwAddin::ConnectToSW
            SolidWorksEnvironment.Application.SolidWorksCookie,
            // Function called when user clicks the context-sensitive menu icon.
            // See Add-in Callback and Enable Methods to learn how to specify CallbackFunction and CallbackUpdateFunction.
            // When the icon is clicked, the function specified in CallbackFunction can perform actions such as displaying a third-party pop-up menu*.
            // *Third-party pop-up: https://help.solidworks.com/2025/english/api/sldworksapi/SOLIDWORKS.Interop.sldworks~SOLIDWORKS.Interop.sldworks.ISldWorks~ShowThirdPartyPopupMenu.html
            $"{nameof(SolidAddIn.Callback)}({CallbackId})",
            // Optional function that controls the state of the icon; if specified, then SOLIDWORKS calls this function before displaying the icon.
            // If CallbackUpdateFunction returns: 
            // 0 - (CommandManagerItemState.DeselectedDisabled): Deselects and disables the item.
            // 1 - (CommandManagerItemState.DeselectedEnabled):  Deselects and enables the item; this is the default state if no update function is specified.
            // 4 - (CommandManagerItemState.Hidden):             Hides the item.
            // Other values (CommandManagerItemState.SelectedDisabled and CommandManagerItemState.SelectedEnabled) are not supported.
            $"{nameof(SolidAddIn.ItemStateCheck)}({CallbackId})",
            // Names of custom feature types.
            // CustomNames is a semi-colon separated list of the names of the custom feature types.
            // This argument is applicable only if SelectType is a custom feature type (like swSelATTRIBUTES);
            // in the case of swSelATTRIBUTES, set this field to the name of the attribute definition
            SelectionType.GetCustomFeatureNames(),
            // Array of strings for the paths for the image files for the context-sensitive menu icon.
            // ImageList images can be: 20 x 20 pixels, 32 x 32 pixels, 40 x 40 pixels, 64 x 64 pixels, 96 x 96 pixels, 128 x128 pixels.
            // Images should use 256-color palette
            icons);

        // If the icon was not added, throw an exception.
        if (!iconAdded)
            throw new SolidDnaException(SolidDnaErrors.CreateError(SolidDnaErrorTypeCode.SolidWorksCommandManager,
                SolidDnaErrorCode.SolidWorksCommandCreateContextIconError));


        Logger.LogDebugSource($"Context menu icon created Name: {Name} CallbackId: {CallbackId})");
    }
}

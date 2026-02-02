namespace CADBooster.SolidDna;

/// <summary>
/// A command context menu item
/// </summary>
internal class CommandContextItemCreated : CommandContextCreatedBase
{
    /// <summary>
    /// Text displayed in the SolidWorks status bar when the user moves the pointer over the item
    /// </summary>
    public string Hint { get; }

    /// <summary>
    /// The name of this command that is displayed in the context menu
    /// </summary>
    public sealed override string Name => _name;

    /// <summary>
    /// Gets the command ID assigned by SolidWorks for this item
    /// </summary>
    public int CommandId { get; }

    private readonly string _name;

    /// <summary>
    /// Creates the command context item for the specified document types
    /// </summary>
    /// <param name="commandContextItem">The command context item configuration</param>
    /// <param name="solidWorksCookie">The SolidWorks add-in cookie</param>
    /// <param name="fullName">The full name with hierarchical path</param>
    /// <param name="documentType">The document type</param>
    /// <exception cref="SolidDnaException">Thrown if the item has already been created</exception>
    public CommandContextItemCreated(CommandContextItem commandContextItem,
                                     int solidWorksCookie,
                                     string fullName,
                                     DocumentType documentType) : base(commandContextItem, documentType)
    {
        if (solidWorksCookie <= 0)
            throw new SolidDnaException(
                SolidDnaErrors.CreateError(SolidDnaErrorTypeCode.SolidWorksCommandManager,
                    SolidDnaErrorCode.SolidWorksCommandManagerError,
                    $"Invalid SolidWorks cookie value: {solidWorksCookie}. Cookie must be positive"));

        if (string.IsNullOrEmpty(fullName))
            throw new SolidDnaException(
                SolidDnaErrors.CreateError(SolidDnaErrorTypeCode.SolidWorksCommandManager,
                    SolidDnaErrorCode.SolidWorksCommandManagerError,
                    "Context menu item name cannot be null or empty"));

        Hint = commandContextItem.Hint;

        _name = commandContextItem.Name;

        /// We have <see cref="ICommandManager.AddContextMenu"/>, but it adds a group, and there's no possibility to add a root item.
        /// But it can still be implemented as an alternative. Just specific handling for this CommandContextItem type should be added.

        if (SelectionType.IsSpecificFeatureType)
        {
            // Adds a menu item to context menus for specific feature types (e.g., "Boss", "SketchHole", "ChamferFeature").
            // This method is similar to AddMenuPopupItem3 but uses feature type names from IFeature.GetTypeName2() 
            // instead of general selection type enums, enabling fine-grained control over which features display the menu item.
            // For available feature type names, see <see cref="SpecificFeatureSelectionType"/>.
            // Returns: SOLIDWORKS runtime command ID or -1 if the method fails.
            // Docs: https://help.solidworks.com/2025/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.ISldWorks~AddMenuPopupItem4.html
            CommandId = AddInIntegration.SolidWorks.UnsafeObject.AddMenuPopupItem4(
                // Document type as defined by swDocumentTypes_e (same as AddMenuPopupItem3)
                (int) DocumentType,
                // ID of the add-in; value of the Cookie argument passed by ISwAddin.ConnectToSW (same as AddMenuPopupItem3)
                solidWorksCookie,
                // Feature type name from IFeature.GetTypeName2() (e.g., "Boss", "SketchHole", "ChamferFeature")
                // This differs from AddMenuPopupItem3 which uses general selection type enums (e.g., swSelFACES, swSelEDGES)
                SelectionType,
                // Description displayed on the shortcut menu (same as AddMenuPopupItem3)
                fullName,
                // Function called when user clicks the context-sensitive menu item (same as AddMenuPopupItem3)
                $"{nameof(SolidAddIn.Callback)}({CallbackId})",
                // Optional function that controls the state of the menu item (same as AddMenuPopupItem3)
                // See AddMenuPopupItem3 call below for detailed state return value documentation
                $"{nameof(SolidAddIn.ItemStateCheck)}({CallbackId})",
                // Text displayed in the SOLIDWORKS status bar when the user moves the pointer over the menu item (same as AddMenuPopupItem3)
                Hint,
                // Semi-colon separated list of custom feature type names (same as AddMenuPopupItem3)
                // Applicable only if SelectType is a custom feature type (e.g., swSelATTRIBUTES)
                SelectionType.GetCustomFeatureNames()
            );
        }
        else
        {
            // Adds a menu item and zero or more submenus to shortcut menus of entities of the specified type in documents of the specified type.
            // This method should be called for each unique combination of DocumentType and SelectType in whose menus you want this menu item to display.
            // Returns: SOLIDWORKS runtime command ID or -1 if the method fails
            // Docs: https://help.solidworks.com/2025/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.ISldWorks~AddMenuPopupItem3.html
            CommandId = AddInIntegration.SolidWorks.UnsafeObject.AddMenuPopupItem3(
                // Document type as defined by swDocumentTypes_e
                (int) DocumentType,
                // ID of the add-in; value of the Cookie argument passed by <see cref="ISwAddin.ConnectToSW"/>
                solidWorksCookie,
                // Selection type whose context-sensitive menus display the item
                SelectionType,
                // Description displayed on the shortcut menu.
                // This parameter should use the at-sign (@) to create submenus.
                //
                // Actually this does not work on SW 22 and 24; it seems like an API bug: 
                //     To add a separator bar after a menu item, append an at-sign to PopupItemName and enclose PopupItemName in double quotes ("").
                //     For example: "Edge" adds menu item Edge, "Edge@Color" adds Edge with submenu Color, "Edge@" adds Edge with separator bar after it
                //     It looks like SW still splits the string in ("") with '@' and uses the first part as menu item name and the second (empty) part as submenu name.
                fullName,
                // Function called when user clicks the context-sensitive menu item.
                $"{nameof(SolidAddIn.Callback)}({CallbackId})",
                // Optional function that controls the state of the menu item.
                // If specified, SOLIDWORKS calls this function before displaying the menu.
                // Display of the menu item is controlled by the return value of MenuEnableMethod.
                // If MenuEnableMethod returns:
                // 0 - (CommandManagerItemState.DeselectedDisabled): Deselects and disables the menu item.
                // 1 - (CommandManagerItemState.DeselectedEnabled):  Deselects and enables the menu item (this is the default menu state if no update function is specified).
                // 2 - (CommandManagerItemState.SelectedDisabled):   Selects and disables the menu item.
                // 3 - (CommandManagerItemState.SelectedEnabled):    Selects and enables the menu item.
                // 4 - (CommandManagerItemState.Hidden):             Hides the menu item.
                $"{nameof(SolidAddIn.ItemStateCheck)}({CallbackId})",
                // Text displayed in the SOLIDWORKS status bar when the user moves the pointer over the menu item.
                // If you specify a HintString, it must be preceded by a comma
                Hint,
                // Semi-colon separated list of the names of the custom feature types.
                // This argument is applicable only if SelectType is a custom feature type (like swSelATTRIBUTES);
                // in the case of swSelATTRIBUTES, set this field to the name of the attribute definition
                SelectionType.GetCustomFeatureNames()
            );
        }

        // If the returned position is -1, the item was not added.
        if (CommandId == -1)
            throw new SolidDnaException(SolidDnaErrors.CreateError(SolidDnaErrorTypeCode.SolidWorksCommandManager,
                SolidDnaErrorCode.SolidWorksCommandCreateContextItemError));

        Logger.LogDebugSource($"Context menu item created Name: {Name} CallbackId: {CallbackId})");
    }

    /// <summary>
    /// Disposing
    /// </summary>
    public override void Dispose()
    {
        base.Dispose();

        /// There is no way to remove the item

        /// It always returns false, and the item isn't removed.
        /// Docs: https://help.solidworks.com/2025/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.ISldWorks~RemoveMenuPopupItem2.html
        //var removeMenuPopupItemResult = AddInIntegration.SolidWorks.UnsafeObject.RemoveMenuPopupItem2(
        //    (int)DocumentType,
        //    SolidWorksEnvironment.Application.SolidWorksCookie,
        //    (int)SelectionType,
        //    FullName,
        //    $"{nameof(SolidAddIn.Callback)}({CallbackId})",
        //    $"{nameof(SolidAddIn.ItemStateCheck)}({CallbackId})",
        //    Hint,
        //    string.Empty
        //);

        /// It always returns true, but the item isn't removed.
        /// Docs: https://help.solidworks.com/2025/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.ISldWorks~RemoveFromPopupMenu.html
        //var removeFromPopupMenuResult = AddInIntegration.SolidWorks.UnsafeObject.RemoveFromPopupMenu(
        //    CommandId,
        //    (int)DocumentType,
        //    (int)SelectionType,
        //    true
        //);

        /// This one isn't possible since AddMenuPopupItem2 callback is only for C++
        /// Docs: https://help.solidworks.com/2025/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IFrame~AddMenuPopupItem2.html
        /// Frame docs: https://help.solidworks.com/2025/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.ISldWorks~Frame.html
        /// RemoveMenu docs: https://help.solidworks.com/2025/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.ISldWorks~RemoveMenu.html
        //var frame = (IFrame) AddInIntegration.SolidWorks.UnsafeObject.Frame();
        //frame.AddMenuPopupItem2(..., fullName, ...);
        //frame.RemoveMenu(fullName);

        /// Besides, the user can hide it using <see cref="ICommandItem.OnStateCheck"/>.

        /// Tested on SW 2024
        /// It can be fixed in future SolidWorks updates, so comments are added for this reason
    }
}

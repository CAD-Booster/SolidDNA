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
    /// <param name="path">The path to use for hierarchical naming. If empty, the item's name is used</param>
    /// <returns>A list of created command context items</returns>
    /// <exception cref="SolidDnaException">Thrown if the item has already been created</exception>
    public CommandContextItemCreated(CommandContextItem commandContextItem,
                                     string fullName,
                                     DocumentType documentType) : base(commandContextItem, documentType)
    {
        Hint = commandContextItem.Hint;

        _name = commandContextItem.Name;

        /// We have <see cref="ICommandManager.AddContextMenu"/>, but it adds a group, and there's no possibility to add a root item.
        /// But it can still be implemented as an alternative. Just specific handling for this CommandContextItem type should be added.

        /// Also we have AddMenuPopupItem4 (AddMenuPopupItem3 still not marked obsolete).
        /// Documentation says that it is related to features selection and SelectionType is string.
        /// AddMenuPopupItem3 looks more understandable so this one is preferred.
        /// https://help.solidworks.com/2025/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.ISldWorks~AddMenuPopupItem4.html


        // AddMenuPopupItem3 documentation:
        // https://help.solidworks.com/2023/English/api/sldworksapi/SOLIDWORKS.Interop.sldworks~SOLIDWORKS.Interop.sldworks.ISldWorks~AddMenuPopupItem3.html

        // Adds a menu item and zero or more submenus to shortcut menus of entities of the specified type in documents of the specified type.
        // This method should be called for each unique combination of DocumentType and SelectType in whose menus you want this menu item to display.
        // Returns: SOLIDWORKS runtime command ID or -1 if the method fails
        CommandId = AddInIntegration.SolidWorks.UnsafeObject.AddMenuPopupItem3(
            // Document type as defined by swDocumentTypes_e
            (int)DocumentType,
            // ID of the add-in; value of the Cookie argument passed by <see cref="ISwAddin.ConnectToSW"/>
            SolidWorksEnvironment.Application.SolidWorksCookie,
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
        //var removeFromPopupMenuResult = AddInIntegration.SolidWorks.UnsafeObject.RemoveFromPopupMenu(
        //    CommandId,
        //    (int)DocumentType,
        //    (int)SelectionType,
        //    true
        //);

        /// This one isn't possible since AddMenuPopupItem2 callback is only for C++
        //var frame = (IFrame) AddInIntegration.SolidWorks.UnsafeObject.Frame();
        //frame.AddMenuPopupItem2(..., fullName, ...);
        //frame.RemoveMenu(fullName);

        /// Besides, the user can hide it using <see cref="ICommandItem.OnStateCheck"/>.

        /// Tested on SW 2024
        /// It can be fixed in future SolidWorks updates, so comments are added for this reason
    }
}

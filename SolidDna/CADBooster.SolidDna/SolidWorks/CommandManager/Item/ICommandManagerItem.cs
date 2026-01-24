namespace CADBooster.SolidDna;

public interface ICommandManagerItem : ICommandItem
{
    /// <summary>
    /// True if the command should be added to the tab
    /// </summary>
    bool AddToTab { get; set; }

    /// <summary>
    /// The command ID for this flyout item
    /// </summary>
    int CommandId { get; }

    /// <summary>
    /// The position of the item in the list. Specify 0 to add the item to the beginning of the toolbar or menu, or specify -1 to add it to the end.
    /// After creating the item, we set this to the actual position (flyouts and separators are not included in the count) and we use the actual position to get the <see cref="CommandId"/>.
    /// </summary>
    int Position { get; set; }

    /// <summary>
    /// The tab view style (whether and how to show in the large icon tab bar view)
    /// </summary>
    CommandManagerItemTabView TabView { get; set; }

    /// <summary>
    /// True to show this item in the command tab when a part is open
    /// </summary>
    bool VisibleForParts { get; set; }

    /// <summary>
    /// True to show this item in the command tab when an assembly is open
    /// </summary>
    bool VisibleForAssemblies { get; set; }

    /// <summary>
    /// True to show this item in the command tab when a drawing is open
    /// </summary>
    bool VisibleForDrawings { get; set; }
}
namespace CADBooster.SolidDna;

/// <summary>
/// States for command manager items, flyout items and flyout groups.
/// 0, 2 and 4 do not seem to work correctly for flyout items:
/// <see cref="DeselectedDisabled"/> and <see cref="SelectedDisabled"/> hide the item instead of disabling,
/// <see cref="Hidden"/>: shows the item.
/// </summary>
public enum CommandManagerItemState
{
    /// <summary>
    /// Deselect and disable the item. 
    /// Valid for items, flyout items and flyout groups.
    /// </summary>
    DeselectedDisabled = 0,

    /// <summary>
    /// Deselect and enable the item. This is the default state if no update function is specified.
    /// Valid for items, flyout items and flyout groups.
    /// </summary>
    DeselectedEnabled = 1,

    /// <summary>
    /// Select and disable the item. 
    /// The default behavior of SolidWorks is to select a (flyout) item if the property manager page for that item is active, like while creating a sketch or drawing a rectangle.
    /// Also used in the Tools menu to show a selected option with a check mark.
    /// Valid for items and flyout items.
    /// </summary>
    SelectedDisabled = 2,

    /// <summary>
    /// Select and enable the item. 
    /// The default behavior of SolidWorks is to select a (flyout) item if the property manager page for that item is active, like while creating a sketch or drawing a rectangle.
    /// Also used in the Tools menu to show a selected option with a check mark.
    /// Valid for items and flyout items.
    /// </summary>
    SelectedEnabled = 3,

    /// <summary>
    /// Hide the item. Valid for flyout items.
    /// </summary>
    Hidden = 4,
}
using System.Windows.Controls.Primitives;

namespace CADBooster.SolidDna
{
    /// <summary>
    /// Types of state command or flyout items 
    /// </summary>
    public enum ItemState
    {
        /// <summary>
        /// Deselects and disables the item
        /// </summary>
        DeselectedDisabled = 0,

        /// <summary>
        /// Deselects and enables the item. This is the default state if no update function is specified
        /// </summary>
        DeselectedEnabled = 1,

        /// <summary>
        /// Selects and disables the item
        /// </summary>
        SelectedDisabled = 2,

        /// <summary>
        /// Selects and enables the item
        /// </summary>
        SelectedEnabled = 3,

        /// <summary>
        /// Hides the item, not supported for command item
        /// </summary>
        Hidden = 4
    }
}
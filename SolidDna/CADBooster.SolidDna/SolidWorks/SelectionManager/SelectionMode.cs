namespace CADBooster.SolidDna
{
    /// <summary>
    /// Specifies how a selection operation should interact with the current selection.
    /// </summary>
    public enum SelectionMode
    {
        /// <summary>
        /// Replaces the current selection with the new selection.
        /// Any previously selected items will be deselected.
        /// </summary>
        Override,

        /// <summary>
        /// Adds to the current selection while keeping existing selected items.
        /// The new selection will be combined with the current selection.
        /// </summary>
        Append
    }
}
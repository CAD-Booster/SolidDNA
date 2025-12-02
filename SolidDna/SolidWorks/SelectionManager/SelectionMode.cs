namespace CADBooster.SolidDna
{
    /// <summary>
    /// Specifies whether to start a new selection or append the object to the existing selection.
    /// </summary>
    public enum SelectionMode
    {
        /// <summary>
        /// Replaces the current selection with the new selection. Any previously selected items will be deselected.
        /// Same as append = false.
        /// </summary>
        Create,

        /// <summary>
        /// Adds to the current selection while keeping existing selected items. The new selection will be combined with the current selection.
        /// Same as append = true.
        /// </summary>
        Append
    }
}

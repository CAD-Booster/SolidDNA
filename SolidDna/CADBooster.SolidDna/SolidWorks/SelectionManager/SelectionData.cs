namespace CADBooster.SolidDna
{
    /// <summary>
    /// Represents data required for object selection in SolidWorks, including selection point,
    /// selection mark, and selection mode.
    /// </summary>
    public struct SelectionData
    {
        /// <summary>
        /// Gets or sets the 3D point where the selection occurs.
        /// This is typically used for precise selection of entities in the graphics area.
        /// </summary>
        public Point3D Point { get; set; }

        /// <summary>
        /// Gets or sets the selection mark identifier.
        /// A value of -1 indicates no specific mark (default selection).
        /// Marks can be used to identify specific selections or groups of selections.
        /// </summary>
        public int Mark { get; set; }

        /// <summary>
        /// Gets or sets how the selection should interact with existing selections.
        /// </summary>
        /// <remarks>
        /// <para><b>Possible values:</b></para>
        /// <list type="bullet">
        /// <item>
        /// <description><see cref="SelectionMode.Override"/>: Clears current selection before selecting new items</description>
        /// </item>
        /// <item>
        /// <description><see cref="SelectionMode.Append"/>: Adds to current selection while keeping existing items selected</description>
        /// </item>
        /// </list>
        /// <para><b>Default:</b> <see cref="SelectionMode.Override"/> (when using <see cref="SelectionData.Default"/>)</para>
        /// </remarks>
        public SelectionMode Mode { get; set; }

        /// <summary>
        /// Provides default selection data with the following values:
        /// </summary>
        /// <remarks>
        /// <list type="bullet">
        ///   <item><description><b>Point</b>: <see cref="Point3D.Zero"/> (0, 0, 0)</description></item>
        ///   <item><description><b>Mark</b>: -1 (no specific selection mark)</description></item>
        ///   <item><description><b>Mode</b>: <see cref="SelectionMode.Override"/> (replaces current selection)</description></item>
        /// </list>
        /// <para>This serves as a convenient starting point for selection operations.</para>
        /// </remarks>
        public readonly static SelectionData Default = new SelectionData(Point3D.Zero, -1, SelectionMode.Override);

        /// <summary>
        /// Initializes a new instance of the SelectionData struct with specific values.
        /// </summary>
        /// <param name="point3D">The 3D point where selection occurs</param>
        /// <param name="mark">The selection mark identifier (-1 for default)</param>
        /// <param name="mode">The selection mode</param>
        public SelectionData(Point3D point3D, int mark, SelectionMode mode)
        {
            Point = point3D;
            Mark = mark;
            Mode = mode;
        }
    }
}


using SolidWorks.Interop.sldworks;

namespace CADBooster.SolidDna
{
    /// <summary>
    /// A sheet of a drawing
    /// </summary>
    public class DrawingSheet : SolidDnaObject<Sheet>, IDrawingSheet
    {
        #region Private Members

        /// <summary>
        /// The parent drawing document of this sheet
        /// </summary>
        private readonly DrawingDocument mDrawingDoc;

        #endregion

        #region Public Properties

        /// <summary>
        /// The sheet name
        /// </summary>
        public string SheetName => BaseObject.GetName();

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="comObject">The underlying COM object</param>
        /// <param name="drawing">The parent drawing document</param>
        public DrawingSheet(Sheet comObject, DrawingDocument drawing) : base(comObject)
        {
            mDrawingDoc = drawing;
        }

        #endregion

        /// <summary>
        /// Activate this sheet.
        /// </summary>
        /// <returns>True if successful</returns>
        public bool Activate() => mDrawingDoc.ActivateSheet(SheetName);
    }
}

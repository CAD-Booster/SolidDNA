using SolidWorks.Interop.sldworks;
using System;

namespace CADBooster.SolidDna
{
    /// <summary>
    /// Interface for a SolidWorks Note object.
    /// Enables mocking for unit testing code that consumes notes.
    /// </summary>
    public interface ISolidDnaNote : IDisposable
    {
        #region Properties

        /// <summary>
        /// Get or set the note name.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Get or set the note text content.
        /// </summary>
        string Text { get; set; }

        /// <summary>
        /// The raw underlying COM object.
        /// WARNING: Use with caution. You must handle all disposal from this point on.
        /// </summary>
        INote UnsafeObject { get; }

        #endregion

        #region Balloon Methods

        /// <summary>
        /// Get the upper text of the selected BOM Balloon note.
        /// </summary>
        /// <returns>The upper text</returns>
        string GetBOMBalloonUpperText();

        /// <summary>
        /// Get the lower text of the selected BOM Balloon note.
        /// </summary>
        /// <returns>The lower text</returns>
        string GetBOMBalloonLowerText();

        /// <summary>
        /// Set the text for the selected BOM Balloon note.
        /// </summary>
        /// <param name="upperTextStyle">The upper text style</param>
        /// <param name="upperText">The upper text</param>
        /// <param name="lowerTextStyle">The lower text style</param>
        /// <param name="lowerText">The lower text</param>
        void SetBOMBalloonText(NoteTextContent upperTextStyle, string upperText, NoteTextContent lowerTextStyle, string lowerText);

        #endregion
    }
}

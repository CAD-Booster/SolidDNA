using SolidWorks.Interop.sldworks;

namespace CADBooster.SolidDna;

/// <summary>
/// A SolidWorks Note object
/// </summary>
public class Note : SolidDnaObject<INote>, ISolidDnaNote
{
    #region Constructor

    /// <summary>
    /// Default constructor
    /// </summary>
    public Note(INote note) : base(note)
    {

    }

    #endregion

    #region Public properties

    /// <summary>
    /// Get or set the note name.
    /// </summary>
    public string Name
    {
        get => BaseObject.GetName();
        set => BaseObject.SetName(value);
    }

    /// <summary>
    /// Get or set the note text content.
    /// </summary>
    public string Text
    {
        get => BaseObject.GetText();
        set => BaseObject.SetText(value);
    }

    #endregion

    #region Balloon Methods

    /// <summary>
    /// Get the upper text of the selected BOM Balloon note.
    /// </summary>
    /// <returns>The upper text</returns>
    public string GetBOMBalloonUpperText() => BaseObject.GetBomBalloonText(true);

    /// <summary>
    /// Get the lower text of the selected BOM Balloon note.
    /// </summary>
    /// <returns>The lower text</returns>
    public string GetBOMBalloonLowerText() => BaseObject.GetBomBalloonText(false);

    /// <summary>
    /// Set the text for the selected BOM Balloon note.
    /// </summary>
    /// <param name="upperTextStyle">The upper text style</param>
    /// <param name="upperText">The upper text</param>
    /// <param name="lowerTextStyle">The lower text style</param>
    /// <param name="lowerText">The lower text</param>
    /// <remarks>
    ///     If the upper or lower text style is <see cref="NoteTextContent.TextQuantity"/>
    ///     or <see cref="NoteTextContent.ItemNumber"/>, then SOLIDWORKS ignores the 
    ///     specified upper or lower text.
    /// </remarks>
    public void SetBOMBalloonText(NoteTextContent upperTextStyle, string upperText, NoteTextContent lowerTextStyle, string lowerText)
        => BaseObject.SetBomBalloonText((int)upperTextStyle, upperText, (int)lowerTextStyle, lowerText);

    #endregion
}
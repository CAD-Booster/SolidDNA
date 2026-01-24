using System.Collections.Generic;

namespace CADBooster.SolidDna;

/// <summary>
/// Represents a context icon in SolidWorks
/// </summary>
public class CommandContextIcon : CommandContextBase, ICommandCreatable
{
    /// <summary>
    /// Absolute path to the image files that contain the single icon.
    /// Based on a string format, replacing {0} with the size. For example C:\Folder\Icon{0}.png
    /// If batch icon files are provided, SolidWorks uses the first icon (no index support).
    /// </summary>
    public string IconPathFormat { get; set; }

    /// <summary>
    /// The text that displays on mouse hover. In other words, it is the tooltip for the icon button
    /// </summary>
    string ICommandCreatable.Name => Hint;

    /// <summary>
    /// Creates the command context icon for the specified document types
    /// </summary>
    /// <param name="path">Not used for icon</param>
    /// <returns>A list of created command context icons</returns>
    /// <exception cref="SolidDnaException">Thrown if the item has already been created</exception>
    public sealed override IEnumerable<ICommandCreated> Create(string path = "")
    {
        _ = base.Create();

        var created = new List<CommandContextIconCreated>();

        if (VisibleForAssemblies)
            created.Add(new CommandContextIconCreated(this, DocumentType.Assembly));
        if (VisibleForDrawings)
            created.Add(new CommandContextIconCreated(this, DocumentType.Drawing));
        if (VisibleForParts)
            created.Add(new CommandContextIconCreated(this, DocumentType.Part));

        return created;
    }
}

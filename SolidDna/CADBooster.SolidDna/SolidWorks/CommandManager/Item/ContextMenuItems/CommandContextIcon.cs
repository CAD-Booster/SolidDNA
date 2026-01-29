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
    /// Text displayed in the SolidWorks status bar and as a tooltip when the user moves the pointer over the icon
    /// </summary>
    public string Hint { get; set; }

    /// <summary>
    /// The name to identify the command. For icons, Hint is used as the name since it's the only text that the icon has.
    /// </summary>
    string ICommandCreatable.Name => Hint;

    /// <summary>
    /// Creates the command context icon
    /// </summary>
    /// <param name="info">Create information containing cookie</param>
    /// <returns>A list of created command context icons</returns>
    /// <exception cref="SolidDnaException">Thrown if the item has already been created</exception>
    public sealed override IEnumerable<ICommandCreated> Create(ICommandContextCreateInfo info)
    {
        // Call base class method to ensure that object isnt created and move object to created state if not
        _ = base.Create(info);

        List<CommandContextIconCreated> created = [];

        // Create commands for each SW document type
        if (VisibleForAssemblies)
            created.Add(new CommandContextIconCreated(this, info.SolidWorksCookie, DocumentType.Assembly));
        if (VisibleForDrawings)
            created.Add(new CommandContextIconCreated(this, info.SolidWorksCookie, DocumentType.Drawing));
        if (VisibleForParts)
            created.Add(new CommandContextIconCreated(this, info.SolidWorksCookie, DocumentType.Part));

        return created;
    }
}

using System;
using System.Collections.Generic;

namespace CADBooster.SolidDna;

/// <summary>
/// Represents a context icon in SolidWorks.
/// <para>
/// It shows in the SolidWorks context popup. 
/// If no popup for <see cref="SelectionType"/> shows, this means the icon will not add a new popup and will be inaccessible, so make sure that a popup shows for the selection.
/// </para>
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
    /// The action to call when the item state requested
    /// SolidWorks calls it each time when a context popup in the specified selection context shows
    /// Try to avoid long operations on this callback 
    /// </summary>
    public override Action<CommandManagerItemStateCheckArgs> OnStateCheck { get; set; }

    /// <summary>
    /// Creates the command context icon
    /// </summary>
    /// <param name="info">Create information containing cookie</param>
    /// <returns>A list of created command context icons</returns>
    /// <exception cref="SolidDnaException">Thrown if the item has already been created</exception>
    public sealed override IEnumerable<ICommandCreated> Create(ICommandContextCreateInfo info)
    {
        if (info is null)
            throw new SolidDnaException(
                SolidDnaErrors.CreateError(SolidDnaErrorTypeCode.SolidWorksCommandManager,
                    SolidDnaErrorCode.SolidWorksCommandManagerError,
                    "Context menu create info cannot be null"));

        if (string.IsNullOrWhiteSpace(Hint))
            throw new SolidDnaException(
                SolidDnaErrors.CreateError(SolidDnaErrorTypeCode.SolidWorksCommandManager,
                    SolidDnaErrorCode.SolidWorksCommandManagerError,
                    "Context menu icon hint cannot be null or empty"));

        if (string.IsNullOrWhiteSpace(IconPathFormat))
            throw new SolidDnaException(
                SolidDnaErrors.CreateError(SolidDnaErrorTypeCode.SolidWorksCommandManager,
                    SolidDnaErrorCode.SolidWorksCommandManagerError,
                    "Context menu icon path format cannot be null or empty"));

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

using System;
using System.Collections.Generic;

namespace CADBooster.SolidDna;

/// <summary>
/// A command context menu item
/// </summary>
public class CommandContextItem : CommandContextBase, ICommandCreatable
{
    /// <summary>
    /// Text displayed in the SolidWorks status bar when the user moves the pointer over the item
    /// </summary>
    public string Hint { get; set; }

    /// <summary>
    /// The name of this command that is displayed in the context menu
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// The action to call when the item state requested
    /// SolidWorks calls it each time when a context menu in the specified selection context shows, and some duplicated calls are performed after this
    /// Try to avoid long operations on this callback 
    /// </summary>
    public override Action<CommandManagerItemStateCheckArgs> OnStateCheck { get; set; }

    /// <summary>
    /// Creates the command context item for the specified document types
    /// </summary>
    /// <param name="info">Create information. Should be <see cref="CommandContextItemCreateInfo"/> to create nested items with proper path hierarchy</param>
    /// <returns>A list of created command context items</returns>
    /// <exception cref="SolidDnaException">Thrown if the item has already been created</exception>
    public sealed override IEnumerable<ICommandCreated> Create(ICommandContextCreateInfo info)
    {
        if (info is null)
            throw new SolidDnaException(
                SolidDnaErrors.CreateError(SolidDnaErrorTypeCode.SolidWorksCommandManager,
                    SolidDnaErrorCode.SolidWorksCommandManagerError,
                    "Context menu create info cannot be null"));

        if (string.IsNullOrWhiteSpace(Name))
            throw new SolidDnaException(
                SolidDnaErrors.CreateError(SolidDnaErrorTypeCode.SolidWorksCommandManager,
                    SolidDnaErrorCode.SolidWorksCommandManagerError,
                    "Context menu item name cannot be null or empty"));

        _ = base.Create(info);

        // Ensure we have CommandContextItemCreateInfo with path, create one if needed
        var itemInfo = info as CommandContextItemCreateInfo 
            ?? new CommandContextItemCreateInfo(info.SolidWorksCookie, string.Empty);

        var fullName = string.IsNullOrEmpty(itemInfo.Path) ? $"{Name}" : $"{itemInfo.Path}@{Name}";

        List<CommandContextItemCreated> created = [];

        if (VisibleForAssemblies)
            created.Add(new CommandContextItemCreated(this, itemInfo.SolidWorksCookie, fullName, DocumentType.Assembly));
        if (VisibleForDrawings)
            created.Add(new CommandContextItemCreated(this, itemInfo.SolidWorksCookie, fullName, DocumentType.Drawing));
        if (VisibleForParts)
            created.Add(new CommandContextItemCreated(this, itemInfo.SolidWorksCookie, fullName, DocumentType.Part));

        return created;
    }
}

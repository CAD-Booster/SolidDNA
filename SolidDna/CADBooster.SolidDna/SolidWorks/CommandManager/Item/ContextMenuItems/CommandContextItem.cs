using System.Collections.Generic;

namespace CADBooster.SolidDna;

/// <summary>
/// A command context menu item
/// </summary>
public class CommandContextItem : CommandContextBase, ICommandCreatable
{
    /// <summary>
    /// The name of this command
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Creates the command context item for the specified document types
    /// </summary>
    /// <param name="path">The path to use for hierarchical naming. If empty, the item's name is used</param>
    /// <returns>A list of created command context items</returns>
    /// <exception cref="SolidDnaException">Thrown if the item has already been created</exception>
    public sealed override IEnumerable<ICommandCreated> Create(string path = "")
    {
        _ = base.Create(path);

        var fullName = string.IsNullOrEmpty(path) ? $"{Name}" : $"{path}@{Name}";

        var created = new List<CommandContextItemCreated>();

        if (VisibleForAssemblies)
            created.Add(new CommandContextItemCreated(this, fullName, DocumentType.Assembly));
        if (VisibleForDrawings)
            created.Add(new CommandContextItemCreated(this, fullName, DocumentType.Drawing));
        if (VisibleForParts)
            created.Add(new CommandContextItemCreated(this, fullName, DocumentType.Part));

        return created;
    }
}

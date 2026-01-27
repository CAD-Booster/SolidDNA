using System.Collections.Generic;
using System.Linq;

namespace CADBooster.SolidDna;

/// <summary>
/// Represents a created command context group in SolidWorks
/// This class handles the creation and disposal of a group of context menu items
/// </summary>
internal class CommandContextGroupCreated : ICommandCreated
{
    /// <summary>
    /// The name of this command context group that is displayed in the context menu
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// A list of created command items within this group
    /// </summary>
    private readonly List<ICommandCreated> _createdItems;

    /// <summary>
    /// Initializes a new instance of the <see cref="CommandContextGroupCreated"/> class
    /// </summary>
    /// <param name="name">The name of the group</param>
    /// <param name="path">The hierarchical path for the group</param>
    /// <param name="items">The list of command items to include in the group</param>
    public CommandContextGroupCreated(string name, string path, IEnumerable<ICommandCreatable> items)
    {
        Name = name;

        // Construct the full name for the group using the provided path
        var fullName = string.IsNullOrEmpty(path) ? $"{Name}" : $"{path}@{Name}";

        // Create all child items and store them in the list
        _createdItems = items
            .SelectMany(x => x.Create(fullName))
            .ToList();
    }

    /// <summary>
    /// Disposing
    /// </summary>
    public void Dispose() => _createdItems.ForEach(x => x.Dispose());
}

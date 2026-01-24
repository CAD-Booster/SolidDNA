using System.Collections.Generic;

namespace CADBooster.SolidDna;

/// <summary>
/// Represents an interface for creating command items in the SolidWorks
/// </summary>
public interface ICommandCreatable
{
    /// <summary>
    /// Name of the command item
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Creates the command item for the specified path
    /// </summary>
    /// <param name="path">The path to use for hierarchical naming. If empty, the item's name is used</param>
    /// <returns>A list of created command items</returns>
    IEnumerable<ICommandCreated> Create(string path = "");
}

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
    /// <param name="info">Create information containing cookie and path for hierarchical naming</param>
    /// <returns>A list of created command items. These items should be disposed when the commands are no longer needed or when the add-in lifetime ends</returns>
    IEnumerable<ICommandCreated> Create(ICommandContextCreateInfo info);
}

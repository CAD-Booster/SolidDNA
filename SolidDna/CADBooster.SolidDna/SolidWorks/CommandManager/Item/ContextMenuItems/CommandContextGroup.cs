using System.Collections.Generic;
using System.Linq;

namespace CADBooster.SolidDna;

/// <summary>
/// Represents a command context group in SolidWorks
/// </summary>
public class CommandContextGroup : ICommandCreatable
{
    private bool _isCreated;

    #region Public Properties

    /// <summary>
    /// The name of this command group that is displayed in the context menu
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Context menu items in this group
    /// </summary>
    public IEnumerable<ICommandCreatable> Items { get; set; }

    #endregion

    /// <summary>
    /// Creates the command context group and its items
    /// </summary>
    /// <param name="info">Create information. Should be <see cref="CommandContextItemCreateInfo"/> to create nested groups with proper path hierarchy</param>
    /// <returns>A list of created command context items</returns>
    /// <exception cref="SolidDnaException">Thrown if the group has already been created</exception>
    public IEnumerable<ICommandCreated> Create(ICommandContextCreateInfo info)
    {
        if (info is null)
            throw new SolidDnaException(
                SolidDnaErrors.CreateError(SolidDnaErrorTypeCode.SolidWorksCommandManager,
                    SolidDnaErrorCode.SolidWorksCommandManagerError,
                    "Context menu create info cannot be null"));

        if (_isCreated)
            throw new SolidDnaException(
                SolidDnaErrors.CreateError(SolidDnaErrorTypeCode.SolidWorksCommandManager,
                SolidDnaErrorCode.SolidWorksCommandContextMenuItemReActivateError));

        if (string.IsNullOrWhiteSpace(Name))
            throw new SolidDnaException(
                SolidDnaErrors.CreateError(SolidDnaErrorTypeCode.SolidWorksCommandManager,
                    SolidDnaErrorCode.SolidWorksCommandManagerError,
                    "Context menu group name cannot be null or empty"));

        if (Items is null)
            throw new SolidDnaException(
                SolidDnaErrors.CreateError(SolidDnaErrorTypeCode.SolidWorksCommandManager,
                    SolidDnaErrorCode.SolidWorksCommandManagerError,
                    "Context menu group items cannot be null"));

        if (!Items.Any())
            throw new SolidDnaException(
                SolidDnaErrors.CreateError(SolidDnaErrorTypeCode.SolidWorksCommandManager,
                    SolidDnaErrorCode.SolidWorksCommandManagerError,
                    "Context menu group items cannot be empty"));

        // Ensure we have CommandContextItemCreateInfo with path, create one if needed
        // Empty path means this is a root-level group (not nested under another group)
        var itemInfo = info as CommandContextItemCreateInfo 
            ?? new CommandContextItemCreateInfo(info.SolidWorksCookie, string.Empty);

        _isCreated = true;

        return Enumerable.Repeat(new CommandContextGroupCreated(Name, itemInfo, Items), 1);
    }

    public override string ToString() => $"ContextGroup with name: {Name}. Count of sub items: {Items?.Count() ?? 0}";
}

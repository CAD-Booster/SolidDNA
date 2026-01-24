using System;
using System.Collections.Generic;
using System.Linq;

namespace CADBooster.SolidDna;

/// <summary>
/// A command context menu item base class
/// </summary>
public abstract class CommandContextBase 
{
    private bool _isCreated;

    #region Public Properties

    /// <summary>
    /// The help text for this item.
    /// </summary>
    public string Hint { get; set; }

    /// <summary>
    /// True to show this item in the context menu when an assembly is open.
    /// </summary>
    public bool VisibleForAssemblies { get; set; } = true;

    /// <summary>
    /// True to show this item in the context menu when a drawing is open.
    /// </summary>
    public bool VisibleForDrawings { get; set; } = true;

    /// <summary>
    /// True to show this item in the context menu when a part is open.
    /// </summary>
    public bool VisibleForParts { get; set; } = true;

    /// <summary>
    /// The action to call when the item is clicked
    /// </summary>
    public Action OnClick { get; set; }

    /// <summary>
    /// The action to call when the item state requested
    /// </summary>
    public Action<CommandManagerItemStateCheckArgs> OnStateCheck { get; set; }

    /// <summary>
    /// The selection type that determines where the context menu will be shown
    /// </summary>
    public SelectionType SelectionType { get; set; } = SelectionType.Everything;

    #endregion

    /// <summary>
    /// Creates the command context item for the specified document types
    /// </summary>
    /// <param name="path">The path to use for hierarchical naming. If empty, the item's name is used</param>
    /// <returns>A list of created command context items</returns>
    /// <exception cref="SolidDnaException">Thrown if the item has already been created</exception>
    public virtual IEnumerable<ICommandCreated> Create(string path = "")
    {
        if (_isCreated)
            throw new SolidDnaException(
                SolidDnaErrors.CreateError(SolidDnaErrorTypeCode.SolidWorksCommandManager,
                SolidDnaErrorCode.SolidWorksCommandContextMenuItemReActivateError));

        _isCreated = true;

        return Enumerable.Empty<ICommandCreated>();
    }
}

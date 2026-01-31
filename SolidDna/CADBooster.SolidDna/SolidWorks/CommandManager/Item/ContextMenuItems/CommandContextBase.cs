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
    public abstract Action<CommandManagerItemStateCheckArgs> OnStateCheck { get; set; }

    /// <summary>
    /// The selection type that determines with which selection context the item will be shown
    /// </summary>
    public SelectionType SelectionType { get; set; } = SelectionType.Everything;

    #endregion

    /// <summary>
    /// Creates the command context item for the specified document types in derived classes.
    /// The base class only implements restriction of multiple creation; it does not create anything.
    /// </summary>
    /// <param name="info">Create information containing cookie and other context</param>
    /// <returns>Base class method returns empty enumerable</returns>
    /// <exception cref="SolidDnaException">Thrown if the item has already been created</exception>
    public virtual IEnumerable<ICommandCreated> Create(ICommandContextCreateInfo info)
    {
        if (_isCreated)
            throw new SolidDnaException(
                SolidDnaErrors.CreateError(SolidDnaErrorTypeCode.SolidWorksCommandManager,
                SolidDnaErrorCode.SolidWorksCommandContextMenuItemReActivateError));

        _isCreated = true;

        return Enumerable.Empty<ICommandCreated>();
    }
}

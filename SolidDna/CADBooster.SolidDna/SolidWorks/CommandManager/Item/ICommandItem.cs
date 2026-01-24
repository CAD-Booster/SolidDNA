using System;

namespace CADBooster.SolidDna;

/// <summary>
/// Represents an interface for a command item
/// </summary>
public interface ICommandItem
{
    /// <summary>
    /// The unique Callback ID
    /// </summary>
    string CallbackId { get; }

    /// <summary>
    /// The action to call when the item is clicked
    /// </summary>
    Action OnClick { get; }

    /// <summary>
    /// The action to call when the item's state is requested
    /// </summary>
    Action<CommandManagerItemStateCheckArgs> OnStateCheck { get; }
}
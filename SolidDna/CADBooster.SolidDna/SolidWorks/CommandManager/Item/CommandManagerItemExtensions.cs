using System.Collections.Generic;
using System.Linq;

namespace CADBooster.SolidDna;

/// <summary>
/// Provides extension methods for converting <see cref="CommandManagerItem"/> objects to <see cref="ICommandCreatable"/>
/// </summary>
public static class CommandManagerItemExtensions
{
    /// <summary>
    /// Converts a collection of <see cref="CommandManagerItem"/> objects to <see cref="CommandContextItem"/> objects
    /// </summary>
    /// <param name="items">The collection of <see cref="CommandManagerItem"/> objects to convert</param>
    /// <returns>A collection of <see cref="CommandContextItem"/> objects</returns>
    public static IEnumerable<CommandContextItem> AsCommandContextItems(this IEnumerable<CommandManagerItem> items)
        => items.AsCommandContextItems(SelectionType.Everything);

    /// <summary>
    /// Converts a collection of <see cref="CommandManagerItem"/> objects to <see cref="CommandContextItem"/> objects
    /// </summary>
    /// <param name="items">The collection of <see cref="CommandManagerItem"/> objects to convert</param>
    /// <param name="selectType">The selection type for all items</param>
    /// <returns>A collection of <see cref="CommandContextItem"/> objects</returns>
    public static IEnumerable<CommandContextItem> AsCommandContextItems(this IEnumerable<CommandManagerItem> items,
                                                                        SelectionType selectType)
        => items.Select(x => x.AsCommandContextItem(selectType));

    /// <summary>
    /// Converts a single <see cref="CommandManagerItem"/> object to an <see cref="CommandContextItem"/> object
    /// </summary>
    /// <param name="item">The <see cref="CommandManagerItem"/> object to convert</param>
    /// <returns>An <see cref="CommandContextItem"/> object</returns>
    public static CommandContextItem AsCommandContextItem(this CommandManagerItem item)
        => item.AsCommandContextItem(SelectionType.Everything);

    /// <summary>
    /// Converts a single <see cref="CommandManagerItem"/> object to the <see cref="CommandContextItem"/> object
    /// </summary>
    /// <param name="item">The <see cref="CommandManagerItem"/> object to convert</param>
    /// <param name="selectType">The selection type for the item (defaults to <see cref="SelectionType.Everything"/>)</param>
    /// <returns>A <see cref="CommandContextItem"/>. Cloned new item</returns>
    public static CommandContextItem AsCommandContextItem(this CommandManagerItem item, SelectionType selectType)
        => new CommandContextItem()
        {
            Name = item.Name,
            Hint = item.Hint,
            OnClick = item.OnClick,
            OnStateCheck = item.OnStateCheck,
            SelectionType = selectType,
            VisibleForAssemblies = item.VisibleForAssemblies,
            VisibleForDrawings = item.VisibleForDrawings,
            VisibleForParts = item.VisibleForParts
        };

    /// <summary>
    /// Converts a single <see cref="CommandManagerItem"/> object to an <see cref="CommandContextIcon"/> object
    /// </summary>
    /// <param name="item">The <see cref="CommandManagerItem"/> object to convert</param>
    /// <param name="iconPathFormat">
    /// Absolute path to the image files that contain the single icon.
    /// Based on a string format, replacing {0} with the size. For example C:\Folder\Icon{0}.png
    /// If batch icon files are provided, SolidWorks uses the first icon (no index support).
    /// </param>
    /// <returns>An <see cref="CommandContextIcon"/> object</returns>
    public static CommandContextIcon AsCommandContextIcon(this CommandManagerItem item, string iconPathFormat)
        => item.AsCommandContextIcon(iconPathFormat, SelectionType.Everything);

    /// <summary>
    /// Converts a single <see cref="CommandManagerItem"/> object to the <see cref="CommandContextIcon"/> object
    /// </summary>
    /// <param name="item">The <see cref="CommandManagerItem"/> object to convert</param>
    /// <param name="iconPathFormat">
    /// Absolute path to the image files that contain the single icon.
    /// Based on a string format, replacing {0} with the size. For example C:\Folder\Icon{0}.png
    /// If batch icon files are provided, SolidWorks uses the first icon (no index support).
    /// </param>
    /// <param name="selectType">The selection type for the item (defaults to <see cref="SelectionType.Everything"/>)</param>
    /// <returns>A <see cref="CommandContextIcon"/>. Cloned new item</returns>
    public static CommandContextIcon AsCommandContextIcon(this CommandManagerItem item, string iconPathFormat, SelectionType selectType)
        => new CommandContextIcon()
        {
            Hint = item.Hint,
            IconPathFormat = iconPathFormat,
            OnClick = item.OnClick,
            OnStateCheck = item.OnStateCheck,
            SelectionType = selectType,
            VisibleForAssemblies = item.VisibleForAssemblies,
            VisibleForDrawings = item.VisibleForDrawings,
            VisibleForParts = item.VisibleForParts
        };

    /// <summary>
    /// Converts a collection of <see cref="CommandManagerItem"/> objects to <see cref="CommandContextIcon"/> objects
    /// </summary>
    /// <param name="items">The collection of <see cref="CommandManagerItem"/> objects to convert</param>
    /// <param name="iconPathFormat">
    /// Absolute path to the image files that contain the single icon.
    /// Based on a string format, replacing {0} with the size. For example C:\Folder\Icon{0}.png
    /// If batch icon files are provided, SolidWorks uses the first icon (no index support).
    /// </param>
    /// <returns>A collection of <see cref="CommandContextIcon"/> objects</returns>
    public static IEnumerable<CommandContextIcon> AsCommandContextIcons(this IEnumerable<CommandManagerItem> items,
                                                                        string iconPathFormat)
        => items.AsCommandContextIcons(iconPathFormat, SelectionType.Everything);

    /// <summary>
    /// Converts a collection of <see cref="CommandManagerItem"/> objects to <see cref="CommandContextIcon"/> objects
    /// </summary>
    /// <param name="items">The collection of <see cref="CommandManagerItem"/> objects to convert</param>
    /// <param name="iconPathFormat">
    /// Absolute path to the image files that contain the single icon.
    /// Based on a string format, replacing {0} with the size. For example C:\Folder\Icon{0}.png
    /// If batch icon files are provided, SolidWorks uses the first icon (no index support).
    /// </param>
    /// <param name="selectType">The selection type for all items</param>
    /// <returns>A collection of <see cref="CommandContextIcon"/> objects</returns>
    public static IEnumerable<CommandContextIcon> AsCommandContextIcons(this IEnumerable<CommandManagerItem> items,
                                                                        string iconPathFormat,
                                                                        SelectionType selectType)
        => items.Select(x => x.AsCommandContextIcon(iconPathFormat, selectType));
}

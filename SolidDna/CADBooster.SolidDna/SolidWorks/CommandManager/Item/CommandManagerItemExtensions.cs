using SolidWorks.Interop.swconst;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CADBooster.SolidDna
{
    /// <summary>
    /// Provides extension methods for converting <see cref="CommandManagerItem"/> objects to <see cref="ICommandCreatable"/>
    /// </summary>
    public static class CommandManagerItemExtensions
    {
        /// <summary>
        /// Converts a collection of <see cref="CommandManagerItem"/> objects to <see cref="ICommandCreatable"/> objects
        /// </summary>
        /// <param name="items">The collection of <see cref="CommandManagerItem"/> objects to convert</param>
        /// <param name="selectTypeSelector">An optional function to determine the selection type for each item</param>
        /// <returns>A collection of <see cref="ICommandCreatable"/> objects</returns>
        public static IEnumerable<ICommandCreatable> AsCommandCreatable(this IEnumerable<CommandManagerItem> items,
                                                                        Func<CommandManagerItem, SelectionType> selectTypeSelector = null)
            => items.Select(x =>
                x.AsCommandCreatable(
                    selectTypeSelector is null
                    ? SelectionType.Everything
                    : selectTypeSelector.Invoke(x)));

        /// <summary>
        /// Converts a single <see cref="CommandManagerItem"/> object to an <see cref="ICommandCreatable"/> object
        /// </summary>
        /// <param name="item">The <see cref="CommandManagerItem"/> object to convert</param>
        /// <returns>An <see cref="ICommandCreatable"/> object</returns>
        public static ICommandCreatable AsCommandCreatable(this CommandManagerItem item)
            => item.AsCommandCreatable(SelectionType.Everything);

        /// <summary>
        /// Converts a single <see cref="CommandManagerItem"/> object to an <see cref="ICommandCreatable"/> object
        /// </summary>
        /// <param name="item">The <see cref="CommandManagerItem"/> object to convert</param>
        /// <param name="selectType">The selection type for the item (defaults to <see cref="SelectionType.Everything"/>)</param>
        /// <returns>An <see cref="ICommandCreatable"/> object</returns>
        public static ICommandCreatable AsCommandCreatable(this CommandManagerItem item, SelectionType selectType)
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
    }
}

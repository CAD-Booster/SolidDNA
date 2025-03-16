using CADBooster.SolidDna.SolidWorks.CommandManager.Item;
using SolidWorks.Interop.swconst;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CADBooster.SolidDna
{
    public static class CommandManagerItemExtensions
    {
        public static IEnumerable<ICommandCreatable> AsCommandCreatable(this IEnumerable<CommandManagerItem> items,
                                                                        Func<CommandManagerItem, swSelectType_e> selectTypeSelector = null)
            => items.Select(x =>
                x.AsCommandCreatable(
                    selectTypeSelector is null
                    ? swSelectType_e.swSelEVERYTHING
                    : selectTypeSelector.Invoke(x)));

        public static ICommandCreatable AsCommandCreatable(this CommandManagerItem item, swSelectType_e selectType = swSelectType_e.swSelEVERYTHING)
            => new CommandContextItem()
            {
                Name = item.Name,
                Hint = item.Hint,
                OnClick = item.OnClick,
                OnStateCheck = item.OnStateCheck,
                SelectionType = selectType
            };
    }
}

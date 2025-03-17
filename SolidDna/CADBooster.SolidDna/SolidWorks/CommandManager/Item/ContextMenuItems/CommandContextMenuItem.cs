using CADBooster.SolidDna.SolidWorks.CommandManager.Item;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.Collections.Generic;
using System.Windows.Shapes;

namespace CADBooster.SolidDna
{
    /// <summary>
    /// A command context menu in SolidWorks
    /// </summary>
    public class CommandContextItem : ICommandCreatable
    {
        private bool _isCreated;
        #region Public Properties

        /// <summary>
        /// The hint of this command group
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The help text for this item. Is only used for toolbar items and flyouts, underneath the <see cref="Tooltip"/>. Is not used for menus and flyout items.
        /// </summary>
        public string Hint { get; set; }

        /// <summary>
        /// True to show this item in the command tab when an assembly is open. Only works for toolbar items and flyouts, not for menus or flyout items.
        /// </summary>
        public bool VisibleForAssemblies { get; set; } = true;

        /// <summary>
        /// True to show this item in the command tab when a drawing is open. Only works for toolbar items and flyouts, not for menus or flyout items.
        /// </summary>
        public bool VisibleForDrawings { get; set; } = true;

        /// <summary>
        /// True to show this item in the command tab when a part is open. Only works for toolbar items and flyouts, not for menus or flyout items.
        /// </summary>
        public bool VisibleForParts { get; set; } = true;

        /// <summary>
        /// The action to call when the item is clicked
        /// </summary>
        public Action OnClick { get; set; }

        /// <summary>
        /// The action to call when the item state requested
        /// </summary>
        public Action<ItemStateCheckArgs> OnStateCheck { get; set; }

        public swSelectType_e SelectionType { get; set; } = swSelectType_e.swSelNOTHING;

        #endregion

        /// <summary>
        /// Create a command manager flyout (group).
        /// </summary>
        public CommandContextItem()
        {
        }

        /// <summary>
        /// Remove, then re-add all items to the flyout.
        /// Is called on every click of the flyout, but only does something on the first click.
        /// SolidWorks calls this a 'dynamic flyout' in the help.
        /// </summary>
        public IEnumerable<ICommandCreated> Create(string path = "")
        {
            //if (_isCreated)
            //    throw new NotImplementedException(); // TODO

            _isCreated = true;

            var fullName = string.IsNullOrEmpty(path) ? $"{Name}" : $"{path}@{Name}";

            var created = new List<CommandContextItemCreated>();

            if (VisibleForAssemblies)
                created.Add(new CommandContextItemCreated(this, fullName, DocumentType.Assembly));
            if (VisibleForDrawings)
                created.Add(new CommandContextItemCreated(this, fullName, DocumentType.Drawing));
            if (VisibleForParts)
                created.Add(new CommandContextItemCreated(this, fullName, DocumentType.Part));

            return created;
        }
    }
}

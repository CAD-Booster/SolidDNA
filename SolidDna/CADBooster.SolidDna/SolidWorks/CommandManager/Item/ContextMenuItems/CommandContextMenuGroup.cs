using CADBooster.SolidDna.SolidWorks.CommandManager.Item;
using SolidWorks.Interop.swconst;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CADBooster.SolidDna
{
    /// <summary>
    /// A command context menu group in SolidWorks
    /// </summary>
    public class CommandContextMenuGroup : ICommandCreatable
    {
        private bool _isCreated;

        #region Public Properties

        /// <summary>
        /// The name of this command group
        /// </summary>
        public string Name { get; set; }

        public List<ICommandCreatable> Items { get; set; }

        #endregion

        /// <summary>
        /// Remove, then re-add all items to the flyout.
        /// Is called on every click of the flyout, but only does something on the first click.
        /// SolidWorks calls this a 'dynamic flyout' in the help.
        /// </summary>
        public IEnumerable<ICommandCreated> Create(string path)
        {
            if (_isCreated)
                throw new NotImplementedException(); // TODO

            _isCreated = true;

            return Enumerable.Repeat(new CommandContextMenuGroupCreated(Name, path, Items), 1);
        }

        public override string ToString() => $"ContextMenuGroup with name: {Name}. Count of sub items: {Items.Count}";
    }
}

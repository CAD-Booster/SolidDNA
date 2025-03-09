using CADBooster.SolidDna.SolidWorks.CommandManager.Item;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CADBooster.SolidDna.SolidWorks.CommandManager.Group
{
    /// <summary>
    /// A command flyout for the top command bar in SolidWorks
    /// </summary>
    public class CommandContextMenuGroup : ICommandCreatable, IDisposable
    {
        private bool _isCreated;

        #region Public Properties

        /// <summary>
        /// The ID used when this command flyout was created
        /// </summary>
        public int UserId { get; }

        /// <summary>
        /// The hint of this command group
        /// </summary>
        public string Name { get; set; }

        public List<ICommandCreatable> Items { get; set; }

        #endregion

        /// <summary>
        /// Create a command manager flyout (group).
        /// </summary>
        public CommandContextMenuGroup()
        {
        }

        /// <summary>
        /// Remove, then re-add all items to the flyout.
        /// Is called on every click of the flyout, but only does something on the first click.
        /// SolidWorks calls this a 'dynamic flyout' in the help.
        /// </summary>
        public /* TODO*/ void Create(string path)
        {
            if (_isCreated)
                throw new NotImplementedException(); // TODO

            _isCreated = true;

            var name = string.IsNullOrEmpty(path) ? $"{Name}" : $"{path}@{Name}";

            for (var i = 0; i < Items.Count; i++)
            {
                var item = Items[i];
                item.Create(name);
            }
        }

        /// <summary>
        /// Disposing
        /// </summary>
        public void Dispose()
        {
            // Dispose child items
            foreach (var item in Items.OfType<IDisposable>())
                item.Dispose();
        }

        public override string ToString() => $"ContextMenuGroup with name: {Name}. Count of sub items: {Items.Count}";
    }
}

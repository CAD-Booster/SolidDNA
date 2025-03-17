using CADBooster.SolidDna.SolidWorks.CommandManager.Item;
using System.Collections.Generic;
using System.Linq;

namespace CADBooster.SolidDna
{
    /// <summary>
    /// Represents a command context menu group in SolidWorks
    /// </summary>
    public class CommandContextMenuGroup : ICommandCreatable
    {
        private bool _isCreated;

        #region Public Properties

        /// <summary>
        /// The name of this command group
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Context menu items in this group
        /// </summary>
        public List<ICommandCreatable> Items { get; set; }

        #endregion

        /// <summary>
        /// Creates the command context menu group and its items
        /// </summary>
        /// <param name="path">The hierarchical path for the group</param>
        /// <returns>A list of created command context menu items</returns>
        /// <exception cref="SolidDnaException">Thrown if the group has already been created</exception>
        public IEnumerable<ICommandCreated> Create(string path)
        {
            if (_isCreated)
                throw new SolidDnaException(
                    SolidDnaErrors.CreateError(SolidDnaErrorTypeCode.SolidWorksCommandManager,
                    SolidDnaErrorCode.SolidWorksCommandContextMenuItemReActivateError));

            _isCreated = true;

            return Enumerable.Repeat(new CommandContextMenuGroupCreated(Name, path, Items), 1);
        }

        public override string ToString() => $"ContextMenuGroup with name: {Name}. Count of sub items: {Items.Count}";
    }
}

using System.Runtime.InteropServices;

namespace CADBooster.SolidDna
{
    /// <summary>
    /// A command context menu item
    /// </summary>
    internal class CommandContextItemCreated : CommandContextCreatedBase
    {
        /// <summary>
        /// Gets the name of this command context item
        /// </summary>
        public sealed override string Name => _name;

        /// <summary>
        /// Gets the command ID assigned by SolidWorks for this item
        /// </summary>
        public int CommandId { get; }

        private readonly string _name;

        /// <summary>
        /// Creates the command context item for the specified document types
        /// </summary>
        /// <param name="path">The path to use for hierarchical naming. If empty, the item's name is used</param>
        /// <returns>A list of created command context items</returns>
        /// <exception cref="SolidDnaException">Thrown if the item has already been created</exception>
        public CommandContextItemCreated(CommandContextItem commandContextItem,
                                         string fullName,
                                         DocumentType documentType) : base(commandContextItem, documentType)
        {
            _name = commandContextItem.Name;

            /// We have <see cref="ICommandManager.AddContextMenu"/>, but it adds a group, and there's no possibility to add a root item.
            CommandId = AddInIntegration.SolidWorks.UnsafeObject.AddMenuPopupItem3(
                (int)DocumentType,
                SolidWorksEnvironment.Application.SolidWorksCookie,
                (int)SelectionType,
                fullName,
                $"{nameof(SolidAddIn.Callback)}({CallbackId})",
                $"{nameof(SolidAddIn.ItemStateCheck)}({CallbackId})",
                Hint,
                string.Empty
            );
        }
    }
}

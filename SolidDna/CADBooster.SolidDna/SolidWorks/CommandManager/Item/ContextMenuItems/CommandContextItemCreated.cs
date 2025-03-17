using SolidWorks.Interop.swconst;
using System;

namespace CADBooster.SolidDna
{
    internal class CommandContextItemCreated : ICommandCreated, ICommandItem
    {
        /// <summary>
        /// Gets the unique callback ID for this command context item
        /// </summary>
        public string CallbackId { get; } = Guid.NewGuid().ToString("N");

        /// <summary>
        /// Gets the command ID assigned by SolidWorks for this item
        /// </summary>
        public int CommandId { get; }

        /// <summary>
        /// Gets the name of this command context item
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the hint text for this command context item
        /// </summary>
        public string Hint { get; }

        /// <summary>
        /// Gets the selection type that determines where this item is shown
        /// </summary>
        public swSelectType_e SelectionType { get; }

        /// <summary>
        /// Gets the document type (Assembly, Part, or Drawing) for which this item is created
        /// </summary>
        public DocumentType DocumentType { get; }

        /// <summary>
        /// Gets the action to call when this item is clicked
        /// </summary>
        public Action OnClick { get; }

        /// <summary>
        /// Gets the action to call when the state of this item is checked
        /// </summary>
        public Action<ItemStateCheckArgs> OnStateCheck { get; private set; }

        /// <summary>
        /// Gets the full name of this command context item, including its hierarchical path
        /// </summary>
        public string FullName { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandContextItemCreated"/> class
        /// </summary>
        /// <param name="commandContextItem">The command context item to create</param>
        /// <param name="fullName">The full name of the item, including its hierarchical path</param>
        /// <param name="documentType">The document type (Assembly, Part, or Drawing) for which this item is created</param>
        public CommandContextItemCreated(CommandContextItem commandContextItem, string fullName, DocumentType documentType)
        {
            Name = commandContextItem.Name;
            Hint = commandContextItem.Hint;
            OnClick = commandContextItem.OnClick;
            OnStateCheck = commandContextItem.OnStateCheck;
            SelectionType = commandContextItem.SelectionType;
            DocumentType = documentType;
            FullName = fullName;

            /// Maybe we can add an icon using <see cref="IFrame.AddMenuPopupIcon2"/>
            /// Also, we have <see cref="ICommandManager.AddContextMenu"/>, but it adds a group, and there's no possibility to add a root item.

            CommandId = AddInIntegration.SolidWorks.UnsafeObject.AddMenuPopupItem3(
                    (int)DocumentType,
                    SolidWorksEnvironment.Application.SolidWorksCookie,
                    (int)SelectionType,
                    FullName,
                    $"{nameof(SolidAddIn.Callback)}({CallbackId})",
                    $"{nameof(SolidAddIn.ItemStateCheck)}({CallbackId})",
                    Hint,
                    string.Empty
            );

            // Listen out for callbacks
            PlugInIntegration.CallbackFired += PlugInIntegration_CallbackFired;

            // Listen out for EnableMethod
            PlugInIntegration.ItemStateCheckFired += PlugInIntegration_EnableMethodFired;

            Logger.LogDebugSource("Context menu item created");
        }

        /// <summary>
        /// Fired when a SolidWorks callback is fired
        /// </summary>
        /// <param name="name">The name of the callback that was fired</param>
        private void PlugInIntegration_CallbackFired(string name)
        {
            if (CallbackId != name)
                return;

            // Call the action
            OnClick?.Invoke();
        }

        /// <summary>
        /// Fired when a SolidWorks UpdateCallbackFunction is fired
        /// </summary>
        /// <param name="args">The arguments for user handling</param>
        private void PlugInIntegration_EnableMethodFired(ItemStateCheckArgs args)
        {
            if (CallbackId != args.CallbackId)
                return;

            // Call the action
            OnStateCheck?.Invoke(args);
        }

        /// <summary>
        /// Disposing
        /// </summary>
        public void Dispose()
        {
            /// I can't find the way to remove the item

            /// It always returns false, and the item isn't removed.
            //var removeMenuPopupItemResult = AddInIntegration.SolidWorks.UnsafeObject.RemoveMenuPopupItem2(
            //    (int)DocumentType,
            //    SolidWorksEnvironment.Application.SolidWorksCookie,
            //    (int)SelectionType,
            //    FullName,
            //    $"{nameof(SolidAddIn.Callback)}({CallbackId})",
            //    $"{nameof(SolidAddIn.ItemStateCheck)}({CallbackId})",
            //    Hint,
            //    string.Empty
            //);

            /// It always returns true, but the item isn't removed.
            //var removeFromPopupMenuResult = AddInIntegration.SolidWorks.UnsafeObject.RemoveFromPopupMenu(
            //    CommandId,
            //    (int)DocumentType,
            //    (int)SelectionType,
            //    true
            //);

            /// Besides, the user can hide it using <see cref="ICommandItem.OnStateCheck"/>.

            // Stop listening out for callbacks
            PlugInIntegration.CallbackFired -= PlugInIntegration_CallbackFired;
            PlugInIntegration.ItemStateCheckFired -= PlugInIntegration_EnableMethodFired;
        }
    }
}

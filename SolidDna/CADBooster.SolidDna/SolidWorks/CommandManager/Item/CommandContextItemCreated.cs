using SolidWorks.Interop.swconst;
using System;
using System.Diagnostics;

namespace CADBooster.SolidDna
{
    internal class CommandContextItemCreated : ICommandCreated, ICommandItem
    {
        public string CallbackId { get; } = Guid.NewGuid().ToString("N");
        public int CommandId { get; }
        public string Name { get; }
        public string Hint { get; }
        public swSelectType_e SelectionType { get; }
        public DocumentType DocumentType { get; }
        public Action OnClick { get; }
        public Action<ItemStateCheckArgs> OnStateCheck { get; private set; }
        public string FullName { get; }

        public CommandContextItemCreated(CommandContextItem commandContextItem, string fullName, DocumentType documentType)
        {
            Name = commandContextItem.Name;
            Hint = commandContextItem.Hint;
            OnClick = commandContextItem.OnClick;
            OnStateCheck = commandContextItem.OnStateCheck;
            SelectionType = commandContextItem.SelectionType;
            DocumentType = documentType;
            FullName = fullName;

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
            if (args.CallbackId == "CommandContextItemCreated")
                Debugger.Break();

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
            // I can't find the way to remove the item

            // It always returns false, and the item isn't removed
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

            // It always returns true, but the item isn't removed
            //var removeFromPopupMenuResult = AddInIntegration.SolidWorks.UnsafeObject.RemoveFromPopupMenu(
            //    CommandId,
            //    (int)DocumentType,
            //    (int)SelectionType,
            //    true
            //);

            // Stop listening out for callbacks
            PlugInIntegration.CallbackFired -= PlugInIntegration_CallbackFired;
            PlugInIntegration.ItemStateCheckFired -= PlugInIntegration_EnableMethodFired;
        }
    }
}

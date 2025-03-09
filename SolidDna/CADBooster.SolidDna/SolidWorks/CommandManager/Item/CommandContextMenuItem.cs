using CADBooster.SolidDna.SolidWorks.CommandManager.Item;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace CADBooster.SolidDna
{
    /// <summary>
    /// A command flyout for the top command bar in SolidWorks
    /// </summary>
    public class CommandContextItem : ICommandItem, ICommandCreatable, IDisposable
    {
        private bool _isCreated;
        #region Public Properties

        /// <summary>
        /// The ID used when this command flyout was created
        /// </summary>
        public int UserId { get; }

        /// <summary>
        /// The unique Callback ID (set by creator)
        /// </summary>
        public string CallbackId { get; } = Guid.NewGuid().ToString("N");

        /// <summary>
        /// The command ID for this item
        /// </summary>
        public int CommandId { get; private set; }

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
        public void Create(string path = "")
        {
            if (_isCreated)
                throw new NotImplementedException(); // TODO

            _isCreated = true;

            var name = string.IsNullOrEmpty(path) ? $"{Name}" : $"{path}@{Name}";

            if(VisibleForAssemblies)
                CreateForDocumentType(name, DocumentType.Assembly);
            if(VisibleForDrawings)
                CreateForDocumentType(name, DocumentType.Drawing);
            if(VisibleForParts)
                CreateForDocumentType(name, DocumentType.Part);

            // Listen out for callbacks
            PlugInIntegration.CallbackFired += PlugInIntegration_CallbackFired;

            // Listen out for EnableMethod
            PlugInIntegration.ItemStateCheckFired += PlugInIntegration_EnableMethodFired;
        }

        private void CreateForDocumentType(string name, DocumentType documentType)
        {
            CommandId = AddInIntegration.SolidWorks.UnsafeObject.AddMenuPopupItem3(
                    (int)documentType,
                    SolidWorksEnvironment.Application.SolidWorksCookie,
                    (int)SelectionType,
                    name,
                    $"{nameof(SolidAddIn.Callback)}({CallbackId})",
                    $"{nameof(SolidAddIn.ItemStateCheck)}({CallbackId})",
                    Hint,
                    null
            );
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
            // Stop listening out for callbacks
            PlugInIntegration.CallbackFired -= PlugInIntegration_CallbackFired;
            PlugInIntegration.ItemStateCheckFired -= PlugInIntegration_EnableMethodFired;
        }

        public override string ToString() => $"Flyout with name: {Name}. CommandID: {CommandId}. Hint: {Hint}.";
    }

    public static class CommandManagerItemExtensions
    {
        public static IEnumerable<ICommandCreatable> AsCommandCreatable(this IEnumerable<CommandManagerItem> items, Func<CommandManagerItem, swSelectType_e> selectTypeSelector = null)
            => items.Select(x =>
                new CommandContextItem()
                {
                    Name = x.Name,
                    Hint = x.Hint,
                    OnClick = x.OnClick,
                    OnStateCheck = x.OnStateCheck,
                    SelectionType = selectTypeSelector is null ? swSelectType_e.swSelEVERYTHING : selectTypeSelector.Invoke(x)
                });
    }
}

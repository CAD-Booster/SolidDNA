using CADBooster.SolidDna.SolidWorks.CommandManager.Item;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Shapes;

namespace CADBooster.SolidDna
{
    /// <summary>
    /// A command flyout for the top command bar in SolidWorks
    /// </summary>
    public class CommandContextItem : ICommandItem, ICommandCreatable
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
        public IEnumerable<ICommandCreated> Create(string path = "")
        {
            if (_isCreated)
                throw new NotImplementedException(); // TODO

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

        public override string ToString() => $"Flyout with name: {Name}. CommandID: {CommandId}. Hint: {Hint}.";
    }

    public interface ICommandCreated : IDisposable
    {
        int UserId { get; }
        string CallbackId { get; }
        string Name { get; }
    }

    internal class CommandContextItemCreated : ICommandCreated
    {

        public int UserId { get; }
        public string CallbackId => _callbackId;
        public int CommandId { get; }
        public string Name { get; }
        public string Hint { get; }
        public swSelectType_e SelectionType { get; }
        public DocumentType DocumentType { get; }
        public Action OnClick { get; }
        public Action<ItemStateCheckArgs> OnStateCheck { get; private set; }
        public string FullName { get; }

        private readonly string _menuCallback;
        private readonly string _menuEnableMethod;
        //private bool _isDisposed;
        private static readonly Action<ItemStateCheckArgs> _hideStateAction = arg => arg.Result = ItemState.Hidden;
#pragma warning disable IDE0032 // Use auto property
        private readonly string _callbackId = Guid.NewGuid().ToString("N");
#pragma warning restore IDE0032 // Use auto property

        public CommandContextItemCreated(CommandContextItem commandContextItem, string fullName, DocumentType documentType)
        {
            UserId = commandContextItem.UserId;
            Name = commandContextItem.Name;
            Hint = commandContextItem.Hint;
            OnClick = commandContextItem.OnClick;
            OnStateCheck = commandContextItem.OnStateCheck;
            SelectionType = commandContextItem.SelectionType;
            DocumentType = documentType;
            FullName = fullName;

            _menuCallback = $"{nameof(SolidAddIn.Callback)}({CallbackId})";
            _menuEnableMethod = $"{nameof(SolidAddIn.ItemStateCheck)}({CallbackId})";

            // create a third-party icon in the context-sensitive menus of faces in parts
            var frame = (IFrame)AddInIntegration.SolidWorks.UnsafeObject.Frame();

            var boo = frame.AddMenuPopupIcon2(
                (int)swDocumentTypes_e.swDocASSEMBLY, 
                (int)swSelectType_e.swSelCOMPONENTS, 
                "contextsensitive",
                SolidWorksEnvironment.Application.SolidWorksCookie,
                "",                
                _menuCallback,
                _menuEnableMethod,

                @"C:\Users\gektvi\source\repos\GeKtvi\solidworks-api\Tutorials\09-CommandItems\SolidDna.CommandItems\bin\Debug\net48-windows\icons20.png"
            );
            var id = AddInIntegration.SolidWorks.UnsafeObject.RegisterThirdPartyPopupMenu();

            var res = AddInIntegration.SolidWorks.UnsafeObject.AddItemToThirdPartyPopupMenu2(
                    id,
                    (int)DocumentType,
                    Name,
                    SolidWorksEnvironment.Application.SolidWorksCookie,
                    _menuCallback,
                    _menuEnableMethod,
                    string.Empty,
                    Hint,
                    @"C:\Users\gektvi\source\repos\GeKtvi\solidworks-api\Tutorials\09-CommandItems\SolidDna.CommandItems\bin\Debug\net48-windows\icons20.png",
                    (int)swMenuItemType_e.swMenuItemType_Default
            );
            //// create and register the third party menu
            //registerID = SwApp.RegisterThirdPartyPopupMenu();
            //(int)swDocumentTypes_e.swDocPART, (int)swSelectType_e.swSelFACES, "contextsensitive", addinID, "CSCallbackFunction", "CSEnable", "", cmdGroup.SmallMainIcon);

            CommandId = AddInIntegration.SolidWorks.UnsafeObject.AddMenuPopupItem3(
                    (int)DocumentType,
                    SolidWorksEnvironment.Application.SolidWorksCookie,
                    (int)SelectionType,
                    FullName,
                    _menuCallback,
                    _menuEnableMethod,
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

            //if (_isDisposed)
            //{

            //}


            // Call the action
            OnStateCheck?.Invoke(args);
        }

        /// <summary>
        /// Disposing
        /// </summary>
        public void Dispose()
        {
            //_isDisposed = true;

            //OnStateCheck = _hideStateAction;

            var result = AddInIntegration.SolidWorks.UnsafeObject.RemoveItemFromThirdPartyPopupMenu(
                CommandId,
                (int)DocumentType.None,
                FullName,
                0
            );

            //var result0 = AddInIntegration.SolidWorks.UnsafeObject.RemoveMenuPopupItem2(
            //    (int)DocumentType.None,
            //    SolidWorksEnvironment.Application.SolidWorksCookie,
            //    (int)SelectionType,
            //    Name,
            //    _menuCallback,
            //    $"{nameof(SolidAddIn.Callback)}(CommandContextItemCreated)",
            //    Hint,
            //    string.Empty
            //);


            //var result = AddInIntegration.SolidWorks.UnsafeObject.RemoveFromPopupMenu(
            //    CommandId,
            //    (int)DocumentType.None,
            //    (int)swSelectType_e.swSelEVERYTHING,
            //    true
            //);

            //var result1 = AddInIntegration.SolidWorks.UnsafeObject.RemoveFromMenu(
            //    CommandId,
            //    (int)DocumentType,
            //    (int)1,
            //    true
            //);

            //var result2 = AddInIntegration.SolidWorks.UnsafeObject.RemoveFromMenu(
            //    CommandId,
            //    (int)DocumentType,
            //    (int)2,
            //    true
            //);

            //var result3 = AddInIntegration.SolidWorks.UnsafeObject.RemoveFromMenu(
            //    CommandId,
            //    (int)DocumentType,
            //    (int)3,
            //    true
            //);

            ////var com = AddInIntegration.SolidWorks.UnsafeObject.AddMenuPopupItem3(
            ////    (int)DocumentType,
            ////    SolidWorksEnvironment.Application.SolidWorksCookie,
            ////    (int)SelectionType,
            ////    FullName,
            ////    _menuCallback,
            ////    $"{nameof(SolidAddIn.Callback)}(CommandContextItemCreated)",
            ////    Hint,
            ////    string.Empty
            ////);

            //if (result)
            //    Logger.LogInformationSource("Item successful removed");
            //else
            //    Logger.LogErrorSource("Item is not successful removed");
            ////Logger.LogInformationSource("Item finally disposed");
            //// Stop listening out for callbacks
            //PlugInIntegration.CallbackFired -= PlugInIntegration_CallbackFired;
            //PlugInIntegration.ItemStateCheckFired -= PlugInIntegration_EnableMethodFired;
            //PlugInIntegration.ItemStateCheckFired -= PlugInIntegration_EnableMethodFired;
        }
    }

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

using CADBooster.SolidDna.SolidWorks.CommandManager.Item;
using SolidWorks.Interop.swconst;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CADBooster.SolidDna
{
    /// <summary>
    /// A command flyout for the top command bar in SolidWorks
    /// </summary>
    public class CommandContextMenuGroup : ICommandCreatable, IDisposable
    {
        private ObservableCollection<ICommandCreatable> _items = new ObservableCollection<ICommandCreatable>();
        private readonly Dictionary<ICommandCreatable, List<ICommandCreated>> _createdItems = new Dictionary<ICommandCreatable, List<ICommandCreated>>();
        private string _path = string.Empty;
        private bool _isItemsSetAllowed = true;

        #region Public Properties

        /// <summary>
        /// The ID used when this command flyout was created
        /// </summary>
        public int UserId { get; }

        /// <summary>
        /// The hint of this command group
        /// </summary>
        public string Name { get; set; }

        public ObservableCollection<ICommandCreatable> Items
        {
            get => _items;
            set
            {
                if (!_isItemsSetAllowed)
                    throw new NotImplementedException(); // TODO

                _isItemsSetAllowed = false;
                _items.CollectionChanged -= ItemsCollectionChanged;
                value.CollectionChanged += ItemsCollectionChanged;
                _items = value;
                //Create(_path);
            }
        }

        private void ItemsCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            Logger.LogTraceSource($"Context menu item group changed action: \t{e.Action}");
            Logger.LogTraceSource($"Context menu item group changed count: \t{_items.Count}");

            _isItemsSetAllowed = false;
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    Create(_path, e.NewItems.OfType<ICommandCreatable>());
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Replace:
                    Create(_path, e.NewItems.OfType<ICommandCreatable>());
                    if (e.OldItems != null)
                        foreach (var old in e.OldItems.OfType<ICommandCreatable>())
                        {
                            _createdItems[old].ForEach(x => x.Dispose());
                            _createdItems.Remove(old);
                        }
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    if (e.OldItems != null)
                        foreach (var old in e.OldItems.OfType<ICommandCreatable>())
                        {
                            _createdItems[old].ForEach(x => x.Dispose());
                            _createdItems.Remove(old);
                        }
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Reset:
                        foreach (var old in _createdItems.Values)
                            old.ForEach(x => x.Dispose());
                        _createdItems.Clear();
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Move:
                    Logger.LogWarningSource("Operation Move not supported.");
                    break;
            }

            Logger.LogTraceSource($"Context menu item group changed created count: \t{_createdItems.Count}");
        }

        #endregion

        /// <summary>
        /// Create a command manager flyout (group).
        /// </summary>
        public CommandContextMenuGroup()
        {
            _items.CollectionChanged += ItemsCollectionChanged;
        }

        /// <summary>
        /// Remove, then re-add all items to the flyout.
        /// Is called on every click of the flyout, but only does something on the first click.
        /// SolidWorks calls this a 'dynamic flyout' in the help.
        /// </summary>
        public IEnumerable<ICommandCreated> Create(string path) => Create(path, null);

        public IEnumerable<ICommandCreated> Create(string path, IEnumerable<ICommandCreatable> items = null)
        {
            _path = path;
            var name = string.IsNullOrEmpty(path) ? $"{Name}" : $"{path}@{Name}";

            var created = Enumerable.Empty<ICommandCreated>();
            foreach (var item in items ?? Items)
            {
                var createdItems = item.Create(name).ToList();
                _createdItems[item] = createdItems;
                created = created.Concat(createdItems);
            }

            return created.ToArray();
        }

        /// <summary>
        /// Disposing
        /// </summary>
        public void Dispose()
        {
            // Dispose child items
            foreach (var item in _createdItems.Values)
                item.ForEach(x => x.Dispose());
        }

        public override string ToString() => $"ContextMenuGroup with name: {Name}. Count of sub items: {Items.Count}";
    }
}

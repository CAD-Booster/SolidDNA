using CADBooster.SolidDna.SolidWorks.CommandManager.Item;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CADBooster.SolidDna
{
    internal class CommandContextMenuGroupCreated : ICommandCreated
    {
        public string CallbackId => Guid.Empty.ToString("N");
        public string Name { get; }

        private readonly List<ICommandCreated> _createdItems;

        public CommandContextMenuGroupCreated(string name, string path, List<ICommandCreatable> items)
        {
            Name = name;

            var fullName = string.IsNullOrEmpty(path) ? $"{Name}" : $"{path}@{Name}";

            _createdItems = items
                .SelectMany(x => x.Create(fullName))
                .ToList();
        }

        /// <summary>
        /// Disposing
        /// </summary>
        public void Dispose()
        {
            // Dispose child items
            _createdItems.ForEach(x => x.Dispose());
        }
    }
}

using System.Collections.Generic;

namespace CADBooster.SolidDna
{
    /// <summary>
    /// Represents a context icon in SolidWorks
    /// </summary>
    public class CommandContextIcon : CommandContextBase, ICommandCreatable
    {
        /// <summary>
        /// Gets or sets the icon formatted path
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// Gets the name of the command (implementing ICommandCreatable interface)
        /// </summary>
        string ICommandCreatable.Name => Hint;

        /// <summary>
        /// Creates the command context icon for the specified document types
        /// </summary>
        /// <param name="path">Not used for icon</param>
        /// <returns>A list of created command context icons</returns>
        /// <exception cref="SolidDnaException">Thrown if the item has already been created</exception>
        public sealed override IEnumerable<ICommandCreated> Create(string _s = "")
        {
            _ = base.Create();

            var created = new List<CommandContextIconCreated>();

            if (VisibleForAssemblies)
                created.Add(new CommandContextIconCreated(this, DocumentType.Assembly));
            if (VisibleForDrawings)
                created.Add(new CommandContextIconCreated(this, DocumentType.Drawing));
            if (VisibleForParts)
                created.Add(new CommandContextIconCreated(this, DocumentType.Part));

            return created;
        }
    }
}

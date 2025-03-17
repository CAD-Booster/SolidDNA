using System;

namespace CADBooster.SolidDna
{
    /// <summary>
    /// Represents an interface for a created command item in SolidWorks
    /// </summary>
    public interface ICommandCreated : IDisposable
    {
        /// <summary>
        /// Gets the name of the created command item
        /// </summary>
        string Name { get; }
    }
}

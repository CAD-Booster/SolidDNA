using System;
using System.IO;

namespace CADBooster.SolidDna;

/// <summary>
/// Extension methods for objects related to assembly (the .DLL and .exe types) information.
/// </summary>
public static class AssemblyObjectExtensions
{
    /// <summary>
    /// Get the full directory path of the physical file (typically .exe or .dll) for where the callers type is located.
    /// </summary>
    /// <param name="self">An instance of the calling type</param>
    /// <returns>The directory path that contains this file</returns>
    [Obsolete("Use AssemblyDirectoryPath instead")]
    public static string AssemblyPath(this object self) => AssemblyDirectoryPath(self);

    /// <summary>
    /// Get the full directory path of the physical file (typically .exe or .dll) for where the callers type is located.
    /// </summary>
    /// <param name="self">An instance of the calling type</param>
    /// <returns>The directory path that contains this file</returns>
    public static string AssemblyDirectoryPath(this object self) => Path.GetDirectoryName(self.AssemblyFilePath());

    /// <summary>
    /// Get the full path (including filename) of the physical file (typically .exe or .dll) for where the callers type is located.
    /// </summary>
    /// <param name="self">An instance of the calling type</param>
    /// <returns>The full path of this file that contains this type.</returns>
    public static string AssemblyFilePath(this object self) => self.GetType().Assembly.Location;

    /// <summary>
    /// Get the full path (including filename) of the physical file (typically .exe or .dll) for where the callers type is located.
    /// </summary>
    /// <param name="type">A calling type</param>
    /// <returns>The full path of this file that contains this type.</returns>
    public static string AssemblyFilePath(this Type type) => type.Assembly.Location;
}
using System;
using System.IO;

namespace CADBooster.SolidDna;

/// <summary>
/// A set of helper functions related to File data
/// </summary>
public static class FileHelpers
{
    /// <summary>
    /// Gets a file or folder name from a given path.
    /// <summary>
    /// Gets the codebase directory of a type in a normalized format, removing any file: prefixes
    /// </summary>
    /// <param name="type">The type to get the codebase from</param>
    /// <returns></returns>
    public static string CodeBaseNormalized(this Type type) => Path.GetDirectoryName(type.AssemblyBaseNormalized());

    /// <summary>
    /// Gets the assembly base of a type in a normalized format, removing any file: prefixes
    /// </summary>
    /// <param name="type">The type to get the assembly base from</param>
    /// <returns></returns>
    public static string AssemblyBaseNormalized(this Type type) => type.Assembly.CodeBase.Replace(@"file:\", "").Replace(@"file:///", "");
}
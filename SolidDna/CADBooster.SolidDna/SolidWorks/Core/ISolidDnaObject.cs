using System;

namespace CADBooster.SolidDna;

/// <summary>
/// Interface for SolidDnaObject to enable mocking and abstraction.
/// </summary>
public interface ISolidDnaObject
{
    /// <summary>
    /// The raw underlying COM object, without a type.
    /// </summary>
    object UnsafeObject { get; }
}

public interface ISolidDnaObject<out TSolidWorksObject> : ISolidDnaObject, IDisposable
{
    /// <summary>
    /// The raw underlying COM object, but with a type.
    /// Overrides the non-generic version.
    /// </summary>
    new TSolidWorksObject UnsafeObject { get; }
}
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

/// <summary>
/// Interface for SolidDnaObject to enable mocking and abstraction, with a typed underlying object. Overwrites the non-generic version.
/// </summary>
/// <typeparam name="TSolidWorksObject"></typeparam>
public interface ISolidDnaObject<out TSolidWorksObject> : ISolidDnaObject, IDisposable
{
    /// <summary>
    /// The raw underlying COM object, but with a type.
    /// Overrides the non-generic version.
    /// </summary>
    new TSolidWorksObject UnsafeObject { get; }
}
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.Collections.Generic;

namespace CADBooster.SolidDna;

/// <summary>
/// Interface for a SolidWorks custom property editor.
/// Enables mocking for unit testing code that consumes custom properties.
/// </summary>
public interface ICustomPropertyEditor : IDisposable
{
    #region Properties

    /// <summary>
    /// The raw underlying COM object.
    /// WARNING: Use with caution. You must handle all disposal from this point on.
    /// </summary>
    CustomPropertyManager UnsafeObject { get; }

    #endregion

    #region Methods

    /// <summary>
    /// Checks if a custom property exists.
    /// </summary>
    /// <param name="name">The name of the custom property</param>
    /// <returns>True if the property exists, false otherwise</returns>
    bool CustomPropertyExists(string name);

    /// <summary>
    /// Gets the value of a custom property by name.
    /// </summary>
    /// <param name="name">The name of the custom property</param>
    /// <param name="resolve">True to resolve the custom property value</param>
    /// <returns>The value of the custom property</returns>
    string GetCustomProperty(string name, bool resolve = false);

    /// <summary>
    /// Sets the value of a custom property by name.
    /// </summary>
    /// <param name="name">The name of the custom property</param>
    /// <param name="value">The value of the custom property</param>
    /// <param name="type">The type of the custom property</param>
    void SetCustomProperty(string name, string value, swCustomInfoType_e type = swCustomInfoType_e.swCustomInfoText);

    /// <summary>
    /// Deletes a custom property by name.
    /// </summary>
    /// <param name="name">The name of the custom property</param>
    void DeleteCustomProperty(string name);

    /// <summary>
    /// Gets a list of all custom properties.
    /// </summary>
    /// <returns>A list of all custom properties</returns>
    List<CustomProperty> GetCustomProperties();

    #endregion
}
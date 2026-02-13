using SolidWorks.Interop.sldworks;
using System;

namespace CADBooster.SolidDna;

/// <summary>
/// Interface for a SolidWorks drawing view.
/// Enables mocking for unit testing code that consumes drawing views.
/// </summary>
public interface IDrawingView : IDisposable
{
    #region Properties

    /// <summary>
    /// The drawing view type.
    /// </summary>
    DrawingViewType ViewType { get; }

    /// <summary>
    /// The name of the view.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// The X position of the view origin with respect to the drawing sheet origin.
    /// </summary>
    double PositionX { get; }

    /// <summary>
    /// The Y position of the view origin with respect to the drawing sheet origin.
    /// </summary>
    double PositionY { get; }

    /// <summary>
    /// The bounding box of the view.
    /// </summary>
    BoundingBox BoundingBox { get; }

    /// <summary>
    /// The raw underlying COM object.
    /// WARNING: Use with caution. You must handle all disposal from this point on.
    /// </summary>
    View UnsafeObject { get; }

    #endregion
}
using SolidWorks.Interop.swconst;

namespace CADBooster.SolidDna;

/// <summary>
/// The type of sketch segment, from <see cref="swSketchSegments_e"/>
/// </summary>
public enum SketchSegmentType
{
    /// <summary>
    /// Sketch segment is a line.
    /// </summary>
    Line = 0,

    /// <summary>
    /// Sketch segment is an arc.
    /// </summary>
    Arc = 1,

    /// <summary>
    /// Sketch segment is an ellipse.
    /// </summary>
    Ellipse = 2,

    /// <summary>
    /// Sketch segment is a spline.
    /// </summary>
    Spline = 3,

    /// <summary>
    /// Sketch segment is text.
    /// </summary>
    Text = 4,

    /// <summary>
    /// Sketch segment is a parabola.
    /// </summary>
    Parabola = 5,
}
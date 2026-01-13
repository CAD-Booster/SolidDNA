using SolidWorks.Interop.swconst;

namespace CADBooster.SolidDna;

/// <summary>
/// The type of sketch segment, from <see cref="swSketchSegments_e"/>
/// </summary>
public enum SketchSegmentType
{
    Line = 0,
    Arc = 1,
    Ellipse = 2,
    Spline = 3,
    Text = 4,
    Parabola = 5,
}
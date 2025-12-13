using SolidWorks.Interop.swconst;

namespace CADBooster.SolidDna
{
    /// <summary>
    /// The type of sketch segment, from <see cref="swSketchSegments_e"/>
    /// </summary>
    public enum SketchSegmentType
    {
        /// <summary>
        /// Represents a sketch point. Is not present in <see cref="swSketchSegments_e"/>, but we need it for <see cref="SketchSegmentId"/>.
        /// </summary>
        Point = -1,
        Line = 0,
        Arc = 1,
        Ellipse = 2,
        Spline = 3,
        Text = 4,
        Parabola = 5
    }
}

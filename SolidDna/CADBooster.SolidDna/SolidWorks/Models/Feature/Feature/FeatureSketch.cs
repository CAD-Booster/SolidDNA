using SolidWorks.Interop.sldworks;
using System.Collections.Generic;
using System.Linq;

namespace CADBooster.SolidDna;

/// <summary>
/// Represents a SolidWorks Sketch feature
/// </summary>
public class FeatureSketch : SolidDnaObject<ISketch>
{
    #region Constructor

    /// <summary>
    /// Default constructor
    /// </summary>
    public FeatureSketch(ISketch model) : base(model)
    {
    }

    #endregion

    #region Features

    /// <summary>
    /// Cast a sketch to a feature.
    /// </summary>
    /// <returns></returns>
    public ModelFeature AsFeature()
    {
        // ReSharper disable once SuspiciousTypeConversion.Global
        return new ModelFeature((Feature) UnsafeObject);
    }

    #endregion

    #region Get object from this sketch

    /// <summary>
    /// Get all sketch points in this sketch.
    /// </summary>
    /// <returns></returns>
    public IEnumerable<SketchPoint> GetSketchPoints() => ((object[]) UnsafeObject.GetSketchPoints2()).Cast<SketchPoint>();

    /// <summary>
    /// Get all sketch segments in this sketch.
    /// </summary>
    /// <returns></returns>
    public IEnumerable<SketchSegment> GetSketchSegments() => ((object[]) UnsafeObject.GetSketchSegments()).Cast<SketchSegment>();

    #endregion
}
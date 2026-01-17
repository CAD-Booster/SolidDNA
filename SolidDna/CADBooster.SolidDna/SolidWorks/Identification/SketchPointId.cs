using System;
using System.Collections.Generic;
using System.Linq;
using SolidWorks.Interop.sldworks;

namespace CADBooster.SolidDna;

/// <summary>
/// The unique ID for a sketch point. Consists of two longs, but is only unique in combination with the sketch (name).
/// This means that the same two long values can be used in different sketches. Use <see cref="SketchSegmentId"/> for sketch segments.
/// See <see href="https://help.solidworks.com/2026/english/api/sldworksapi/solidworks.interop.sldworks~solidworks.interop.sldworks.isketchpoint~getid.html"/>.
/// </summary>
public class SketchPointId
{
    #region Public properties

    /// <summary>
    /// First of two longs
    /// </summary>
    public long Id0 { get; }

    /// <summary>
    /// Second of two longs
    /// </summary>
    public long Id1 { get; }

    /// <summary>
    /// Name of the containing sketch
    /// </summary>
    public string SketchName { get; }

    #endregion

    #region Constructor

    /// <summary>
    /// The unique identifier for a point in a sketch.
    /// Is determined by its sketch name, two long/int values.
    /// </summary>
    /// <param name="sketchPoint"></param>
    public SketchPointId(ISketchPoint sketchPoint)
    {
        // Get the two longs 
        var ids = GetIds(sketchPoint);
        Id0 = ids[0];
        Id1 = ids[1];

        // Get the sketch 
        var featureSketch = new FeatureSketch(sketchPoint.GetSketch());

        // Get the sketch name by casting the sketch to a feature
        SketchName = featureSketch.AsFeature().FeatureName;
    }

    #endregion

    #region Equals, GetHashCode and ToString

    /// <Inheritdoc />
    public override string ToString() => $"Sketch Point ID {SketchName}-{Id0}-{Id1}";

    /// <summary>
    /// Get if this sketch point ID is equal to another sketch point ID.
    /// </summary>
    /// <param name="otherId"></param>
    /// <returns></returns>
    public bool Equals(SketchPointId otherId) => SketchName.Equals(otherId.SketchName, StringComparison.InvariantCultureIgnoreCase) && Id0 == otherId.Id0 && Id1 == otherId.Id1;

    /// <Inheritdoc />
    public override bool Equals(object obj)
    {
        if (obj == null || obj.GetType() != typeof(SketchPointId))
            return false;

        return Equals((SketchPointId) obj);
    }

    /// <Inheritdoc />
    public override int GetHashCode()
    {
        unchecked
        {
            var hashCode = SketchName != null ? SketchName.GetHashCode() : 0;
            hashCode = (hashCode * 397) ^ Id0.GetHashCode();
            hashCode = (hashCode * 397) ^ Id1.GetHashCode();
            return hashCode;
        }
    }

    #endregion

    #region Private methods

    /// <summary>
    /// Get two longs from two integers or longs.
    /// See https://help.solidworks.com/2026/english/api/sldworksapiprogguide/overview/Long_vs_Integer.htm
    /// </summary>
    /// <param name="sketchPoint"></param>
    /// <returns></returns>
    private static List<long> GetIds(ISketchPoint sketchPoint)
    {
        try
        {
            // Try getting the IDs as integers first, the convert them to longs
            return ((int[]) sketchPoint.GetID()).Select(Convert.ToInt64).ToList();
        }
        catch (Exception)
        {
            // If that fails, try getting them as longs directly
            return ((long[]) sketchPoint.GetID()).ToList();
        }
    }

    #endregion
}
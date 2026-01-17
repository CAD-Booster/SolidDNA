using System;
using System.Collections.Generic;
using System.Linq;
using SolidWorks.Interop.sldworks;

namespace CADBooster.SolidDna;

/// <summary>
/// The unique ID for a sketch segment. Consists of two longs, but is only unique in combination with the sketch (persistent ID) and sketch segment type.
/// This means that the same two long values can be used in different sketches and the same sketch can have segments with the same two long values but different types.
/// See <see href="https://help.solidworks.com/2026/english/api/sldworksapi/solidworks.interop.sldworks~solidworks.interop.sldworks.isketchsegment~getid.html"/>.
/// Sketch points are not sketch segments and cannot be cast to <see cref="SketchSegment"/>.
/// Sketch points also have two long values and a sketch name, so use <see cref="SketchPointId"/> for those.
/// </summary>
public class SketchSegmentId
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
    [Obsolete("Use SketchPersistentId instead. The sketch name can change, making it unreliable for identification.")]
    public string SketchName { get; }

    /// <summary>
    /// Persistent ID sketch that contains this sketch segment. This ID does not change when the sketch is renamed.
    /// </summary>
    public PersistentId SketchPersistentId { get; }

    /// <summary>
    /// Sketch segment type. 
    /// </summary>
    public SketchSegmentType Type { get; }

    #endregion

    #region Constructor

    /// <summary>
    /// The unique identifier for a sketch segment.
    /// Is determined by its sketch persistent ID, two long/int values and the type.
    /// </summary>
    /// <param name="sketchSegment"></param>
    public SketchSegmentId(ISketchSegment sketchSegment)
    {
        // Get the two longs 
        var ids = GetIds(sketchSegment);
        Id0 = ids[0];
        Id1 = ids[1];

        // Get the sketch 
        var featureSketch = new FeatureSketch(sketchSegment.GetSketch());
        var feature = featureSketch.AsFeature();

        // Get the sketch name by casting the sketch to a feature (kept for backward compatibility)
#pragma warning disable CS0618 // Type or member is obsolete
        SketchName = feature.FeatureName;
#pragma warning restore CS0618 // Type or member is obsolete

        // Get the sketch persistent ID
        SketchPersistentId = PersistentId.GetFromObject(feature.UnsafeObject);

        // Get the sketch segment type
        Type = (SketchSegmentType) sketchSegment.GetType();
    }

    #endregion

    #region Get a sketch segment by its id

    /// <summary>
    /// Get a sketch segment by its ID. Throws when it cannot find the sketch or the sketch segment.
    /// </summary>
    /// <returns>A sketch segment when found.</returns>
    public SketchSegment GetSketchSegment()
    {
        // Get the sketch. Throws when it fails.
        var featureSketch = SketchPersistentId.GetSketch();

        // Get all sketch segments
        var sketchSegments = featureSketch.GetSketchSegments();

        // Find a sketch segment with the matching ID
        var firstOrDefault = sketchSegments.FirstOrDefault(sketchSegment => new SketchSegmentId(sketchSegment).Equals(this));

        // Throw when we cannot find a sketch segment with this ID
        if (firstOrDefault == null)
            throw new SolidDnaException(SolidDnaErrors.CreateError(SolidDnaErrorTypeCode.Identification, SolidDnaErrorCode.IdentificationObjectNotFoundFromSketchSegmentId));

        // Return the found sketch segment
        return firstOrDefault;
    }

    #endregion

    #region Equals, GetHashCode and ToString

    /// <Inheritdoc />
    public override string ToString() => $"Sketch segment ID {Type}-{Id0}-{Id1}";

    /// <summary>
    /// Get if this sketch segment ID is equal to another sketch segment ID.
    /// </summary>
    /// <param name="otherId"></param>
    /// <returns></returns>
    public bool Equals(SketchSegmentId otherId) => Id0 == otherId.Id0 && Id1 == otherId.Id1 && Type == otherId.Type && SketchPersistentId.Equals(otherId.SketchPersistentId);

    /// <Inheritdoc />
    public override bool Equals(object obj)
    {
        if (obj == null || obj.GetType() != typeof(SketchSegmentId))
            return false;

        return Equals((SketchSegmentId) obj);
    }

    /// <Inheritdoc />
    public override int GetHashCode()
    {
        unchecked
        {
            var hashCode = SketchPersistentId.GetHashCode();
            hashCode = (hashCode * 397) ^ Id0.GetHashCode();
            hashCode = (hashCode * 397) ^ Id1.GetHashCode();
            hashCode = (hashCode * 397) ^ SketchPersistentId.GetHashCode();
            hashCode = (hashCode * 397) ^ (int) Type;
            return hashCode;
        }
    }

    #endregion

    #region Private methods

    /// <summary>
    /// Get two longs from two integers or longs.
    /// See https://help.solidworks.com/2026/english/api/sldworksapiprogguide/overview/Long_vs_Integer.htm
    /// </summary>
    /// <param name="sketchSegment"></param>
    /// <returns></returns>
    private static List<long> GetIds(ISketchSegment sketchSegment)
    {
        try
        {
            // Try getting the IDs as integers first, the convert them to longs
            return ((int[]) sketchSegment.GetID()).Select(Convert.ToInt64).ToList();
        }
        catch (Exception)
        {
            // If that fails, try getting them as longs directly
            return ((long[]) sketchSegment.GetID()).ToList();
        }
    }

    #endregion
}
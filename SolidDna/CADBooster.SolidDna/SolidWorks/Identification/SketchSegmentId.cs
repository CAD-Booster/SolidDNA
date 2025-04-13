using System;
using System.Collections.Generic;
using System.Linq;
using SolidWorks.Interop.sldworks;

namespace CADBooster.SolidDna
{
    /// <summary>
    /// The unique ID for a sketch segment. Consists of two longs, but is only unique in combination with the sketch (name) and sketch segment type.
    /// This means that the same two long values can be used in different sketches and the same sketch can have segments with the same two long values but different types.
    /// See https://help.solidworks.com/2025/english/api/sldworksapi/solidworks.interop.sldworks~solidworks.interop.sldworks.isketchsegment~getid.html
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
        public string SketchName { get; }

        /// <summary>
        /// Sketch segment type
        /// </summary>
        public SketchSegmentType Type { get; }

        #endregion

        #region Constructor

        /// <summary>
        /// The unique identifier for an item in a sketch.
        /// Is determined by its sketch name, two long/int values and the type.
        /// </summary>
        /// <param name="sketchSegment"></param>
        public SketchSegmentId(ISketchSegment sketchSegment)
        {
            // Get the two longs 
            var ids = GetIds(sketchSegment);
            Id0 = ids[0];
            Id1 = ids[1];

            // Get the sketch name by casting the sketch to a Feature first
            SketchName = ((Feature)sketchSegment.GetSketch()).Name;

            // Get the sketch segment type
            Type = (SketchSegmentType)sketchSegment.GetType();
        }

        #endregion

        #region Equals, GetHashCode and ToString

        // Inheritdoc
        public override string ToString() => $"Sketch segment ID {SketchName}-{Type}-{Id0}-{Id1}";

        /// <summary>
        /// Get if this sketch segment ID is equal to another sketch segment ID.
        /// </summary>
        /// <param name="otherId"></param>
        /// <returns></returns>
        public bool Equals(SketchSegmentId otherId)
        {
            return SketchName.Equals(otherId.SketchName, StringComparison.InvariantCultureIgnoreCase) && Id0 == otherId.Id0 && Id1 == otherId.Id1 && Type == otherId.Type;
        }

        // Inheritdoc
        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != typeof(SketchSegmentId))
                return false;

            return Equals((SketchSegmentId)obj);
        }

        // Inheritdoc
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = SketchName != null ? SketchName.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ Id0.GetHashCode();
                hashCode = (hashCode * 397) ^ Id1.GetHashCode();
                hashCode = (hashCode * 397) ^ (int)Type;
                return hashCode;
            }
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Get two longs from two integers or longs.
        /// See https://help.solidworks.com/2025/english/api/sldworksapiprogguide/overview/Long_vs_Integer.htm
        /// </summary>
        /// <param name="sketchSegment"></param>
        /// <returns></returns>
        private static List<long> GetIds(ISketchSegment sketchSegment)
        {
            try
            {
                // Try getting the IDs as integers first, the convert them to longs
                return ((int[])sketchSegment.GetID()).Select(Convert.ToInt64).ToList();
            }
            catch (Exception)
            {
                // If that fails, try getting them as longs directly
                return ((long[])sketchSegment.GetID()).ToList();
            }
        }

        #endregion
    }
}
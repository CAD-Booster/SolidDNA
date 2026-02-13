using SolidWorks.Interop.sldworks;

namespace CADBooster.SolidDna;

/// <summary>
/// Represents a SolidWorks Mate feature
/// </summary>
public class FeatureMate : SolidDnaObject<IMate2>
{
    #region Public properties

    /// <summary>
    /// Get the current mate alignment.
    /// </summary>
    public MateAlignment Alignment => (MateAlignment) UnsafeObject.Alignment;

    /// <summary>
    /// Get whether the mate can be flipped.
    /// </summary>
    public bool CanBeFlipped => UnsafeObject.CanBeFlipped;

    /// <summary>
    /// Get or set whether the mate is flipped.
    /// </summary>
    public bool IsFlipped
    {
        get => UnsafeObject.Flipped;
        set => UnsafeObject.Flipped = value;
    }

    /// <summary>
    /// Get the type of mate.
    /// </summary>
    public MateType Type => (MateType) UnsafeObject.Type;

    #endregion

    #region Constructor

    /// <summary>
    /// Default constructor
    /// </summary>
    public FeatureMate(IMate2 model) : base(model)
    {
    }

    #endregion

    #region Public methods

    /// <summary>
    /// Get the first mate entity. Returns null if not available.
    /// Call <see cref="GetEntityCount"/> to check how many are available.
    /// </summary>
    /// <returns></returns>
    public MateEntity GetEntity0() => new MateEntity(UnsafeObject.MateEntity(0)).CreateOrNull();

    /// <summary>
    /// Get the second mate entity. Returns null if not available.
    /// Call <see cref="GetEntityCount"/> to check how many are available.
    /// </summary>
    /// <returns></returns>
    public MateEntity GetEntity1() => new MateEntity(UnsafeObject.MateEntity(1)).CreateOrNull();

    /// <summary>
    /// Get the third mate entity. Returns null if not available, is null for most mate types.
    /// Call <see cref="GetEntityCount"/> to check how many are available.
    /// </summary>
    /// <returns></returns>
    public MateEntity GetEntity2() => new MateEntity(UnsafeObject.MateEntity(2)).CreateOrNull();

    /// <summary>
    /// Get the fourth mate entity. Returns null if not available, is null for most mate types.
    /// Call <see cref="GetEntityCount"/> to check how many are available.
    /// </summary>
    /// <returns></returns>
    public MateEntity GetEntity3() => new MateEntity(UnsafeObject.MateEntity(3)).CreateOrNull();

    /// <summary>
    /// Get the number of mate entities for this mate.
    /// </summary>
    public int GetEntityCount() => UnsafeObject.GetMateEntityCount();

    #endregion

    #region Equals, GetHashCode and ToString

    /// <summary>
    /// Get if this mate feature is equal to another mate feature.
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool Equals(FeatureMate other)
    {
        if (other == null)
            return false;

        // The feature comparison is the best check, but other properties may be faster to check first
        return Alignment == other.Alignment &&
               CanBeFlipped == other.CanBeFlipped &&
               IsFlipped == other.IsFlipped &&
               Type == other.Type &&
               GetEntityCount() == other.GetEntityCount() &&
               ((Feature) UnsafeObject).Equals((Feature) other.UnsafeObject);
    }

    /// <inheritdoc />
    public override bool Equals(object obj)
    {
        if (obj == null || obj.GetType() != typeof(FeatureMate))
            return false;

        return Equals((FeatureMate)obj);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        unchecked
        {
            var hashCode = Alignment.GetHashCode();
            hashCode = (hashCode * 397) ^ CanBeFlipped.GetHashCode();
            hashCode = (hashCode * 397) ^ IsFlipped.GetHashCode();
            hashCode = (hashCode * 397) ^ Type.GetHashCode();
            hashCode = (hashCode * 397) ^ GetEntityCount().GetHashCode();
            return hashCode;
        }
    }

    /// <inheritdoc />
    public override string ToString() => $"Mate feature, type {Type}, alignment {Alignment}, flipped {IsFlipped}, entity count {GetEntityCount()}";

    #endregion
}
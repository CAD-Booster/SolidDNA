using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;

namespace CADBooster.SolidDna;

/// <summary>
/// Represents a mate entity, a selected object used in a mate within a SolidWorks assembly.
/// Provides access to its underlying entity object, its type and the owning component.
/// </summary>
public class MateEntity : SolidDnaObject<IMateEntity2>
{
    #region Constructor

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="model"></param>
    public MateEntity(IMateEntity2 model) : base(model)
    {

    }

    #endregion

    #region Public properties

    /// <summary>
    /// Get the underlying entity object.
    /// </summary>
    public Entity Entity => (Entity) UnsafeObject.Reference;

    /// <summary>
    /// Get the component that owns the entity.
    /// </summary>
    public Component Component => new Component(UnsafeObject.ReferenceComponent).CreateOrNull();

    /// <summary>
    /// Get the type of the entity.
    /// </summary>
    public swSelectType_e EntityType => (swSelectType_e) UnsafeObject.ReferenceType2;

    #endregion

    #region Equals, GetHashCode and ToString

    /// <summary>
    /// Get if this mate entity is equal to another mate entity.
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool Equals(MateEntity other) => Entity.Equals(other.Entity) && Component.Equals(other.Component) && EntityType == other.EntityType;

    /// <Inheritdoc />
    public override bool Equals(object obj)
    {
        if (obj == null || obj.GetType() != typeof(MateEntity))
            return false;

        return Equals((MateEntity) obj);
    }

    /// <Inheritdoc />
    public override int GetHashCode()
    {
        unchecked
        {
            var hashCode = Entity.GetHashCode();
            hashCode = (hashCode * 397) ^ Component.GetHashCode();
            hashCode = (hashCode * 397) ^ EntityType.GetHashCode();
            return hashCode;
        }
    }

    /// <Inheritdoc />
    public override string ToString() => $"Mate entity of type {EntityType} from component {Component?.Name}";

    #endregion
}

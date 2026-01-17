using SolidWorks.Interop.sldworks;

namespace CADBooster.SolidDna;

/// <summary>
/// Represents a SolidWorks InPlace Mate feature, a special mate that is created when virtual components are mated in-context.
/// When you get the mates from a component, the list can contain a mix of normal mates and in-place mates.
/// </summary>
public class FeatureInPlaceMate : SolidDnaObject<IMateInPlace>
{
    #region Constructor

    /// <summary>
    /// Default constructor
    /// </summary>
    public FeatureInPlaceMate(IMateInPlace model) : base(model)
    {
    }

    #endregion
}
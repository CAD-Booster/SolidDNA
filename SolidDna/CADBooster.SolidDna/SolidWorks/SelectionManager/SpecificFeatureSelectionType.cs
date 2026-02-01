using SolidWorks.Interop.swconst;

namespace CADBooster.SolidDna;

/// <summary>
/// Provides pre-typed selection types for specific SolidWorks feature types.
/// These correspond to the string values returned by <see cref="SolidWorks.Interop.sldworks.IFeature.GetTypeName"/> 
/// and <see cref="SolidWorks.Interop.sldworks.IFeature.GetTypeName2"/>.
/// </summary>
/// <remarks>
/// <para>
/// These specific feature selection types should be used with <c>CommandContextItem</c> only.
/// They are not supported by other context menu types (e.g., <c>CommandContextIcon</c>, <c>CommandContextGroup</c>).
/// </para>
/// <para>
/// For complete documentation of feature types and their corresponding interfaces, see:
/// <see href="https://help.solidworks.com/2026/english/api/sldworksapi/SOLIDWORKS.Interop.sldworks~SOLIDWORKS.Interop.sldworks.IFeature~GetTypeName2.html">
/// IFeature.GetTypeName2 Method - SOLIDWORKS API Help
/// </see>
/// </para>
/// <para>
/// To get the feature type name for a selected feature in SolidDNA, use the 
/// <see cref="IModelFeature.FeatureTypeName"/> property 
/// and compare it with the selection types defined in this class.
/// If the desired feature type is not available in this class, you can create a custom selection type by calling 
/// <see cref="CreateSpecificFeatureType"/> with the feature type name string.
/// </para>
/// <para>
/// Use <see cref="SolidWorks.Interop.sldworks.IFeature.GetDefinition"/> to get a feature data object 
/// (e.g., IExtrudeFeatureData2, ILoftFeatureData, etc.), otherwise use 
/// <see cref="SolidWorks.Interop.sldworks.IFeature.GetSpecificFeature2"/> to get an object for a feature.
/// </para>
/// </remarks>
public static class SpecificFeatureSelectionType
{
    #region Assembly

    /// <summary>
    /// Assembly exploded view in ConfigurationManager.
    /// </summary>
    /// <remarks>
    /// This feature type does not have a corresponding SolidWorks API interface.
    /// </remarks>
    public static readonly SelectionType AssemblyExploder = CreateSpecificFeatureType("AsmExploder");

    /// <summary>
    /// Explode step for assembly exploded view.
    /// </summary>
    /// <remarks>
    /// This feature type does not have a corresponding SolidWorks API interface.
    /// </remarks>
    public static readonly SelectionType ComponentExplodeStep = CreateSpecificFeatureType("CompExplodeStep");

    /// <summary>
    /// Explode line profile sketch (3D sketch for explode lines).
    /// </summary>
    /// <remarks>
    /// NOTE: Orig name is ExplodeLineProfileFeature, but is is explode Sketch3D (Assembly > ConfigurationManager > ExplViewX > 3DExplodeX (Sketch3D))
    /// <para>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.ISketch"/>.
    /// SolidDNA wrapped feature: <see cref="FeatureSketch"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.ISketch.html">ISketch</seealso>
    /// </para>
    /// </remarks>
    public static readonly SelectionType ExplodeLineProfileSketch3D = CreateSpecificFeatureType("ExplodeLineProfileFeature");

    /// <summary>
    /// In-context feature holder.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IFeature"/>.
    /// SolidDNA wrapped feature: <see cref="ModelFeature"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IFeature.html">IFeature</seealso>
    /// </remarks>
    public static readonly SelectionType InContextFeatureHolder = CreateSpecificFeatureType("InContextFeatHolder");

    /// <summary>
    /// Magnetic ground plane for assemblies.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IFeature"/>.
    /// SolidDNA wrapped feature: <see cref="ModelFeature"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IFeature.html">IFeature</seealso>
    /// </remarks>
    public static readonly SelectionType MagneticGroundPlane = CreateSpecificFeatureType("MagneticGroundPlane");

    /// <summary>
    /// Cam-tangent mate (CamMate).
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.ICamFollowerMateFeatureData"/>.
    /// SolidDNA wrapped feature: <see cref="FeatureCamFollowerMateData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.ICamFollowerMateFeatureData.html">ICamFollowerMateFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType MateCamTangent = CreateSpecificFeatureType("MateCamTangent");

    /// <summary>
    /// Coincident mate.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.ICoincidentMateFeatureData"/>.
    /// SolidDNA wrapped feature: <see cref="FeatureCoincidentMateData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.ICoincidentMateFeatureData.html">ICoincidentMateFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType MateCoincident = CreateSpecificFeatureType("MateCoincident");

    /// <summary>
    /// Concentric mate.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IConcentricMateFeatureData"/>.
    /// SolidDNA wrapped feature: <see cref="FeatureConcentricMateData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IConcentricMateFeatureData.html">IConcentricMateFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType MateConcentric = CreateSpecificFeatureType("MateConcentric");

    /// <summary>
    /// Distance mate dimension.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IDistanceMateFeatureData"/>.
    /// SolidDNA wrapped feature: <see cref="FeatureDistanceMateData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IDistanceMateFeatureData.html">IDistanceMateFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType MateDistanceDimension = CreateSpecificFeatureType("MateDistanceDim");

    /// <summary>
    /// Gear mate dimension.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IGearMateFeatureData"/>.
    /// SolidDNA wrapped feature: <see cref="FeatureGearMateData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IGearMateFeatureData.html">IGearMateFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType MateGearDimension = CreateSpecificFeatureType("MateGearDim");

    /// <summary>
    /// Hinge mate (Mechanical mate).
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IHingeMateFeatureData"/>.
    /// SolidDNA wrapped feature: <see cref="FeatureHingeMateData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IHingeMateFeatureData.html">IHingeMateFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType MateHinge = CreateSpecificFeatureType("MateHinge");

    /// <summary>
    /// In-place mate.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IMate2"/>.
    /// SolidDNA wrapped feature: <see cref="FeatureInPlaceMate"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IMate2.html">IMate2</seealso>
    /// </remarks>
    public static readonly SelectionType MateInPlace = CreateSpecificFeatureType("MateInPlace");

    /// <summary>
    /// Distance mate with limit.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IDistanceMateFeatureData"/>.
    /// SolidDNA wrapped feature: <see cref="FeatureDistanceMateData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IDistanceMateFeatureData.html">IDistanceMateFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType MateLimitDistanceDimension = CreateSpecificFeatureType("MateLimitDistanceDim");

    /// <summary>
    /// Linear coupler mate (Mechanical mate).
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.ILinearCouplerMateFeatureData"/>.
    /// SolidDNA wrapped feature: <see cref="FeatureLinearCouplerMateData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.ILinearCouplerMateFeatureData.html">ILinearCouplerMateFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType MateLinearCoupler = CreateSpecificFeatureType("MateLinearCoupler");

    /// <summary>
    /// Lock mate.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.ILockMateFeatureData"/>.
    /// SolidDNA wrapped feature: <see cref="FeatureLockMateData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.ILockMateFeatureData.html">ILockMateFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType MateLock = CreateSpecificFeatureType("MateLock");

    /// <summary>
    /// Parallel mate.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IParallelMateFeatureData"/>.
    /// SolidDNA wrapped feature: <see cref="FeatureParallelMateData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IParallelMateFeatureData.html">IParallelMateFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType MateParallel = CreateSpecificFeatureType("MateParallel");

    /// <summary>
    /// Perpendicular mate.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IPerpendicularMateFeatureData"/>.
    /// SolidDNA wrapped feature: <see cref="FeaturePerpendicularMateData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IPerpendicularMateFeatureData.html">IPerpendicularMateFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType MatePerpendicular = CreateSpecificFeatureType("MatePerpendicular");

    /// <summary>
    /// Planar angle mate dimension.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IAngleMateFeatureData"/>.
    /// SolidDNA wrapped feature: <see cref="FeatureAngleMateData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IAngleMateFeatureData.html">IAngleMateFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType MatePlanarAngleDimension = CreateSpecificFeatureType("MatePlanarAngleDim");

    /// <summary>
    /// Profile center mate.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IProfileCenterMateFeatureData"/>.
    /// SolidDNA wrapped feature: <see cref="FeatureProfileCenterMateData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IProfileCenterMateFeatureData.html">IProfileCenterMateFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType MateProfileCenter = CreateSpecificFeatureType("MateProfileCenter");

    /// <summary>
    /// Rack and pinion mate dimension (Mechanical mate).
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IRackPinionMateFeatureData"/>.
    /// SolidDNA wrapped feature: <see cref="FeatureRackPinionMateData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IRackPinionMateFeatureData.html">IRackPinionMateFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType MateRackPinionDimension = CreateSpecificFeatureType("MateRackPinionDim");

    /// <summary>
    /// Screw mate (Mechanical mate).
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IScrewMateFeatureData"/>.
    /// SolidDNA wrapped feature: <see cref="FeatureScrewMateData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IScrewMateFeatureData.html">IScrewMateFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType MateScrew = CreateSpecificFeatureType("MateScrew");

    /// <summary>
    /// Slot mate (Advanced mate).
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.ISlotMateFeatureData"/>.
    /// SolidDNA wrapped feature: <see cref="FeatureSlotMateData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.ISlotMateFeatureData.html">ISlotMateFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType MateSlot = CreateSpecificFeatureType("MateSlot");

    /// <summary>
    /// Symmetric mate.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.ISymmetricMateFeatureData"/>.
    /// SolidDNA wrapped feature: <see cref="FeatureSymmetricMateData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.ISymmetricMateFeatureData.html">ISymmetricMateFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType MateSymmetric = CreateSpecificFeatureType("MateSymmetric");

    /// <summary>
    /// Tangent mate.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.ITangentMateFeatureData"/>.
    /// SolidDNA wrapped feature: <see cref="FeatureTangentMateData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.ITangentMateFeatureData.html">ITangentMateFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType MateTangent = CreateSpecificFeatureType("MateTangent");

    /// <summary>
    /// Universal joint mate (Mechanical mate).
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IUniversalJointMateFeatureData"/>.
    /// SolidDNA wrapped feature: <see cref="FeatureUniversalJointMateData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IUniversalJointMateFeatureData.html">IUniversalJointMateFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType MateUniversalJoint = CreateSpecificFeatureType("MateUniversalJoint");

    /// <summary>
    /// Width mate (Advanced mate).
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IWidthMateFeatureData"/>.
    /// SolidDNA wrapped feature: <see cref="FeatureWidthMateData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IWidthMateFeatureData.html">IWidthMateFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType MateWidth = CreateSpecificFeatureType("MateWidth");

    /// <summary>
    /// Mate reference position group (for SmartMates).
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IMateReference"/>.
    /// SolidDNA wrapped feature: <see cref="FeatureMateReference"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IMateReference.html">IMateReference</seealso>
    /// </remarks>
    public static readonly SelectionType MateReferencePositionGroup = CreateSpecificFeatureType("PosGroupFolder");

    /// <summary>
    /// Reference component in assembly.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IComponent2"/>.
    /// SolidDNA wrapped feature: <see cref="Component"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IComponent2.html">IComponent2</seealso>
    /// </remarks>
    public static readonly SelectionType Reference = CreateSpecificFeatureType("Reference");

    /// <summary>
    /// Reference pattern for components.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IComponent2"/>.
    /// SolidDNA wrapped feature: <see cref="Component"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IComponent2.html">IComponent2</seealso>
    /// </remarks>
    public static readonly SelectionType ReferencePattern = CreateSpecificFeatureType("ReferencePattern");

    /// <summary>
    /// Smart component feature.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.ISmartComponentFeatureData"/>.
    /// SolidDNA wrapped feature: <see cref="FeatureSmartComponentData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.ISmartComponentFeatureData.html">ISmartComponentFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType SmartComponent = CreateSpecificFeatureType("SmartComponentFeature");

    #endregion

    #region Body

    /// <summary>
    /// Advanced Hole Wizard feature.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IAdvancedHoleFeatureData"/>.
    /// SolidDNA wrapped feature: <see cref="FeatureAdvancedHoleWizardData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IAdvancedHoleFeatureData.html">IAdvancedHoleFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType AdvancedHoleWizard = CreateSpecificFeatureType("AdvHoleWzd");

    /// <summary>
    /// Fill pattern feature (array pattern).
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IFillPatternFeatureData"/>.
    /// SolidDNA wrapped feature: <see cref="FeatureFillPatternData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IFillPatternFeatureData.html">IFillPatternFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType ArrayPattern = CreateSpecificFeatureType("APattern");

    /// <summary>
    /// Base body feature created by first solid feature.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IExtrudeFeatureData2"/>.
    /// SolidDNA wrapped feature: <see cref="FeatureExtrudeData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IExtrudeFeatureData2.html">IExtrudeFeatureData2</seealso>
    /// </remarks>
    public static readonly SelectionType BaseBody = CreateSpecificFeatureType("BaseBody");

    /// <summary>
    /// Flex feature (bending).
    /// </summary>
    /// <remarks>
    /// This feature type does not have a corresponding SolidWorks API interface.
    /// </remarks>
    public static readonly SelectionType Bending = CreateSpecificFeatureType("Bending");

    /// <summary>
    /// Loft feature (solid).
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.ILoftFeatureData"/>.
    /// SolidDNA wrapped feature: <see cref="FeatureLoftData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.ILoftFeatureData.html">ILoftFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType Blend = CreateSpecificFeatureType("Blend");

    /// <summary>
    /// Loft cut feature (removes material).
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.ILoftFeatureData"/>.
    /// SolidDNA wrapped feature: <see cref="FeatureLoftData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.ILoftFeatureData.html">ILoftFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType BlendCut = CreateSpecificFeatureType("BlendCut");

    /// <summary>
    /// Explode step for body.
    /// </summary>
    /// <remarks>
    /// This feature type does not have a corresponding SolidWorks API interface.
    /// </remarks>
    public static readonly SelectionType BodyExplodeStep = CreateSpecificFeatureType("BodyExplodeStep");

    /// <summary>
    /// Extruded boss feature (adds material).
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IExtrudeFeatureData2"/>.
    /// SolidDNA wrapped feature: <see cref="FeatureExtrudeData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IExtrudeFeatureData2.html">IExtrudeFeatureData2</seealso>
    /// </remarks>
    public static readonly SelectionType Boss = CreateSpecificFeatureType("Boss");

    /// <summary>
    /// Thin feature extruded boss.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IExtrudeFeatureData2"/>.
    /// SolidDNA wrapped feature: <see cref="FeatureExtrudeData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IExtrudeFeatureData2.html">IExtrudeFeatureData2</seealso>
    /// </remarks>
    public static readonly SelectionType BossThin = CreateSpecificFeatureType("BossThin");

    /// <summary>
    /// Chamfer feature.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IChamferFeatureData2"/>.
    /// SolidDNA wrapped feature: <see cref="FeatureChamferData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IChamferFeatureData2.html">IChamferFeatureData2</seealso>
    /// </remarks>
    public static readonly SelectionType Chamfer = CreateSpecificFeatureType("Chamfer");

    /// <summary>
    /// Circular pattern feature.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.ICircularPatternFeatureData"/>.
    /// SolidDNA wrapped feature: <see cref="FeatureCircularPatternData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.ICircularPatternFeatureData.html">ICircularPatternFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType CircularPattern = CreateSpecificFeatureType("CirPattern");

    /// <summary>
    /// Combine bodies feature (add, subtract, or common).
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.ICombineBodiesFeatureData"/>.
    /// SolidDNA wrapped feature: <see cref="FeatureCombineBodiesData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.ICombineBodiesFeatureData.html">ICombineBodiesFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType CombineBodies = CreateSpecificFeatureType("CombineBodies");

    /// <summary>
    /// Cosmetic thread feature.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.ICosmeticThreadFeatureData"/>.
    /// SolidDNA wrapped feature: <see cref="FeatureCosmeticThreadData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.ICosmeticThreadFeatureData.html">ICosmeticThreadFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType CosmeticThread = CreateSpecificFeatureType("CosmeticThread");

    /// <summary>
    /// Cosmetic weld bead feature.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.ICosmeticWeldBeadFeatureData"/>.
    /// SolidDNA wrapped feature: <see cref="FeatureCosmeticWeldBeadData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.ICosmeticWeldBeadFeatureData.html">ICosmeticWeldBeadFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType CosmeticWeldBead = CreateSpecificFeatureType("CosmeticWeldBead");

    /// <summary>
    /// Save body as assembly feature.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.ISaveBodyFeatureData"/>.
    /// SolidDNA wrapped feature: <see cref="FeatureSaveBodyData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.ISaveBodyFeatureData.html">ISaveBodyFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType CreateAssembly = CreateSpecificFeatureType("CreateAssemFeat");

    /// <summary>
    /// Curve-driven pattern feature.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.ICurveDrivenPatternFeatureData"/>.
    /// SolidDNA wrapped feature: <see cref="FeatureCurveDrivenPatternData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.ICurveDrivenPatternFeatureData.html">ICurveDrivenPatternFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType CurvePattern = CreateSpecificFeatureType("CurvePattern");

    /// <summary>
    /// Extruded cut feature (removes material).
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IExtrudeFeatureData2"/>.
    /// SolidDNA wrapped feature: <see cref="FeatureExtrudeData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IExtrudeFeatureData2.html">IExtrudeFeatureData2</seealso>
    /// </remarks>
    public static readonly SelectionType Cut = CreateSpecificFeatureType("Cut");

    /// <summary>
    /// Thin feature extruded cut.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IExtrudeFeatureData2"/>.
    /// SolidDNA wrapped feature: <see cref="FeatureExtrudeData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IExtrudeFeatureData2.html">IExtrudeFeatureData2</seealso>
    /// </remarks>
    public static readonly SelectionType CutThin = CreateSpecificFeatureType("CutThin");

    /// <summary>
    /// Deform feature.
    /// </summary>
    /// <remarks>
    /// This feature type does not have a corresponding SolidWorks API interface.
    /// </remarks>
    public static readonly SelectionType Deform = CreateSpecificFeatureType("Deform");

    /// <summary>
    /// Delete body feature.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IDeleteBodyFeatureData"/>.
    /// SolidDNA wrapped feature: <see cref="FeatureDeleteBodyData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IDeleteBodyFeatureData.html">IDeleteBodyFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType DeleteBody = CreateSpecificFeatureType("DeleteBody");

    /// <summary>
    /// Delete face feature.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IDeleteFaceFeatureData"/>.
    /// SolidDNA wrapped feature: <see cref="FeatureDeleteFaceData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IDeleteFaceFeatureData.html">IDeleteFaceFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType DeleteFace = CreateSpecificFeatureType("DelFace");

    /// <summary>
    /// Derived circular pattern feature.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IDerivedPatternFeatureData"/>.
    /// SolidDNA wrapped feature: <see cref="FeatureDerivedPatternData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IDerivedPatternFeatureData.html">IDerivedPatternFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType DerivedCircularPattern = CreateSpecificFeatureType("DerivedCirPattern");

    /// <summary>
    /// Derived hole pattern feature.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IDerivedPatternFeatureData"/>.
    /// SolidDNA wrapped feature: <see cref="FeatureDerivedPatternData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IDerivedPatternFeatureData.html">IDerivedPatternFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType DerivedHolePattern = CreateSpecificFeatureType("DerivedHolePattern");

    /// <summary>
    /// Derived linear pattern feature.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IDerivedPatternFeatureData"/>.
    /// SolidDNA wrapped feature: <see cref="FeatureDerivedPatternData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IDerivedPatternFeatureData.html">IDerivedPatternFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType DerivedLinearPattern = CreateSpecificFeatureType("DerivedLPattern");

    /// <summary>
    /// Dimension pattern feature (variable pattern).
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IDimPatternFeatureData"/>.
    /// SolidDNA wrapped feature: <see cref="FeatureDimPatternData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IDimPatternFeatureData.html">IDimPatternFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType DimensionPattern = CreateSpecificFeatureType("DimPattern");

    /// <summary>
    /// Dome feature.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IDomeFeatureData2"/>.
    /// SolidDNA wrapped feature: <see cref="FeatureDomeData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IDomeFeatureData2.html">IDomeFeatureData2</seealso>
    /// </remarks>
    public static readonly SelectionType Dome = CreateSpecificFeatureType("Dome");

    /// <summary>
    /// Draft feature.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IDraftFeatureData2"/>.
    /// SolidDNA wrapped feature: <see cref="FeatureDraftData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IDraftFeatureData2.html">IDraftFeatureData2</seealso>
    /// </remarks>
    public static readonly SelectionType Draft = CreateSpecificFeatureType("Draft");

    /// <summary>
    /// Edge merge feature (heal edges).
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IHealEdgesFeatureData"/>.
    /// SolidDNA wrapped feature: <see cref="FeatureHealEdgesData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IHealEdgesFeatureData.html">IHealEdgesFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType EdgeMerge = CreateSpecificFeatureType("EdgeMerge");

    /// <summary>
    /// Wrap feature (emboss or deboss).
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IWrapSketchFeatureData"/>.
    /// SolidDNA wrapped feature: <see cref="FeatureWrapSketchData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IWrapSketchFeatureData.html">IWrapSketchFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType Emboss = CreateSpecificFeatureType("Emboss");

    /// <summary>
    /// Extrusion feature.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IExtrudeFeatureData2"/>.
    /// SolidDNA wrapped feature: <see cref="FeatureExtrudeData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IExtrudeFeatureData2.html">IExtrudeFeatureData2</seealso>
    /// </remarks>
    public static readonly SelectionType Extrusion = CreateSpecificFeatureType("Extrusion");

    /// <summary>
    /// Fillet feature.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.ISimpleFilletFeatureData2"/>.
    /// SolidDNA wrapped feature: <see cref="FeatureSimpleFilletData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.ISimpleFilletFeatureData2.html">ISimpleFilletFeatureData2</seealso>
    /// </remarks>
    public static readonly SelectionType Fillet = CreateSpecificFeatureType("Fillet");

    /// <summary>
    /// Helix/Spiral feature.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IHelixFeatureData"/>.
    /// SolidDNA wrapped feature: <see cref="FeatureHelixData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IHelixFeatureData.html">IHelixFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType Helix = CreateSpecificFeatureType("Helix");

    /// <summary>
    /// Hole series feature.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IHoleSeriesFeatureData2"/>.
    /// SolidDNA wrapped feature: <see cref="FeatureHoleSeriesData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IHoleSeriesFeatureData2.html">IHoleSeriesFeatureData2</seealso>
    /// </remarks>
    public static readonly SelectionType HoleSeries = CreateSpecificFeatureType("HoleSeries");

    /// <summary>
    /// Hole Wizard feature.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IWizardHoleFeatureData2"/>.
    /// SolidDNA wrapped feature: <see cref="FeatureHoleWizardData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IWizardHoleFeatureData2.html">IWizardHoleFeatureData2</seealso>
    /// </remarks>
    public static readonly SelectionType HoleWizard = CreateSpecificFeatureType("HoleWzd");

    /// <summary>
    /// Imported feature (solid body from imported file).
    /// </summary>
    /// <remarks>
    /// This feature type does not have a corresponding SolidWorks API interface.
    /// </remarks>
    public static readonly SelectionType Imported = CreateSpecificFeatureType("Imported");

    /// <summary>
    /// Local chain pattern feature.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IChainPatternFeatureData"/>.
    /// SolidDNA wrapped feature: <see cref="FeatureChainPatternData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IChainPatternFeatureData.html">IChainPatternFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType LocalChainPattern = CreateSpecificFeatureType("LocalChainPattern");

    /// <summary>
    /// Local circular pattern feature.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.ILocalCircularPatternFeatureData"/>.
    /// SolidDNA wrapped feature: <see cref="FeatureLocalCircularPatternData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.ILocalCircularPatternFeatureData.html">ILocalCircularPatternFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType LocalCircularPattern = CreateSpecificFeatureType("LocalCirPattern");

    /// <summary>
    /// Local curve pattern feature.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.ILocalCurvePatternFeatureData"/>.
    /// SolidDNA wrapped feature: <see cref="FeatureLocalCurvePatternData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.ILocalCurvePatternFeatureData.html">ILocalCurvePatternFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType LocalCurvePattern = CreateSpecificFeatureType("LocalCurvePattern");

    /// <summary>
    /// Local linear pattern feature.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.ILocalLinearPatternFeatureData"/>.
    /// SolidDNA wrapped feature: <see cref="FeatureLocalLinearPatternData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.ILocalLinearPatternFeatureData.html">ILocalLinearPatternFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType LocalLinearPattern = CreateSpecificFeatureType("LocalLPattern");

    /// <summary>
    /// Local sketch pattern feature.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.ILocalSketchPatternFeatureData"/>.
    /// SolidDNA wrapped feature: <see cref="FeatureLocalSketchPatternData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.ILocalSketchPatternFeatureData.html">ILocalSketchPatternFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType LocalSketchPattern = CreateSpecificFeatureType("LocalSketchPattern");

    /// <summary>
    /// Linear pattern feature.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.ILinearPatternFeatureData"/>.
    /// SolidDNA wrapped feature: <see cref="FeatureLinearPatternData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.ILinearPatternFeatureData.html">ILinearPatternFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType LinearPattern = CreateSpecificFeatureType("LPattern");

    /// <summary>
    /// Macro feature (custom feature).
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IMacroFeatureData"/>.
    /// SolidDNA wrapped feature: <see cref="FeatureMacroData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IMacroFeatureData.html">IMacroFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType MacroFeature = CreateSpecificFeatureType("MacroFeature");

    /// <summary>
    /// Mirror component feature (in assembly).
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IMirrorComponentFeatureData"/>.
    /// SolidDNA wrapped feature: <see cref="FeatureMirrorComponentData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IMirrorComponentFeatureData.html">IMirrorComponentFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType MirrorComponent = CreateSpecificFeatureType("MirrorCompFeat");

    /// <summary>
    /// Mirror pattern feature.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IMirrorPatternFeatureData"/>.
    /// SolidDNA wrapped feature: <see cref="FeatureMirrorPatternData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IMirrorPatternFeatureData.html">IMirrorPatternFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType MirrorPattern = CreateSpecificFeatureType("MirrorPattern");

    /// <summary>
    /// Mirror solid feature (mirror entire body).
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IMirrorSolidFeatureData"/>.
    /// SolidDNA wrapped feature: <see cref="FeatureMirrorSolidData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IMirrorSolidFeatureData.html">IMirrorSolidFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType MirrorSolid = CreateSpecificFeatureType("MirrorSolid");

    /// <summary>
    /// Mirror part feature (derived mirrored part).
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IMirrorPartFeatureData"/>.
    /// SolidDNA wrapped feature: <see cref="FeatureMirrorPartData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IMirrorPartFeatureData.html">IMirrorPartFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType MirrorStock = CreateSpecificFeatureType("MirrorStock");

    /// <summary>
    /// Move/Copy body feature.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IMoveCopyBodyFeatureData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IMoveCopyBodyFeatureData.html">IMoveCopyBodyFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType MoveCopyBody = CreateSpecificFeatureType("MoveCopyBody");

    /// <summary>
    /// Boundary boss/base feature.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IBoundaryBossFeatureData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IBoundaryBossFeatureData.html">IBoundaryBossFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType NetBlend = CreateSpecificFeatureType("NetBlend");

    /// <summary>
    /// Part exploded view in ConfigurationManager.
    /// </summary>
    /// <remarks>
    /// This feature type does not have a corresponding SolidWorks API interface.
    /// </remarks>
    public static readonly SelectionType PartExploder = CreateSpecificFeatureType("PrtExploder");

    /// <summary>
    /// Indent feature (punch).
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IIndentFeatureData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IIndentFeatureData.html">IIndentFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType Punch = CreateSpecificFeatureType("Punch");

    /// <summary>
    /// Replace face feature.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IReplaceFaceFeatureData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IReplaceFaceFeatureData.html">IReplaceFaceFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType ReplaceFace = CreateSpecificFeatureType("ReplaceFace");

    /// <summary>
    /// Revolved cut feature (removes material).
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IRevolveFeatureData2"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IRevolveFeatureData2.html">IRevolveFeatureData2</seealso>
    /// </remarks>
    public static readonly SelectionType RevolvedCut = CreateSpecificFeatureType("RevCut");

    /// <summary>
    /// Round fillet corner feature.
    /// </summary>
    /// <remarks>
    /// NOTE: Official docs say "Round fillet corner" but actual API returns "FilletCorner". 
    /// Feature can be created by Fillet > FilletExpert > Corner > CornerFaces > Show Alternative.
    /// In FeatureManager named (Fillet-CornerX).
    /// <para>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.ISimpleFilletFeatureData2"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.ISimpleFilletFeatureData2.html">ISimpleFilletFeatureData2</seealso>
    /// </para>
    /// </remarks>
    public static readonly SelectionType FilletCorner = CreateSpecificFeatureType("FilletCorner");

    /// <summary>
    /// Revolved boss/base feature (adds material).
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IRevolveFeatureData2"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IRevolveFeatureData2.html">IRevolveFeatureData2</seealso>
    /// </remarks>
    public static readonly SelectionType Revolution = CreateSpecificFeatureType("Revolution");

    /// <summary>
    /// Thin feature revolved boss/base.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IRevolveFeatureData2"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IRevolveFeatureData2.html">IRevolveFeatureData2</seealso>
    /// </remarks>
    public static readonly SelectionType RevolutionThin = CreateSpecificFeatureType("RevolutionThin");

    /// <summary>
    /// Rib feature.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IRibFeatureData2"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IRibFeatureData2.html">IRibFeatureData2</seealso>
    /// </remarks>
    public static readonly SelectionType Rib = CreateSpecificFeatureType("Rib");

    /// <summary>
    /// Rip feature (split face).
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IRipFeatureData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IRipFeatureData.html">IRipFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType Rip = CreateSpecificFeatureType("Rip");

    /// <summary>
    /// Intersect feature (sculpt).
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IIntersectFeatureData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IIntersectFeatureData.html">IIntersectFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType Sculpt = CreateSpecificFeatureType("Sculpt");

    /// <summary>
    /// Shape feature (freeform).
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IFreeFormFeatureData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IFreeFormFeatureData.html">IFreeFormFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType Shape = CreateSpecificFeatureType("Shape");

    /// <summary>
    /// Shell feature.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IShellFeatureData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IShellFeatureData.html">IShellFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType Shell = CreateSpecificFeatureType("Shell");

    /// <summary>
    /// Simple hole feature (sketch-based hole).
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.ISimpleHoleFeatureData2"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.ISimpleHoleFeatureData2.html">ISimpleHoleFeatureData2</seealso>
    /// </remarks>
    public static readonly SelectionType SketchHole = CreateSpecificFeatureType("SketchHole");

    /// <summary>
    /// Sketch-driven pattern feature.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.ISketchPatternFeatureData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.ISketchPatternFeatureData.html">ISketchPatternFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType SketchPattern = CreateSpecificFeatureType("SketchPattern");

    /// <summary>
    /// Split feature (split body or part).
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.ISplitBodyFeatureData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.ISplitBodyFeatureData.html">ISplitBodyFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType Split = CreateSpecificFeatureType("Split");

    /// <summary>
    /// Body created by splitting operation.
    /// </summary>
    /// <remarks>
    /// This feature type does not have a corresponding SolidWorks API interface.
    /// </remarks>
    public static readonly SelectionType SplitBody = CreateSpecificFeatureType("SplitBody");

    /// <summary>
    /// Derived configuration part feature.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IDerivedPartFeatureData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IDerivedPartFeatureData.html">IDerivedPartFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType Stock = CreateSpecificFeatureType("Stock");

    /// <summary>
    /// Swept boss/base feature (adds material).
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.ISweepFeatureData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.ISweepFeatureData.html">ISweepFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType Sweep = CreateSpecificFeatureType("Sweep");

    /// <summary>
    /// Swept cut feature (removes material).
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.ISweepFeatureData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.ISweepFeatureData.html">ISweepFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType SweepCut = CreateSpecificFeatureType("SweepCut");

    /// <summary>
    /// Thread feature (swept thread).
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IThreadFeatureData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IThreadFeatureData.html">IThreadFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType SweepThread = CreateSpecificFeatureType("SweepThread");

    /// <summary>
    /// Table pattern feature (coordinate-based pattern).
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.ITablePatternFeatureData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.ITablePatternFeatureData.html">ITablePatternFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType TablePattern = CreateSpecificFeatureType("TablePattern");

    /// <summary>
    /// Thicken feature (adds material to surface).
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IThickenFeatureData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IThickenFeatureData.html">IThickenFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType Thicken = CreateSpecificFeatureType("Thicken");

    /// <summary>
    /// Thicken cut feature (removes material from surface).
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IThickenFeatureData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IThickenFeatureData.html">IThickenFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType ThickenCut = CreateSpecificFeatureType("ThickenCut");

    /// <summary>
    /// Variable-radius fillet feature.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IVariableFilletFeatureData2"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IVariableFilletFeatureData2.html">IVariableFilletFeatureData2</seealso>
    /// </remarks>
    public static readonly SelectionType VariableFillet = CreateSpecificFeatureType("VarFillet");

    #endregion

    #region Drawing

    /// <summary>
    /// Bend table anchor feature in drawing.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IBendTableAnnotation"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IBendTableAnnotation.html">IBendTableAnnotation</seealso>
    /// </remarks>
    public static readonly SelectionType BendTableAnchor = CreateSpecificFeatureType("BendTableAchor");

    /// <summary>
    /// Bill of materials table feature.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IBomFeature"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IBomFeature.html">IBomFeature</seealso>
    /// </remarks>
    public static readonly SelectionType BillOfMaterials = CreateSpecificFeatureType("BomFeat");

    /// <summary>
    /// BOM template feature.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IBomTemplate"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IBomTemplate.html">IBomTemplate</seealso>
    /// </remarks>
    public static readonly SelectionType BomTemplate = CreateSpecificFeatureType("BomTemplate");

    /// <summary>
    /// Detail circle feature in drawing.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IDetailCircle"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IDetailCircle.html">IDetailCircle</seealso>
    /// </remarks>
    public static readonly SelectionType DetailCircle = CreateSpecificFeatureType("DetailCircle");

    /// <summary>
    /// Breakout section line feature in drawing.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IBreakoutSectionLine"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IBreakoutSectionLine.html">IBreakoutSectionLine</seealso>
    /// </remarks>
    public static readonly SelectionType DrawingBreakoutSectionLine = CreateSpecificFeatureType("DrBreakoutSectionLine");

    /// <summary>
    /// Section line feature in drawing.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.ISectionLine"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.ISectionLine.html">ISectionLine</seealso>
    /// </remarks>
    public static readonly SelectionType DrawingSectionLine = CreateSpecificFeatureType("DrSectionLine");

    /// <summary>
    /// General table anchor feature in drawing.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IGeneralTableFeature"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IGeneralTableFeature.html">IGeneralTableFeature</seealso>
    /// </remarks>
    public static readonly SelectionType GeneralTableAnchor = CreateSpecificFeatureType("GeneralTableAnchor");

    /// <summary>
    /// Hole table anchor feature in drawing.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IHoleTable"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IHoleTable.html">IHoleTable</seealso>
    /// </remarks>
    public static readonly SelectionType HoleTableAnchor = CreateSpecificFeatureType("HoleTableAnchor");

    /// <summary>
    /// Live section feature (3D cross section).
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.ILiveSection"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.ILiveSection.html">ILiveSection</seealso>
    /// </remarks>
    public static readonly SelectionType LiveSection = CreateSpecificFeatureType("LiveSection");

    /// <summary>
    /// Punch table anchor feature in drawing.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IPunchTable"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IPunchTable.html">IPunchTable</seealso>
    /// </remarks>
    public static readonly SelectionType PunchTableAnchor = CreateSpecificFeatureType("PunchTableAnchor");

    /// <summary>
    /// Revision table anchor feature in drawing.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IRevisionTableFeature"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IRevisionTableFeature.html">IRevisionTableFeature</seealso>
    /// </remarks>
    public static readonly SelectionType RevisionTableAnchor = CreateSpecificFeatureType("RevisionTableAnchor");

    /// <summary>
    /// Weldment cut list table anchor feature in drawing.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IWeldmentCutListFeature"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IWeldmentCutListFeature.html">IWeldmentCutListFeature</seealso>
    /// </remarks>
    public static readonly SelectionType WeldmentTableAnchor = CreateSpecificFeatureType("WeldmentTableAnchor");

    /// <summary>
    /// Weld table anchor feature in drawing.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IWeldmentCutListFeature"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IWeldmentCutListFeature.html">IWeldmentCutListFeature</seealso>
    /// </remarks>
    public static readonly SelectionType WeldTableAnchor = CreateSpecificFeatureType("WeldTableAnchor");

    #endregion

    #region Folder

    /// <summary>
    /// Block folder in FeatureManager design tree.
    /// </summary>
    /// <remarks>
    /// This feature type does not have a corresponding SolidWorks API interface.
    /// </remarks>
    public static readonly SelectionType BlockFolder = CreateSpecificFeatureType("BlockFolder");

    /// <summary>
    /// Comments folder in FeatureManager design tree.
    /// </summary>
    /// <remarks>
    /// This feature type does not have a corresponding SolidWorks API interface.
    /// </remarks>
    public static readonly SelectionType CommentsFolder = CreateSpecificFeatureType("CommentsFolder");

    /// <summary>
    /// Cosmetic weld sub-folder in FeatureManager design tree.
    /// </summary>
    /// <remarks>
    /// This feature type does not have a corresponding SolidWorks API interface.
    /// </remarks>
    public static readonly SelectionType CosmeticWeldSubFolder = CreateSpecificFeatureType("CosmeticWeldSubFolder");

    /// <summary>
    /// Cut list folder for weldments.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IBodyFolder"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IBodyFolder.html">IBodyFolder</seealso>
    /// </remarks>
    public static readonly SelectionType CutListFolder = CreateSpecificFeatureType("CutListFolder");

    /// <summary>
    /// Solid body folder in FeatureManager design tree.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IBodyFolder"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IBodyFolder.html">IBodyFolder</seealso>
    /// </remarks>
    public static readonly SelectionType FeatureSolidBodyFolder = CreateSpecificFeatureType("FeatSolidBodyFolder");

    /// <summary>
    /// Surface body folder in FeatureManager design tree.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IBodyFolder"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IBodyFolder.html">IBodyFolder</seealso>
    /// </remarks>
    public static readonly SelectionType FeatureSurfaceBodyFolder = CreateSpecificFeatureType("FeatSurfaceBodyFolder");

    /// <summary>
    /// Feature folder in FeatureManager design tree.
    /// </summary>
    /// <remarks>
    /// This feature type does not have a corresponding SolidWorks API interface.
    /// </remarks>
    public static readonly SelectionType FeatureFolder = CreateSpecificFeatureType("FtrFolder");

    /// <summary>
    /// Inserted feature folder in FeatureManager design tree.
    /// </summary>
    /// <remarks>
    /// This feature type does not have a corresponding SolidWorks API interface.
    /// </remarks>
    public static readonly SelectionType InsertedFeatureFolder = CreateSpecificFeatureType("InsertedFeatureFolder");

    /// <summary>
    /// Mate reference group folder in FeatureManager design tree.
    /// </summary>
    /// <remarks>
    /// This feature type does not have a corresponding SolidWorks API interface.
    /// </remarks>
    public static readonly SelectionType MateReferenceGroupFolder = CreateSpecificFeatureType("MateReferenceGroupFolder");

    /// <summary>
    /// Profile feature folder in FeatureManager design tree.
    /// </summary>
    /// <remarks>
    /// This feature type does not have a corresponding SolidWorks API interface.
    /// </remarks>
    public static readonly SelectionType ProfileFeatureFolder = CreateSpecificFeatureType("ProfileFtrFolder");

    /// <summary>
    /// Reference axis feature folder in FeatureManager design tree.
    /// </summary>
    /// <remarks>
    /// This feature type does not have a corresponding SolidWorks API interface.
    /// </remarks>
    public static readonly SelectionType ReferenceAxisFeatureFolder = CreateSpecificFeatureType("RefAxisFtrFolder");

    /// <summary>
    /// Reference plane feature folder in FeatureManager design tree.
    /// </summary>
    /// <remarks>
    /// This feature type does not have a corresponding SolidWorks API interface.
    /// </remarks>
    public static readonly SelectionType ReferencePlaneFeatureFolder = CreateSpecificFeatureType("RefPlaneFtrFolder");

    /// <summary>
    /// Sketch slice folder in FeatureManager design tree.
    /// </summary>
    /// <remarks>
    /// This feature type does not have a corresponding SolidWorks API interface.
    /// </remarks>
    public static readonly SelectionType SketchSliceFolder = CreateSpecificFeatureType("SketchSliceFolder");

    /// <summary>
    /// Solid body folder in FeatureManager design tree.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IBodyFolder"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IBodyFolder.html">IBodyFolder</seealso>
    /// </remarks>
    public static readonly SelectionType SolidBodyFolder = CreateSpecificFeatureType("SolidBodyFolder");

    /// <summary>
    /// Sub-atom folder in FeatureManager design tree.
    /// </summary>
    /// <remarks>
    /// This feature type does not have a corresponding SolidWorks API interface.
    /// </remarks>
    public static readonly SelectionType SubAtomFolder = CreateSpecificFeatureType("SubAtomFolder");

    /// <summary>
    /// Sub-weldment folder in FeatureManager design tree.
    /// </summary>
    /// <remarks>
    /// This feature type does not have a corresponding SolidWorks API interface.
    /// </remarks>
    public static readonly SelectionType SubWeldFolder = CreateSpecificFeatureType("SubWeldFolder");

    /// <summary>
    /// Surface body folder in FeatureManager design tree.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IBodyFolder"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IBodyFolder.html">IBodyFolder</seealso>
    /// </remarks>
    public static readonly SelectionType SurfaceBodyFolder = CreateSpecificFeatureType("SurfaceBodyFolder");

    /// <summary>
    /// Template flat pattern folder in FeatureManager design tree.
    /// </summary>
    /// <remarks>
    /// This feature type does not have a corresponding SolidWorks API interface.
    /// </remarks>
    public static readonly SelectionType TemplateFlatPattern = CreateSpecificFeatureType("TemplateFlatPattern");

    #endregion

    #region Imported File

    /// <summary>
    /// Multi-body import feature.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IImportDxfDwgData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IImportDxfDwgData.html">IImportDxfDwgData</seealso>
    /// </remarks>
    public static readonly SelectionType MultiBodyImport = CreateSpecificFeatureType("MBimport");

    #endregion

    #region Miscellaneous

    /// <summary>
    /// Attribute feature (custom attribute).
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IAttribute"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IAttribute.html">IAttribute</seealso>
    /// </remarks>
    public static readonly SelectionType Attribute = CreateSpecificFeatureType("Attribute");

    /// <summary>
    /// Block definition in sketch.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.ISketchBlockDefinition"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.ISketchBlockDefinition.html">ISketchBlockDefinition</seealso>
    /// </remarks>
    public static readonly SelectionType BlockDefinition = CreateSpecificFeatureType("BlockDef");

    /// <summary>
    /// Curve in file feature (imported curve).
    /// </summary>
    /// <remarks>
    /// This feature type does not have a corresponding SolidWorks API interface.
    /// </remarks>
    public static readonly SelectionType CurveInFile = CreateSpecificFeatureType("CurveInFile");

    /// <summary>
    /// Grid feature for visualization.
    /// </summary>
    /// <remarks>
    /// This feature type does not have a corresponding SolidWorks API interface.
    /// </remarks>
    public static readonly SelectionType Grid = CreateSpecificFeatureType("GridFeature");

    /// <summary>
    /// Library feature (Design Library part).
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.ILibraryFeatureData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.ILibraryFeatureData.html">ILibraryFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType Library = CreateSpecificFeatureType("LibraryFeature");

    /// <summary>
    /// Scale feature.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IScaleFeatureData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IScaleFeatureData.html">IScaleFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType Scale = CreateSpecificFeatureType("Scale");

    /// <summary>
    /// Sensor feature.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.ISensor"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.ISensor.html">ISensor</seealso>
    /// </remarks>
    public static readonly SelectionType Sensor = CreateSpecificFeatureType("Sensor");

    /// <summary>
    /// View body feature.
    /// </summary>
    /// <remarks>
    /// This feature type does not have a corresponding SolidWorks API interface.
    /// </remarks>
    public static readonly SelectionType ViewBody = CreateSpecificFeatureType("ViewBodyFeature");

    #endregion

    #region Mold

    /// <summary>
    /// Cavity feature in mold tooling.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.ICavityFeatureData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.ICavityFeatureData.html">ICavityFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType Cavity = CreateSpecificFeatureType("Cavity");

    /// <summary>
    /// Mold core and cavity solids feature.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IToolingsplits"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IToolingsplits.html">IToolingsplits</seealso>
    /// </remarks>
    public static readonly SelectionType MoldCoreCavitySolids = CreateSpecificFeatureType("MoldCoreCavitySolids");

    /// <summary>
    /// Mold parting geometry feature (parting surfaces).
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IPartingSurfaceFeatureData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IPartingSurfaceFeatureData.html">IPartingSurfaceFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType MoldPartingGeometry = CreateSpecificFeatureType("MoldPartingGeom");

    /// <summary>
    /// Mold parting line feature.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IPartinglineFeatureData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IPartinglineFeatureData.html">IPartinglineFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType MoldPartLine = CreateSpecificFeatureType("MoldPartLine");

    /// <summary>
    /// Mold shut-off surface feature.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IShutoffSurfaceFeatureData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IShutoffSurfaceFeatureData.html">IShutoffSurfaceFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType MoldShutOffSurface = CreateSpecificFeatureType("MoldShutOffSrf");

    /// <summary>
    /// Side core feature in mold tooling.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.ICoreFeatureData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.ICoreFeatureData.html">ICoreFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType SideCore = CreateSpecificFeatureType("SideCore");

    /// <summary>
    /// Transform stock feature for mold tooling.
    /// </summary>
    /// <remarks>
    /// This feature type does not have a corresponding SolidWorks API interface.
    /// </remarks>
    public static readonly SelectionType XFormStock = CreateSpecificFeatureType("XformStock");

    #endregion

    #region Motion and Simulation

    /// <summary>
    /// 3D contact element for motion analysis.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IMotionStudyResults"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IMotionStudyResults.html">IMotionStudyResults</seealso>
    /// </remarks>
    public static readonly SelectionType Contact3D = CreateSpecificFeatureType("AEM3DContact");

    /// <summary>
    /// Gravity force for motion analysis.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IMotionStudyResults"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IMotionStudyResults.html">IMotionStudyResults</seealso>
    /// </remarks>
    public static readonly SelectionType Gravity = CreateSpecificFeatureType("AEMGravity");

    /// <summary>
    /// Linear damper for motion analysis.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IMotionStudyResults"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IMotionStudyResults.html">IMotionStudyResults</seealso>
    /// </remarks>
    public static readonly SelectionType LinearDamper = CreateSpecificFeatureType("AEMLinearDamper");

    /// <summary>
    /// Linear motor for motion analysis.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IMotionStudyResults"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IMotionStudyResults.html">IMotionStudyResults</seealso>
    /// </remarks>
    public static readonly SelectionType LinearMotor = CreateSpecificFeatureType("AEMLinearMotor");

    /// <summary>
    /// Linear spring for motion analysis.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IMotionStudyResults"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IMotionStudyResults.html">IMotionStudyResults</seealso>
    /// </remarks>
    public static readonly SelectionType LinearSpring = CreateSpecificFeatureType("AEMLinearSpring");

    /// <summary>
    /// Rotational motor for motion analysis.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IMotionStudyResults"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IMotionStudyResults.html">IMotionStudyResults</seealso>
    /// </remarks>
    public static readonly SelectionType RotationalMotor = CreateSpecificFeatureType("AEMRotationalMotor");

    /// <summary>
    /// Torque force for motion analysis.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IMotionStudyResults"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IMotionStudyResults.html">IMotionStudyResults</seealso>
    /// </remarks>
    public static readonly SelectionType Torque = CreateSpecificFeatureType("AEMTorque");

    /// <summary>
    /// Torsional damper for motion analysis.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IMotionStudyResults"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IMotionStudyResults.html">IMotionStudyResults</seealso>
    /// </remarks>
    public static readonly SelectionType TorsionalDamper = CreateSpecificFeatureType("AEMTorsionalDamper");

    /// <summary>
    /// Torsional spring for motion analysis.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IMotionStudyResults"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IMotionStudyResults.html">IMotionStudyResults</seealso>
    /// </remarks>
    public static readonly SelectionType TorsionalSpring = CreateSpecificFeatureType("AEMTorsionalSpring");

    /// <summary>
    /// Simulation plot feature.
    /// </summary>
    /// <remarks>
    /// This feature type does not have a corresponding SolidWorks API interface.
    /// </remarks>
    public static readonly SelectionType SimulationPlot = CreateSpecificFeatureType("SimPlotFeature");

    /// <summary>
    /// Simulation plot X-axis feature.
    /// </summary>
    /// <remarks>
    /// This feature type does not have a corresponding SolidWorks API interface.
    /// </remarks>
    public static readonly SelectionType SimulationPlotXAxis = CreateSpecificFeatureType("SimPlotXAxisFeature");

    /// <summary>
    /// Simulation plot Y-axis feature.
    /// </summary>
    /// <remarks>
    /// This feature type does not have a corresponding SolidWorks API interface.
    /// </remarks>
    public static readonly SelectionType SimulationPlotYAxis = CreateSpecificFeatureType("SimPlotYAxisFeature");

    /// <summary>
    /// Simulation result folder in FeatureManager design tree.
    /// </summary>
    /// <remarks>
    /// This feature type does not have a corresponding SolidWorks API interface.
    /// </remarks>
    public static readonly SelectionType SimulationResultFolder = CreateSpecificFeatureType("SimResultFolder");

    #endregion

    #region Reference Geometry

    /// <summary>
    /// Bounding box reference geometry.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IBoundingBoxFeatureData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IBoundingBoxFeatureData.html">IBoundingBoxFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType BoundingBox = CreateSpecificFeatureType("BoundingBox");

    /// <summary>
    /// Center of mass reference point.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.ICenterOfMass"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.ICenterOfMass.html">ICenterOfMass</seealso>
    /// </remarks>
    public static readonly SelectionType CenterOfMass = CreateSpecificFeatureType("CenterOfMass");

    /// <summary>
    /// Coordinate system feature.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.ICoordinateSystemFeatureData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.ICoordinateSystemFeatureData.html">ICoordinateSystemFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType CoordinateSystem = CreateSpecificFeatureType("CoordSys");

    /// <summary>
    /// Ground plane reference geometry.
    /// </summary>
    /// <remarks>
    /// This feature type does not have a corresponding SolidWorks API interface.
    /// </remarks>
    public static readonly SelectionType GroundPlane = CreateSpecificFeatureType("GroundPlane");

    /// <summary>
    /// Reference axis feature.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IRefAxis"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IRefAxis.html">IRefAxis</seealso>
    /// </remarks>
    public static readonly SelectionType ReferenceAxis = CreateSpecificFeatureType("RefAxis");

    /// <summary>
    /// Reference plane feature.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IRefPlane"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IRefPlane.html">IRefPlane</seealso>
    /// </remarks>
    public static readonly SelectionType ReferencePlane = CreateSpecificFeatureType("RefPlane");

    #endregion

    #region Scenes Lights and Cameras

    /// <summary>
    /// Ambient light in scene.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IAmbientLight"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IAmbientLight.html">IAmbientLight</seealso>
    /// </remarks>
    public static readonly SelectionType AmbientLight = CreateSpecificFeatureType("AmbientLight");

    /// <summary>
    /// Camera feature.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.ICameraFeatureData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.ICameraFeatureData.html">ICameraFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType Camera = CreateSpecificFeatureType("CameraFeature");

    /// <summary>
    /// Directional light in scene.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IDirectionalLight"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IDirectionalLight.html">IDirectionalLight</seealso>
    /// </remarks>
    public static readonly SelectionType DirectionLight = CreateSpecificFeatureType("DirectionLight");

    /// <summary>
    /// Point light in scene.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IPointLight"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IPointLight.html">IPointLight</seealso>
    /// </remarks>
    public static readonly SelectionType PointLight = CreateSpecificFeatureType("PointLight");

    /// <summary>
    /// Spot light in scene.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.ISpotLight"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.ISpotLight.html">ISpotLight</seealso>
    /// </remarks>
    public static readonly SelectionType SpotLight = CreateSpecificFeatureType("SpotLight");

    #endregion

    #region Sheet Metal

    /// <summary>
    /// Sheet metal base flange feature.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IBaseFlangeFeatureData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IBaseFlangeFeatureData.html">IBaseFlangeFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType SheetMetalBaseFlange = CreateSpecificFeatureType("SMBaseFlange");

    /// <summary>
    /// Break corner feature.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IBreakCornerFeatureData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IBreakCornerFeatureData.html">IBreakCornerFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType BreakCorner = CreateSpecificFeatureType("BreakCorner");

    /// <summary>
    /// Corner trim feature (close corner).
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IClosedCornerFeatureData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IClosedCornerFeatureData.html">IClosedCornerFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType CornerTrim = CreateSpecificFeatureType("CornerTrim");

    /// <summary>
    /// Cross break feature.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.ICrossBrkFeatureData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.ICrossBrkFeatureData.html">ICrossBrkFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType CrossBreak = CreateSpecificFeatureType("CrossBreak");

    /// <summary>
    /// Edge flange feature.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IEdgeFlangeFeatureData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IEdgeFlangeFeatureData.html">IEdgeFlangeFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType EdgeFlange = CreateSpecificFeatureType("EdgeFlange");

    /// <summary>
    /// Flat pattern feature.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IFlatPatternFeatureData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IFlatPatternFeatureData.html">IFlatPatternFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType FlatPattern = CreateSpecificFeatureType("FlatPattern");

    /// <summary>
    /// Flatten bends feature (process-bends).
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IFlattenBendsFeatureData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IFlattenBendsFeatureData.html">IFlattenBendsFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType FlattenBends = CreateSpecificFeatureType("FlattenBends");

    /// <summary>
    /// Fold feature (unfold/fold).
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IUnFoldFeatureData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IUnFoldFeatureData.html">IUnFoldFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType Fold = CreateSpecificFeatureType("Fold");

    /// <summary>
    /// Forming tool instance feature.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IFormToolInstanceFeatureData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IFormToolInstanceFeatureData.html">IFormToolInstanceFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType FormToolInstance = CreateSpecificFeatureType("FormToolInstance");

    /// <summary>
    /// Hem feature.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IHemFeatureData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IHemFeatureData.html">IHemFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType Hem = CreateSpecificFeatureType("Hem");

    /// <summary>
    /// Jog feature.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IJogFeatureData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IJogFeatureData.html">IJogFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType Jog = CreateSpecificFeatureType("Jog");

    /// <summary>
    /// Lofted bend feature.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.ILoftedBendsFeatureData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.ILoftedBendsFeatureData.html">ILoftedBendsFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType LoftedBend = CreateSpecificFeatureType("LoftedBend");

    /// <summary>
    /// Normal cut feature in sheet metal.
    /// </summary>
    /// <remarks>
    /// NOTE: Official docs say "NormalCut" but actual API returns "SMNormalCut" (similar to docs mismatch for FilletCorner).
    /// <para>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.INormalCutFeatureData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.INormalCutFeatureData.html">INormalCutFeatureData</seealso>
    /// </para>
    /// </remarks>
    public static readonly SelectionType NormalCut = CreateSpecificFeatureType("SMNormalCut");

    /// <summary>
    /// One-bend feature (sketched bend).
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IOneBendFeatureData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IOneBendFeatureData.html">IOneBendFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType OneBend = CreateSpecificFeatureType("OneBend");

    /// <summary>
    /// Process bends feature.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IProcessBendsFeatureData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IProcessBendsFeatureData.html">IProcessBendsFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType ProcessBends = CreateSpecificFeatureType("ProcessBends");

    /// <summary>
    /// Sheet metal feature (base feature).
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.ISheetMetalFeatureData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.ISheetMetalFeatureData.html">ISheetMetalFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType SheetMetal = CreateSpecificFeatureType("SheetMetal");

    /// <summary>
    /// Sketch bend feature.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.ISketchedBendFeatureData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.ISketchedBendFeatureData.html">ISketchedBendFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType SketchBend = CreateSpecificFeatureType("SketchBend");

    /// <summary>
    /// 3D bend feature in sheet metal.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.ISM3dBendFeatureData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.ISM3dBendFeatureData.html">ISM3dBendFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType SheetMetal3DBend = CreateSpecificFeatureType("SM3dBend");

    /// <summary>
    /// Sheet metal gusset feature.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.ISMGussetFeatureData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.ISMGussetFeatureData.html">ISMGussetFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType SheetMetalGusset = CreateSpecificFeatureType("SMGusset");

    /// <summary>
    /// Mitered flange feature.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IMiterFlangeFeatureData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IMiterFlangeFeatureData.html">IMiterFlangeFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType SheetMetalMiteredFlange = CreateSpecificFeatureType("SMMiteredFlange");

    /// <summary>
    /// Convert to sheet metal feature.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.ISheetMetalFeatureData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.ISheetMetalFeatureData.html">ISheetMetalFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType SolidToSheetMetal = CreateSpecificFeatureType("SolidToSheetMetal");

    /// <summary>
    /// Template sheet metal feature.
    /// </summary>
    /// <remarks>
    /// This feature type does not have a corresponding SolidWorks API interface.
    /// </remarks>
    public static readonly SelectionType TemplateSheetMetal = CreateSpecificFeatureType("TemplateSheetMetal");

    /// <summary>
    /// Toroidal bend feature.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IToroidalBendFeatureData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IToroidalBendFeatureData.html">IToroidalBendFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType ToroidalBend = CreateSpecificFeatureType("ToroidalBend");

    /// <summary>
    /// Unfold feature.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IUnFoldFeatureData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IUnFoldFeatureData.html">IUnFoldFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType UnFold = CreateSpecificFeatureType("UnFold");

    #endregion

    #region Sketch

    /// <summary>
    /// 2D sketch feature.
    /// </summary>
    /// <remarks>
    /// Actually named "Sketch" in document.
    /// <para>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.ISketch"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.ISketch.html">ISketch</seealso>
    /// </para>
    /// </remarks>
    public static readonly SelectionType Sketch = CreateSpecificFeatureType("ProfileFeature");

    /// <summary>
    /// 3D sketch feature.
    /// </summary>
    /// <remarks>
    /// NOTE: Actually named "Sketch3D" in document, but feature type name is different.
    /// <para>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.ISketch"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.ISketch.html">ISketch</seealso>
    /// </para>
    /// </remarks>
    public static readonly SelectionType Sketch3D = CreateSpecificFeatureType("3DProfileFeature");

    /// <summary>
    /// 3D spline curve feature.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.ISketchSpline"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.ISketchSpline.html">ISketchSpline</seealso>
    /// </remarks>
    public static readonly SelectionType SplineCurve3D = CreateSpecificFeatureType("3DSplineCurve");

    /// <summary>
    /// Composite curve feature.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.ICurve"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.ICurve.html">ICurve</seealso>
    /// </remarks>
    public static readonly SelectionType CompositeCurve = CreateSpecificFeatureType("CompositeCurve");

    /// <summary>
    /// Imported curve feature.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.ICurve"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.ICurve.html">ICurve</seealso>
    /// </remarks>
    public static readonly SelectionType ImportedCurve = CreateSpecificFeatureType("ImportedCurve");

    /// <summary>
    /// Split line feature (projected curve).
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IProjectionCurveFeatureData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IProjectionCurveFeatureData.html">IProjectionCurveFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType SplitLine = CreateSpecificFeatureType("PLine");

    /// <summary>
    /// Reference curve feature.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.ICurve"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.ICurve.html">ICurve</seealso>
    /// </remarks>
    public static readonly SelectionType ReferenceCurve = CreateSpecificFeatureType("RefCurve");

    /// <summary>
    /// Reference point feature.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IRefPoint"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IRefPoint.html">IRefPoint</seealso>
    /// </remarks>
    public static readonly SelectionType ReferencePoint = CreateSpecificFeatureType("RefPoint");

    /// <summary>
    /// Sketch block definition.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.ISketchBlockDefinition"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.ISketchBlockDefinition.html">ISketchBlockDefinition</seealso>
    /// </remarks>
    public static readonly SelectionType SketchBlockDefinition = CreateSpecificFeatureType("SketchBlockDef");

    /// <summary>
    /// Sketch block instance.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.ISketchBlockInstance"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.ISketchBlockInstance.html">ISketchBlockInstance</seealso>
    /// </remarks>
    public static readonly SelectionType SketchBlockInstance = CreateSpecificFeatureType("SketchBlockInst");

    /// <summary>
    /// Sketch bitmap feature.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.ISketchPicture"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.ISketchPicture.html">ISketchPicture</seealso>
    /// </remarks>
    public static readonly SelectionType SketchBitmap = CreateSpecificFeatureType("SketchBitmap");

    #endregion

    #region Surface

    /// <summary>
    /// Loft surface feature.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.ILoftFeatureData"/>.
    /// SolidDNA wrapped feature: <see cref="FeatureLoftData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.ILoftFeatureData.html">ILoftFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType BlendReferenceSurface = CreateSpecificFeatureType("BlendRefSurface");

    /// <summary>
    /// Extend surface feature.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IExtendSurfaceFeatureData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IExtendSurfaceFeatureData.html">IExtendSurfaceFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType ExtendReferenceSurface = CreateSpecificFeatureType("ExtendRefSurface");

    /// <summary>
    /// Extruded surface feature.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IExtrudeFeatureData2"/>.
    /// SolidDNA wrapped feature: <see cref="FeatureExtrudeData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IExtrudeFeatureData2.html">IExtrudeFeatureData2</seealso>
    /// </remarks>
    public static readonly SelectionType ExtrudeReferenceSurface = CreateSpecificFeatureType("ExtruRefSurface");

    /// <summary>
    /// Filled surface feature.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IFillSurfaceFeatureData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IFillSurfaceFeatureData.html">IFillSurfaceFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType FillReferenceSurface = CreateSpecificFeatureType("FillRefSurface");

    /// <summary>
    /// Flatten surface feature.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IFlattenSurfaceFeatureData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IFlattenSurfaceFeatureData.html">IFlattenSurfaceFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType FlattenSurface = CreateSpecificFeatureType("FlattenSurface");

    /// <summary>
    /// Mid surface feature.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IMidSurfaceFeatureData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IMidSurfaceFeatureData.html">IMidSurfaceFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType MidReferenceSurface = CreateSpecificFeatureType("MidRefSurface");

    /// <summary>
    /// Offset surface feature.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IOffsetSurfaceFeatureData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IOffsetSurfaceFeatureData.html">IOffsetSurfaceFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType OffsetReferenceSurface = CreateSpecificFeatureType("OffsetRefSuface");

    /// <summary>
    /// Planar surface feature.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IPlanarSurfaceFeatureData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IPlanarSurfaceFeatureData.html">IPlanarSurfaceFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType PlanarSurface = CreateSpecificFeatureType("PlanarSurface");

    /// <summary>
    /// Radiate surface feature.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IRadiateSurfaceFeatureData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IRadiateSurfaceFeatureData.html">IRadiateSurfaceFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType RadiateReferenceSurface = CreateSpecificFeatureType("RadiateRefSurface");

    /// <summary>
    /// Reference surface feature.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.ISurface"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.ISurface.html">ISurface</seealso>
    /// </remarks>
    public static readonly SelectionType ReferenceSurface = CreateSpecificFeatureType("RefSurface");

    /// <summary>
    /// Revolved surface feature.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IRevolveFeatureData2"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IRevolveFeatureData2.html">IRevolveFeatureData2</seealso>
    /// </remarks>
    public static readonly SelectionType RevolveReferenceSurface = CreateSpecificFeatureType("RevolvRefSurf");

    /// <summary>
    /// Ruled surface from edge feature.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IRuledSurfaceFeatureData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IRuledSurfaceFeatureData.html">IRuledSurfaceFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType RuledSurfaceFromEdge = CreateSpecificFeatureType("RuledSrfFromEdge");

    /// <summary>
    /// Knit surface feature (sew surfaces).
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IKnitSurfaceFeatureData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IKnitSurfaceFeatureData.html">IKnitSurfaceFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType SewReferenceSurface = CreateSpecificFeatureType("SewRefSurface");

    /// <summary>
    /// Surface cut feature.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.ISurfaceCutFeatureData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.ISurfaceCutFeatureData.html">ISurfaceCutFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType SurfaceCut = CreateSpecificFeatureType("SurfCut");

    /// <summary>
    /// Swept surface feature.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.ISweepFeatureData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.ISweepFeatureData.html">ISweepFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType SweepReferenceSurface = CreateSpecificFeatureType("SweepRefSurface");

    /// <summary>
    /// Trim surface feature.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.ITrimSurfaceFeatureData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.ITrimSurfaceFeatureData.html">ITrimSurfaceFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType TrimReferenceSurface = CreateSpecificFeatureType("TrimRefSurface");

    /// <summary>
    /// Untrim surface feature.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IUntrimSurfaceFeatureData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IUntrimSurfaceFeatureData.html">IUntrimSurfaceFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType UntrimReferenceSurface = CreateSpecificFeatureType("UnTrimRefSurf");

    #endregion

    #region Weldment and Structure System

    /// <summary>
    /// End cap feature for weldment.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IEndCapFeatureData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IEndCapFeatureData.html">IEndCapFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType EndCap = CreateSpecificFeatureType("EndCap");

    /// <summary>
    /// Structure system secondary member between points.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IStructuralMemberFeatureData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IStructuralMemberFeatureData.html">IStructuralMemberFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType SecondaryMemberBetweenPoints = CreateSpecificFeatureType("StrctSysBtwPtsMbrFeat");

    /// <summary>
    /// Structure system corner member.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IStructuralMemberFeatureData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IStructuralMemberFeatureData.html">IStructuralMemberFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType CornerMember = CreateSpecificFeatureType("StrctSysCnrFeat");

    /// <summary>
    /// Structure system corner treatment group folder.
    /// </summary>
    /// <remarks>
    /// This feature type does not have a corresponding SolidWorks API interface.
    /// </remarks>
    public static readonly SelectionType CornerTreatmentGroupFolder = CreateSpecificFeatureType("StrctSysCnrGrpFeat");

    /// <summary>
    /// Structure system corner management folder.
    /// </summary>
    /// <remarks>
    /// This feature type does not have a corresponding SolidWorks API interface.
    /// </remarks>
    public static readonly SelectionType CornerManagementFolder = CreateSpecificFeatureType("StrctSysCnrMgmtFeat");

    /// <summary>
    /// Structure system folder in FeatureManager design tree.
    /// </summary>
    /// <remarks>
    /// This feature type does not have a corresponding SolidWorks API interface.
    /// </remarks>
    public static readonly SelectionType StructureSystemFolder = CreateSpecificFeatureType("StrctSysFeat");

    /// <summary>
    /// Structure system profile group folder.
    /// </summary>
    /// <remarks>
    /// This feature type does not have a corresponding SolidWorks API interface.
    /// </remarks>
    public static readonly SelectionType ProfileGroupFolder = CreateSpecificFeatureType("StrctSysGrpFeat");

    /// <summary>
    /// Structure system primary member path segment.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IStructuralMemberFeatureData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IStructuralMemberFeatureData.html">IStructuralMemberFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType PrimaryMemberPathSegment = CreateSpecificFeatureType("StrctSysPathSegMbrFeat");

    /// <summary>
    /// Structure system secondary member up to members.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IStructuralMemberFeatureData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IStructuralMemberFeatureData.html">IStructuralMemberFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType SecondaryMemberUpToMembers = CreateSpecificFeatureType("StrctSysPtToMem");

    /// <summary>
    /// Structure system primary member reference plane.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IStructuralMemberFeatureData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IStructuralMemberFeatureData.html">IStructuralMemberFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType PrimaryMemberReferencePlane = CreateSpecificFeatureType("StrctSysRefPlnMbrFeat");

    /// <summary>
    /// Structure system primary member sketch point/length.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IStructuralMemberFeatureData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IStructuralMemberFeatureData.html">IStructuralMemberFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType PrimaryMemberPointLength = CreateSpecificFeatureType("StrctSysSkPtLenMbrFeat");

    /// <summary>
    /// Structure system secondary member support plane.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IStructuralMemberFeatureData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IStructuralMemberFeatureData.html">IStructuralMemberFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType SecondaryMemberSupportPlane = CreateSpecificFeatureType("StrctSysSupPlnMbrFeat");

    /// <summary>
    /// Structure system primary member face/plane intersection.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IStructuralMemberFeatureData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IStructuralMemberFeatureData.html">IStructuralMemberFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType PrimaryMemberFacePlaneIntersection = CreateSpecificFeatureType("StrctSysSurfPlnMbrFeat");

    /// <summary>
    /// Advanced structure member feature.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IStructuralMemberFeatureData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IStructuralMemberFeatureData.html">IStructuralMemberFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType AdvancedStructureMember = CreateSpecificFeatureType("AdvStructMember");

    /// <summary>
    /// Gusset feature for weldment.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IGussetFeatureData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IGussetFeatureData.html">IGussetFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType Gusset = CreateSpecificFeatureType("Gusset");

    /// <summary>
    /// Weld bead feature.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IWeldBeadFeatureData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IWeldBeadFeatureData.html">IWeldBeadFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType WeldBead = CreateSpecificFeatureType("WeldBeadFeat");

    /// <summary>
    /// Weld corner feature (weld gap).
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IWeldmentGapFeatureData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IWeldmentGapFeatureData.html">IWeldmentGapFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType WeldCorner = CreateSpecificFeatureType("WeldCornerFeat");

    /// <summary>
    /// Weld member feature (structural member).
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IStructuralMemberFeatureData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IStructuralMemberFeatureData.html">IStructuralMemberFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType WeldMember = CreateSpecificFeatureType("WeldMemberFeat");

    /// <summary>
    /// Weldment feature (base feature).
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IWeldmentFeatureData"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IWeldmentFeatureData.html">IWeldmentFeatureData</seealso>
    /// </remarks>
    public static readonly SelectionType Weld = CreateSpecificFeatureType("WeldmentFeature");

    /// <summary>
    /// Weld table feature in drawing.
    /// </summary>
    /// <remarks>
    /// Corresponding feature SW API interface: <seealso cref="SolidWorks.Interop.sldworks.IWeldmentCutListFeature"/>.
    /// SOLIDWORKS API Help: <seealso href="https://help.solidworks.com/2026/english/api/sldworksapi/SolidWorks.Interop.sldworks~SolidWorks.Interop.sldworks.IWeldmentCutListFeature.html">IWeldmentCutListFeature</seealso>
    /// </remarks>
    public static readonly SelectionType WeldTable = CreateSpecificFeatureType("WeldmentTableFeat");

    #endregion

    public static SelectionType CreateSpecificFeatureType(string specificFeatureType) => new SelectionType(swSelectType_e.swSelNOTHING, specificFeatureType, true);
}
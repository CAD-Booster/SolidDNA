using SolidWorks.Interop.sldworks;
using System;
using System.Collections.Generic;

namespace CADBooster.SolidDna
{
    /// <summary>
    /// Interface for a SolidWorks feature.
    /// Enables mocking for unit testing code that consumes features.
    /// </summary>
    public interface IModelFeature : IDisposable
    {
        #region Properties

        /// <summary>
        /// The specific type of this feature
        /// </summary>
        ModelFeatureType FeatureType { get; }

        /// <summary>
        /// Gets the SolidWorks feature type name, such as RefSurface, CosmeticWeldBead, FeatSurfaceBodyFolder etc...
        /// </summary>
        string FeatureTypeName { get; }

        /// <summary>
        /// Gets or sets the SolidWorks feature name, such as Sketch1
        /// </summary>
        string FeatureName { get; set; }

        /// <summary>
        /// The specific feature for this feature, if any.
        /// NOTE: This is a COM object. Set all instance variables of this to null once done if you set any
        /// </summary>
        object SpecificFeature { get; }

        /// <summary>
        /// The feature data for this feature, if any.
        /// NOTE: This is a COM object. Set all instance variables of this to null once done if you set any
        /// </summary>
        object FeatureData { get; }

        /// <summary>
        /// The raw underlying COM object.
        /// WARNING: Use with caution.
        /// </summary>
        Feature UnsafeObject { get; }

        #region Type Checks - Features

        /// <summary>
        /// Checks if this feature's specific type is an Attribute
        /// </summary>
        bool IsAttribute { get; }

        /// <summary>
        /// Checks if this feature's specific type is a Body Folder
        /// </summary>
        bool IsBodyFolder { get; }

        /// <summary>
        /// Checks if this feature's specific type is a BOM
        /// </summary>
        bool IsBom { get; }

        /// <summary>
        /// Checks if this feature's specific type is a Camera
        /// </summary>
        bool IsCamera { get; }

        /// <summary>
        /// Checks if this feature's specific type is a Comment Folder
        /// </summary>
        bool IsCommentFolder { get; }

        /// <summary>
        /// Checks if this feature's specific type is a Component
        /// </summary>
        bool IsComponent { get; }

        /// <summary>
        /// Checks if this feature's specific type is a Cosmetic Weld Bead Folder 
        /// </summary>
        bool IsCosmeticWeldBeadFolder { get; }

        /// <summary>
        /// Checks if this feature's specific type is a Detail Circle
        /// </summary>
        bool IsDetailCircle { get; }

        /// <summary>
        /// Checks if this feature's specific type is a Section view
        /// </summary>
        bool IsDrSection { get; }

        /// <summary>
        /// Checks if this feature's specific type is a Feature Folder
        /// </summary>
        bool IsFeatureFolder { get; }

        /// <summary>
        /// Checks if this feature's specific type is a Flat Pattern Folder
        /// </summary>
        bool IsFlatPatternFolder { get; }

        /// <summary>
        /// Checks if this feature's specific type is a Light
        /// </summary>
        bool IsLight { get; }

        /// <summary>
        /// Checks if this feature's specific type is a Mate 
        /// </summary>
        bool IsMate { get; }

        /// <summary>
        /// Checks if this feature's specific type is a Mate Group
        /// </summary>
        bool IsMateGroup { get; }

        /// <summary>
        /// Checks if this feature's specific type is a Mate Reference 
        /// </summary>
        bool IsMateReference { get; }

        /// <summary>
        /// Checks if this feature's specific type is a Motion Study Results
        /// </summary>
        bool IsMotionStudyResults { get; }

        /// <summary>
        /// Checks if this feature's specific type is a Ref Axis
        /// </summary>
        bool IsReferenceAxis { get; }

        /// <summary>
        /// Checks if this feature's specific type is a Reference Curve
        /// </summary>
        bool IsReferenceCurve { get; }

        /// <summary>
        /// Checks if this feature's specific type is a Reference Plane
        /// </summary>
        bool IsReferencePlane { get; }

        /// <summary>
        /// Checks if this feature's specific type is a Reference Point
        /// </summary>
        bool IsReferencePoint { get; }

        /// <summary>
        /// Checks if this feature's specific type is a Sensor 
        /// </summary>
        bool IsSensor { get; }

        /// <summary>
        /// Checks if this feature's specific type is a Sheet Metal Folder
        /// </summary>
        bool IsSheetMetalFolder { get; }

        /// <summary>
        /// Checks if this feature's specific type is a Sketch
        /// </summary>
        bool IsSketch { get; }

        /// <summary>
        /// Checks if this feature's specific type is a Sketch Block Definition
        /// </summary>
        bool IsSketchBlockDefinition { get; }

        /// <summary>
        /// Checks if this feature's specific type is a Sketch Block Instance 
        /// </summary>
        bool IsSketchBlockInstance { get; }

        /// <summary>
        /// Checks if this feature's specific type is a Sketch Picture
        /// </summary>
        bool IsSketchPicture { get; }

        /// <summary>
        /// Checks if this feature's specific type is a Surface Mid
        /// </summary>
        bool IsSurfaceMid { get; }

        /// <summary>
        /// Checks if this feature's specific type is a Table Anchor
        /// </summary>
        bool IsTableAnchor { get; }

        /// <summary>
        /// Checks if this feature's specific type is a Weldment
        /// </summary>
        bool IsWeldment { get; }

        /// <summary>
        /// Checks if this feature's specific type is a Weld Table 
        /// </summary>
        bool IsWeldTable { get; }

        #endregion

        #region Type Checks - Feature Data

        /// <summary>
        /// Checks if this feature's specific type is Advanced Hole Wizard data
        /// </summary>
        bool IsAdvancedHoleWizardData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Angle Mate data
        /// </summary>
        bool IsAngleMateData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Base Flange data
        /// </summary>
        bool IsBaseFlangeData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Bends data
        /// </summary>
        bool IsBendsData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Boundary Boss data
        /// </summary>
        bool IsBoundaryBossData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Bounding Box data
        /// </summary>
        bool IsBoundingBoxData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Break Corner data
        /// </summary>
        bool IsBreakCornerData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Broken Out Section data
        /// </summary>
        bool IsBrokenOutSectionData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Cam Follower Mate data
        /// </summary>
        bool IsCamFollowerMateData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Cavity data
        /// </summary>
        bool IsCavityData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Chain Pattern data
        /// </summary>
        bool IsChainPatternData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Chamfer data
        /// </summary>
        bool IsChamferData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Circular Pattern data
        /// </summary>
        bool IsCircularPatternData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Coincident Mate data
        /// </summary>
        bool IsCoincidentMateData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Combine Bodies data
        /// </summary>
        bool IsCombineBodiesData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Composite Curve data
        /// </summary>
        bool IsCompositeCurveData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Concentric Mate data
        /// </summary>
        bool IsConcentricMateData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Coordinate System data
        /// </summary>
        bool IsCoordinateSystemData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Core data
        /// </summary>
        bool IsCoreData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Cosmetic Thread data
        /// </summary>
        bool IsCosmeticThreadData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Cosmetic Weld Bead data
        /// </summary>
        bool IsCosmeticWeldBeadData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Cross Break data
        /// </summary>
        bool IsCrossBreakData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Curve Driven Pattern data
        /// </summary>
        bool IsCurveDrivenPatternData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Delete Body data
        /// </summary>
        bool IsDeleteBodyData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Delete Face data
        /// </summary>
        bool IsDeleteFaceData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Derived Part data
        /// </summary>
        bool IsDerivedPartData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Derived Pattern data
        /// </summary>
        bool IsDerivedPatternData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Dim Pattern data
        /// </summary>
        bool IsDimPatternData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Distance Mate data
        /// </summary>
        bool IsDistanceMateData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Dome data
        /// </summary>
        bool IsDomeData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Draft data
        /// </summary>
        bool IsDraftData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Edge Flange data
        /// </summary>
        bool IsEdgeFlangeData { get; }

        /// <summary>
        /// Checks if this feature's specific type is End Cap data
        /// </summary>
        bool IsEndCapData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Extrude data
        /// </summary>
        bool IsExtrudeData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Fill Pattern data
        /// </summary>
        bool IsFillPatternData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Flat Pattern data
        /// </summary>
        bool IsFlatPatternData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Folds data
        /// </summary>
        bool IsFoldsData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Free Point Curve data
        /// </summary>
        bool IsFreePointCurveData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Gear Mate data
        /// </summary>
        bool IsGearMateData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Ground Plane data
        /// </summary>
        bool IsGroundPlaneData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Gusset data
        /// </summary>
        bool IsGussetData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Heal Edges data
        /// </summary>
        bool IsHealEdgesData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Helix data
        /// </summary>
        bool IsHelixData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Hem data
        /// </summary>
        bool IsHemData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Hinge Mate data
        /// </summary>
        bool IsHingeMateData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Hole Series data
        /// </summary>
        bool IsHoleSeriesData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Hole Wizard data
        /// </summary>
        bool IsHoleWizardData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Import 3D Interconnect data
        /// </summary>
        bool IsImport3DInterconnectData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Imported Curve data
        /// </summary>
        bool IsImportedCurveData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Indent data
        /// </summary>
        bool IsIndentData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Intersect data
        /// </summary>
        bool IsIntersectData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Jog data
        /// </summary>
        bool IsJogData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Library Feature data
        /// </summary>
        bool IsLibraryFeatureData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Linear Coupler Mate data
        /// </summary>
        bool IsLinearCouplerMateData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Linear Pattern data
        /// </summary>
        bool IsLinearPatternData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Local Circular Pattern data
        /// </summary>
        bool IsLocalCircularPatternData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Local Curve Pattern data
        /// </summary>
        bool IsLocalCurvePatternData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Local Linear Pattern data
        /// </summary>
        bool IsLocalLinearPatternData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Local Sketch Pattern data
        /// </summary>
        bool IsLocalSketchPatternData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Lock Mate data
        /// </summary>
        bool IsLockMateData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Loft data
        /// </summary>
        bool IsLoftData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Lofted Bend data
        /// </summary>
        bool IsLoftedBendData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Macro data
        /// </summary>
        bool IsMacroData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Mirror Component data
        /// </summary>
        bool IsMirrorComponentData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Mirror Part data
        /// </summary>
        bool IsMirrorPartData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Mirror Pattern data
        /// </summary>
        bool IsMirrorPatternData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Mirror Solid data
        /// </summary>
        bool IsMirrorSolidData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Miter Flange data
        /// </summary>
        bool IsMiterFlangeData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Motion Plot Axis data
        /// </summary>
        bool IsMotionPlotAxisData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Motion Plot data
        /// </summary>
        bool IsMotionPlotData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Move Copy Body data
        /// </summary>
        bool IsMoveCopyBodyData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Normal Cut data
        /// </summary>
        bool IsNormalCutData { get; }

        /// <summary>
        /// Checks if this feature's specific type is One Bend data
        /// </summary>
        bool IsOneBendData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Parallel Mate data
        /// </summary>
        bool IsParallelMateData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Parting Line data
        /// </summary>
        bool IsPartingLineData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Parting Surface data
        /// </summary>
        bool IsPartingSurfaceData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Perpendicular Mate data
        /// </summary>
        bool IsPerpendicularMateData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Profile Center Mate data
        /// </summary>
        bool IsProfileCenterData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Projection Curve data
        /// </summary>
        bool IsProjectionCurveData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Rack Pinion Mate data
        /// </summary>
        bool IsRackPinionMateData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Reference Axis data
        /// </summary>
        bool IsReferenceAxisData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Reference Plane data
        /// </summary>
        bool IsReferencePlaneData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Reference Point Curve data
        /// </summary>
        bool IsReferencePointCurveData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Reference Point data
        /// </summary>
        bool IsReferencePointData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Replace Face data
        /// </summary>
        bool IsReplaceFaceData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Revolve data
        /// </summary>
        bool IsRevolveData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Rib data
        /// </summary>
        bool IsRibData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Rip data
        /// </summary>
        bool IsRipData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Save Body data
        /// </summary>
        bool IsSaveBodyData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Scale data
        /// </summary>
        bool IsScaleData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Screw Mate data
        /// </summary>
        bool IsScrewMateData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Sheet Metal data
        /// </summary>
        bool IsSheetMetalData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Sheet Metal Gusset data
        /// </summary>
        bool IsSheetMetalGussetData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Shell data
        /// </summary>
        bool IsShellData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Shut Off Surface data
        /// </summary>
        bool IsShutOffSurfaceData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Simple Fillet data
        /// </summary>
        bool IsSimpleFilletData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Simple Hole data
        /// </summary>
        bool IsSimpleHoleData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Simulation 3D Contact data
        /// </summary>
        bool IsSimulation3DContactData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Simulation Damper data
        /// </summary>
        bool IsSimulationDamperData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Simulation Force data
        /// </summary>
        bool IsSimulationForceData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Simulation Gravity data
        /// </summary>
        bool IsSimulationGravityData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Simulation Linear Spring data
        /// </summary>
        bool IsSimulationLinearSpringData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Simulation Motor data
        /// </summary>
        bool IsSimulationMotorData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Sketched Bend data
        /// </summary>
        bool IsSketchedBendData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Sketch Pattern data
        /// </summary>
        bool IsSketchPatternData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Slot Mate data
        /// </summary>
        bool IsSlotMateData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Smart Component data
        /// </summary>
        bool IsSmartComponentData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Split Body data
        /// </summary>
        bool IsSplitBodyData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Split Line data
        /// </summary>
        bool IsSplitLineData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Surface Cut data
        /// </summary>
        bool IsSurfaceCutData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Surface Extend data
        /// </summary>
        bool IsSurfaceExtendData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Surface Extrude data
        /// </summary>
        bool IsSurfaceExtrudeData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Surface Fill data
        /// </summary>
        bool IsSurfaceFillData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Surface Flatten data
        /// </summary>
        bool IsSurfaceFlattenData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Surface Knit data
        /// </summary>
        bool IsSurfaceKnitData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Surface Offset data
        /// </summary>
        bool IsSurfaceOffsetData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Surface Planar data
        /// </summary>
        bool IsSurfacePlanarData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Surface Radiate data
        /// </summary>
        bool IsSurfaceRadiateData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Surface Revolve data
        /// </summary>
        bool IsSurfaceRevolveData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Surface Ruled data
        /// </summary>
        bool IsSurfaceRuledData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Surface Sweep data
        /// </summary>
        bool IsSurfaceSweepData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Surface Trim data
        /// </summary>
        bool IsSurfaceTrimData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Sweep data
        /// </summary>
        bool IsSweepData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Symmetric Mate data
        /// </summary>
        bool IsSymmetricMateData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Table Pattern data
        /// </summary>
        bool IsTablePatternData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Tangent Mate data
        /// </summary>
        bool IsTangentMateData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Thicken data
        /// </summary>
        bool IsThickenData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Thread data
        /// </summary>
        bool IsThreadData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Tooling Split data
        /// </summary>
        bool IsToolingSplitData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Universal Joint Mate data
        /// </summary>
        bool IsUniversalJointMateData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Variable Fillet data
        /// </summary>
        bool IsVariableFilletData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Weldment Bead data
        /// </summary>
        bool IsWeldmentBeadData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Weldment Cut List data
        /// </summary>
        bool IsWeldmentCutListData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Weldment Member data
        /// </summary>
        bool IsWeldmentMemberData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Weldment Trim Extend data
        /// </summary>
        bool IsWeldmentTrimExtendData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Width Mate data
        /// </summary>
        bool IsWidthMateData { get; }

        /// <summary>
        /// Checks if this feature's specific type is Wrap Sketch data
        /// </summary>
        bool IsWrapSketchData { get; }

        #endregion

        #endregion

        #region Methods

        /// <summary>
        /// Get a list of all features that are required to create this feature.
        /// </summary>
        /// <returns></returns>
        List<ModelFeature> GetParents();

        /// <summary>
        /// Get a list of all child features.
        /// </summary>
        /// <returns></returns>
        List<ModelFeature> GetChildren();

        /// <summary>
        /// Sets the suppression state of this feature
        /// </summary>
        /// <param name="state">Suppression state as defined in <see cref="ModelFeatureSuppressionState"/></param>
        /// <param name="configurationOption">Configuration option as defined in <see cref="ModelConfigurationOptions"/></param>
        /// <param name="configurationNames">Array of configuration names; valid only if configurationOption set to <see cref="ModelConfigurationOptions.SpecificConfiguration"/></param>
        /// <returns>True if operation was successful</returns>
        bool SetSuppressionState(ModelFeatureSuppressionState state, ModelConfigurationOptions configurationOption, string[] configurationNames = null);

        /// <summary>
        /// Gets whether the feature in the specified configurations is suppressed
        /// </summary>
        /// <param name="configurationOption">Configuration option as defined in <see cref="ModelConfigurationOptions"/></param>
        /// <param name="configurationNames">Array of configuration names</param>
        /// <returns>Array of Booleans indicating the suppression states for the feature in the specified configurations</returns>
        bool[] IsSuppressed(ModelConfigurationOptions configurationOption, string[] configurationNames = null);

        /// <summary>
        /// Gets a custom property editor for this feature.
        /// Throws an error when the feature is not a cut list folder or the Weldment feature.
        /// </summary>
        CustomPropertyEditor GetCustomPropertyEditor();

        /// <summary>
        /// Gets all the custom properties in this feature.
        /// Only works for Cut List Folders and the Weldment feature.
        /// </summary>
        /// <param name="action">The custom properties list to be worked on inside the action.</param>
        void CustomProperties(Action<List<CustomProperty>> action);

        /// <summary>
        /// Deletes a custom property by the given name
        /// </summary>
        /// <param name="name">The name of the custom property</param>
        void DeleteCustomProperty(string name);

        /// <summary>
        /// Gets a custom property by the given name. 
        /// Only works for Cut List Folders and the Weldment feature.
        /// </summary>
        /// <param name="name">The name of the custom property</param>
        /// <param name="resolved">True to get the resolved value of the property, false to get the actual text</param>
        /// <returns></returns>
        string GetCustomProperty(string name, bool resolved = false);

        /// <summary>
        /// Sets a custom property to the given value.
        /// Only works for Cut List Folders and the Weldment feature.
        /// </summary>
        /// <param name="name">The name of the property</param>
        /// <param name="value">The value of the property</param>
        void SetCustomProperty(string name, string value);

        #endregion
    }
}

using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.Linq;

namespace CADBooster.SolidDna
{
    /// <summary>
    /// An object that combines the enum and string that the SolidWorks API needs to select objects.
    /// Cast it to an integer or string when calling a SolidWorks API.
    /// Source: https://help.solidworks.com/2025/english/api/swconst/SolidWorks.Interop.swconst~SolidWorks.Interop.swconst.swSelectType_e.html
    /// </summary>
    public class SelectionType
    {
        #region Static values

        /// <summary>
        /// Everything selection.
        /// </summary>
        public static readonly SelectionType Everything = new SelectionType(swSelectType_e.swSelEVERYTHING, "EVERYTHING");

        /// <summary>
        /// Location selection.
        /// </summary>
        public static readonly SelectionType Location = new SelectionType(swSelectType_e.swSelLOCATIONS, "LOCATIONS");

        /// <summary>
        /// Unsupported selection.
        /// </summary>
        public static readonly SelectionType Unsupported = new SelectionType(swSelectType_e.swSelUNSUPPORTED, "UNSUPPORTED");

        /// <summary>
        /// Nothing selected.
        /// </summary>
        public static readonly SelectionType Nothing = new SelectionType(swSelectType_e.swSelNOTHING, string.Empty);

        /// <summary>
        /// Edge selection.
        /// </summary>
        public static readonly SelectionType Edge = new SelectionType(swSelectType_e.swSelEDGES, "EDGE");

        /// <summary>
        /// Face selection.
        /// </summary>
        public static readonly SelectionType Face = new SelectionType(swSelectType_e.swSelFACES, "FACE");

        /// <summary>
        /// Vertex selection.
        /// </summary>
        public static readonly SelectionType Vertex = new SelectionType(swSelectType_e.swSelVERTICES, "VERTEX");

        /// <summary>
        /// Datum plane selection.
        /// </summary>
        public static readonly SelectionType DatumPlane = new SelectionType(swSelectType_e.swSelDATUMPLANES, "PLANE");

        /// <summary>
        /// Datum axis selection.
        /// </summary>
        public static readonly SelectionType DatumAxis = new SelectionType(swSelectType_e.swSelDATUMAXES, "AXIS");

        /// <summary>
        /// Datum point selection.
        /// </summary>
        public static readonly SelectionType DatumPoint = new SelectionType(swSelectType_e.swSelDATUMPOINTS, "DATUMPOINT");

        /// <summary>
        /// OLE item selection.
        /// </summary>
        public static readonly SelectionType OleItem = new SelectionType(swSelectType_e.swSelOLEITEMS, "OLEITEM");

        /// <summary>
        /// Attribute selection.
        /// </summary>
        public static readonly SelectionType Attribute = new SelectionType(swSelectType_e.swSelATTRIBUTES, "ATTRIBUTE");

        /// <summary>
        /// Sketch selection.
        /// </summary>
        public static readonly SelectionType Sketch = new SelectionType(swSelectType_e.swSelSKETCHES, "SKETCH");

        /// <summary>
        /// Sketch segment selection.
        /// </summary>
        public static readonly SelectionType SketchSegment = new SelectionType(swSelectType_e.swSelSKETCHSEGS, "SKETCHSEGMENT");

        /// <summary>
        /// Sketch point selection.
        /// </summary>
        public static readonly SelectionType SketchPoint = new SelectionType(swSelectType_e.swSelSKETCHPOINTS, "SKETCHPOINT");

        /// <summary>
        /// Drawing view selection.
        /// </summary>
        public static readonly SelectionType DrawingView = new SelectionType(swSelectType_e.swSelDRAWINGVIEWS, "DRAWINGVIEW");

        /// <summary>
        /// Geometric tolerance selection.
        /// </summary>
        public static readonly SelectionType GeometricTolerance = new SelectionType(swSelectType_e.swSelGTOLS, "GTOL");

        /// <summary>
        /// Dimension selection.
        /// </summary>
        public static readonly SelectionType Dimension = new SelectionType(swSelectType_e.swSelDIMENSIONS, "DIMENSION");

        /// <summary>
        /// Note selection.
        /// </summary>
        public static readonly SelectionType Note = new SelectionType(swSelectType_e.swSelNOTES, "NOTE");

        /// <summary>
        /// Section line selection.
        /// </summary>
        public static readonly SelectionType SectionLine = new SelectionType(swSelectType_e.swSelSECTIONLINES, "SECTIONLINE");

        /// <summary>
        /// Detail circle selection.
        /// </summary>
        public static readonly SelectionType DetailCircle = new SelectionType(swSelectType_e.swSelDETAILCIRCLES, "DETAILCIRCLE");

        /// <summary>
        /// Section text selection.
        /// </summary>
        public static readonly SelectionType SectionText = new SelectionType(swSelectType_e.swSelSECTIONTEXT, "SECTIONTEXT");

        /// <summary>
        /// Sheet selection.
        /// </summary>
        public static readonly SelectionType Sheet = new SelectionType(swSelectType_e.swSelSHEETS, "SHEET");

        /// <summary>
        /// Component selection.
        /// </summary>
        public static readonly SelectionType Component = new SelectionType(swSelectType_e.swSelCOMPONENTS, "COMPONENT");

        /// <summary>
        /// Mate selection.
        /// </summary>
        public static readonly SelectionType Mate = new SelectionType(swSelectType_e.swSelMATES, "MATE");

        /// <summary>
        /// Body feature selection.
        /// </summary>
        public static readonly SelectionType BodyFeature = new SelectionType(swSelectType_e.swSelBODYFEATURES, "BODYFEATURE");

        /// <summary>
        /// Reference curve selection.
        /// </summary>
        public static readonly SelectionType ReferenceCurve = new SelectionType(swSelectType_e.swSelREFCURVES, "REFCURVE");

        /// <summary>
        /// External sketch segment selection.
        /// </summary>
        public static readonly SelectionType ExternalSketchSegment = new SelectionType(swSelectType_e.swSelEXTSKETCHSEGS, "EXTSKETCHSEGMENT");

        /// <summary>
        /// External sketch point selection.
        /// </summary>
        public static readonly SelectionType ExternalSketchPoint = new SelectionType(swSelectType_e.swSelEXTSKETCHPOINTS, "EXTSKETCHPOINT");

        /// <summary>
        /// Helix selection.
        /// Note: The enum value 26 is also used for <see cref="swSelectType_e.swSelREFERENCECURVES"/>.
        /// </summary>
        public static readonly SelectionType Helix = new SelectionType(swSelectType_e.swSelHELIX, "HELIX");

        /// <summary>
        /// Reference surface selection.
        /// </summary>
        public static readonly SelectionType ReferenceSurface = new SelectionType(swSelectType_e.swSelREFSURFACES, "REFSURFACE");

        /// <summary>
        /// Center mark selection.
        /// </summary>
        public static readonly SelectionType CenterMark = new SelectionType(swSelectType_e.swSelCENTERMARKS, "CENTERMARKS");

        /// <summary>
        /// In-context feature selection.
        /// </summary>
        public static readonly SelectionType InContextFeature = new SelectionType(swSelectType_e.swSelINCONTEXTFEAT, "INCONTEXTFEAT");

        /// <summary>
        /// Mate group selection.
        /// </summary>
        public static readonly SelectionType MateGroup = new SelectionType(swSelectType_e.swSelMATEGROUP, "MATEGROUP");

        /// <summary>
        /// Break line selection.
        /// </summary>
        public static readonly SelectionType BreakLine = new SelectionType(swSelectType_e.swSelBREAKLINES, "BREAKLINE");

        /// <summary>
        /// In-context features selection.
        /// </summary>
        public static readonly SelectionType InContextFeatures = new SelectionType(swSelectType_e.swSelINCONTEXTFEATS, "INCONTEXTFEATS");

        /// <summary>
        /// Mate groups selection.
        /// </summary>
        public static readonly SelectionType MateGroups = new SelectionType(swSelectType_e.swSelMATEGROUPS, "MATEGROUPS");

        /// <summary>
        /// Sketch text selection.
        /// </summary>
        public static readonly SelectionType SketchText = new SelectionType(swSelectType_e.swSelSKETCHTEXT, "SKETCHTEXT");

        /// <summary>
        /// SF symbol selection.
        /// </summary>
        public static readonly SelectionType SfSymbol = new SelectionType(swSelectType_e.swSelSFSYMBOLS, "SFSYMBOL");

        /// <summary>
        /// Datum tag selection.
        /// </summary>
        public static readonly SelectionType DatumTag = new SelectionType(swSelectType_e.swSelDATUMTAGS, "DATUMTAG");

        /// <summary>
        /// Component pattern selection.
        /// </summary>
        public static readonly SelectionType ComponentPattern = new SelectionType(swSelectType_e.swSelCOMPPATTERN, "COMPPATTERN");

        /// <summary>
        /// Weld selection.
        /// </summary>
        public static readonly SelectionType Weld = new SelectionType(swSelectType_e.swSelWELDS, "WELD");

        /// <summary>
        /// Cosmetic thread selection.
        /// </summary>
        public static readonly SelectionType CosmeticThread = new SelectionType(swSelectType_e.swSelCTHREADS, "CTHREAD");

        /// <summary>
        /// Datum target selection.
        /// </summary>
        public static readonly SelectionType DatumTarget = new SelectionType(swSelectType_e.swSelDTMTARGS, "DTMTARG");

        /// <summary>
        /// Point reference selection.
        /// </summary>
        public static readonly SelectionType PointReference = new SelectionType(swSelectType_e.swSelPOINTREFS, "POINTREF");

        /// <summary>
        /// Cabinet selection.
        /// </summary>
        public static readonly SelectionType Cabinet = new SelectionType(swSelectType_e.swSelDCABINETS, "DCABINET");

        /// <summary>
        /// Exploded view selection.
        /// </summary>
        public static readonly SelectionType ExplodedView = new SelectionType(swSelectType_e.swSelEXPLVIEWS, "EXPLODEDVIEWS");

        /// <summary>
        /// Explode step selection.
        /// </summary>
        public static readonly SelectionType ExplodeStep = new SelectionType(swSelectType_e.swSelEXPLSTEPS, "EXPLODESTEPS");

        /// <summary>
        /// Explode line selection.
        /// </summary>
        public static readonly SelectionType ExplodeLine = new SelectionType(swSelectType_e.swSelEXPLLINES, "EXPLODELINES");

        /// <summary>
        /// Silhouette selection.
        /// </summary>
        public static readonly SelectionType Silhouette = new SelectionType(swSelectType_e.swSelSILHOUETTES, "SILHOUETTE");

        /// <summary>
        /// Configuration selection.
        /// </summary>
        public static readonly SelectionType Configuration = new SelectionType(swSelectType_e.swSelCONFIGURATIONS, "CONFIGURATIONS");

        /// <summary>
        /// Object handle selection.
        /// </summary>
        public static readonly SelectionType ObjectHandle = new SelectionType(swSelectType_e.swSelOBJHANDLES, string.Empty);

        /// <summary>
        /// Arrow selection.
        /// </summary>
        public static readonly SelectionType Arrow = new SelectionType(swSelectType_e.swSelARROWS, "VIEWARROW");

        /// <summary>
        /// Zone selection.
        /// </summary>
        public static readonly SelectionType Zone = new SelectionType(swSelectType_e.swSelZONES, "ZONES");

        /// <summary>
        /// Reference edge selection.
        /// </summary>
        public static readonly SelectionType ReferenceEdge = new SelectionType(swSelectType_e.swSelREFEDGES, "REFERENCE-EDGE");

        /// <summary>
        /// Reference face selection.
        /// </summary>
        public static readonly SelectionType ReferenceFace = new SelectionType(swSelectType_e.swSelREFFACES, string.Empty);

        /// <summary>
        /// Reference silhouette selection.
        /// </summary>
        public static readonly SelectionType ReferenceSilhouette = new SelectionType(swSelectType_e.swSelREFSILHOUETTE, string.Empty);

        /// <summary>
        /// BOM selection.
        /// </summary>
        public static readonly SelectionType Bom = new SelectionType(swSelectType_e.swSelBOMS, "BOM");

        /// <summary>
        /// Equation folder selection.
        /// </summary>
        public static readonly SelectionType EquationFolder = new SelectionType(swSelectType_e.swSelEQNFOLDER, "EQNFOLDER");

        /// <summary>
        /// Sketch hatch selection.
        /// </summary>
        public static readonly SelectionType SketchHatch = new SelectionType(swSelectType_e.swSelSKETCHHATCH, "SKETCHHATCH");

        /// <summary>
        /// Import folder selection.
        /// </summary>
        public static readonly SelectionType ImportFolder = new SelectionType(swSelectType_e.swSelIMPORTFOLDER, "IMPORTFOLDER");

        /// <summary>
        /// Viewer hyperlink selection.
        /// </summary>
        public static readonly SelectionType ViewerHyperlink = new SelectionType(swSelectType_e.swSelVIEWERHYPERLINK, "HYPERLINK");

        /// <summary>
        /// Midpoint selection.
        /// </summary>
        public static readonly SelectionType Midpoint = new SelectionType(swSelectType_e.swSelMIDPOINTS, string.Empty);

        /// <summary>
        /// Custom symbol selection. Deprecated.
        /// </summary>
        public static readonly SelectionType CustomSymbol = new SelectionType(swSelectType_e.swSelCUSTOMSYMBOLS, "CUSTOMSYMBOL");

        /// <summary>
        /// Coordinate system selection.
        /// </summary>
        public static readonly SelectionType CoordinateSystem = new SelectionType(swSelectType_e.swSelCOORDSYS, "COORDSYS");

        /// <summary>
        /// Datum line selection.
        /// </summary>
        public static readonly SelectionType DatumLine = new SelectionType(swSelectType_e.swSelDATUMLINES, "REFLINE");

        /// <summary>
        /// Route curve selection.
        /// </summary>
        public static readonly SelectionType RouteCurve = new SelectionType(swSelectType_e.swSelROUTECURVES, string.Empty);

        /// <summary>
        /// BOM template selection.
        /// </summary>
        public static readonly SelectionType BomTemplate = new SelectionType(swSelectType_e.swSelBOMTEMPS, "BOMTEMP");

        /// <summary>
        /// Route point selection.
        /// </summary>
        public static readonly SelectionType RoutePoint = new SelectionType(swSelectType_e.swSelROUTEPOINTS, "ROUTEPOINT");

        /// <summary>
        /// Connection point selection.
        /// </summary>
        public static readonly SelectionType ConnectionPoint = new SelectionType(swSelectType_e.swSelCONNECTIONPOINTS, "CONNECTIONPOINT");

        /// <summary>
        /// Route sweep selection.
        /// </summary>
        public static readonly SelectionType RouteSweep = new SelectionType(swSelectType_e.swSelROUTESWEEPS, string.Empty);

        /// <summary>
        /// Position group selection.
        /// </summary>
        public static readonly SelectionType PositionGroup = new SelectionType(swSelectType_e.swSelPOSGROUP, "POSGROUP");

        /// <summary>
        /// Browser item selection.
        /// </summary>
        public static readonly SelectionType BrowserItem = new SelectionType(swSelectType_e.swSelBROWSERITEM, "BROWSERITEM");

        /// <summary>
        /// Fabricated route selection.
        /// </summary>
        public static readonly SelectionType FabricatedRoute = new SelectionType(swSelectType_e.swSelFABRICATEDROUTE, "ROUTEFABRICATED");

        /// <summary>
        /// Sketch point feature selection.
        /// </summary>
        public static readonly SelectionType SketchPointFeature = new SelectionType(swSelectType_e.swSelSKETCHPOINTFEAT, "SKETCHPOINTFEAT");

        /// <summary>
        /// Component don't override selection.
        /// </summary>
        public static readonly SelectionType ComponentDoNotOverride = new SelectionType(swSelectType_e.swSelCOMPSDONTOVERRIDE, string.Empty);

        /// <summary>
        /// Light selection.
        /// </summary>
        public static readonly SelectionType Light = new SelectionType(swSelectType_e.swSelLIGHTS, "LIGHTS");

        /// <summary>
        /// Wire body selection.
        /// </summary>
        public static readonly SelectionType WireBody = new SelectionType(swSelectType_e.swSelWIREBODIES, string.Empty);

        /// <summary>
        /// Surface body selection.
        /// </summary>
        public static readonly SelectionType SurfaceBody = new SelectionType(swSelectType_e.swSelSURFACEBODIES, "SURFACEBODY");

        /// <summary>
        /// Solid body selection.
        /// </summary>
        public static readonly SelectionType SolidBody = new SelectionType(swSelectType_e.swSelSOLIDBODIES, "SOLIDBODY");

        /// <summary>
        /// Frame point selection.
        /// </summary>
        public static readonly SelectionType FramePoint = new SelectionType(swSelectType_e.swSelFRAMEPOINT, "FRAMEPOINT");

        /// <summary>
        /// Surface body first selection.
        /// </summary>
        public static readonly SelectionType SurfaceBodyFirst = new SelectionType(swSelectType_e.swSelSURFBODIESFIRST, string.Empty);

        /// <summary>
        /// Manipulator selection.
        /// </summary>
        public static readonly SelectionType Manipulator = new SelectionType(swSelectType_e.swSelMANIPULATORS, "MANIPULATOR");

        /// <summary>
        /// Picture body selection.
        /// </summary>
        public static readonly SelectionType PictureBody = new SelectionType(swSelectType_e.swSelPICTUREBODIES, "PICTURE BODY");

        /// <summary>
        /// Solid body first selection.
        /// </summary>
        public static readonly SelectionType SolidBodyFirst = new SelectionType(swSelectType_e.swSelSOLIDBODIESFIRST, string.Empty);

        /// <summary>
        /// Hole series selection.
        /// </summary>
        public static readonly SelectionType HoleSeries = new SelectionType(swSelectType_e.swSelHOLESERIES, "HOLESERIES");

        /// <summary>
        /// Leader selection.
        /// </summary>
        public static readonly SelectionType Leader = new SelectionType(swSelectType_e.swSelLEADERS, "LEADER");

        /// <summary>
        /// Sketch bitmap selection.
        /// </summary>
        public static readonly SelectionType SketchBitmap = new SelectionType(swSelectType_e.swSelSKETCHBITMAP, "SKETCHBITMAP");

        /// <summary>
        /// Dowel symbol selection.
        /// </summary>
        public static readonly SelectionType DowelSymbol = new SelectionType(swSelectType_e.swSelDOWELSYMS, "DOWLELSYM");

        /// <summary>
        /// External sketch text selection.
        /// </summary>
        public static readonly SelectionType ExternalSketchText = new SelectionType(swSelectType_e.swSelEXTSKETCHTEXT, "EXTSKETCHTEXT");

        /// <summary>
        /// Old block instance selection. Deprecated.
        /// </summary>
        public static readonly SelectionType BlockInstance = new SelectionType(swSelectType_e.swSelBLOCKINST, "BLOCKINST");

        /// <summary>
        /// Feature folder selection.
        /// </summary>
        public static readonly SelectionType FeatureFolder = new SelectionType(swSelectType_e.swSelFTRFOLDER, "FTRFOLDER");

        /// <summary>
        /// Sketch region selection.
        /// </summary>
        public static readonly SelectionType SketchRegion = new SelectionType(swSelectType_e.swSelSKETCHREGION, "SKETCHREGION");

        /// <summary>
        /// Sketch contour selection.
        /// </summary>
        public static readonly SelectionType SketchContour = new SelectionType(swSelectType_e.swSelSKETCHCONTOUR, "SKETCHCONTOUR");

        /// <summary>
        /// BOM feature selection.
        /// </summary>
        public static readonly SelectionType BomFeature = new SelectionType(swSelectType_e.swSelBOMFEATURES, "BOMFEATURE");

        /// <summary>
        /// Annotation table selection.
        /// </summary>
        public static readonly SelectionType AnnotationTable = new SelectionType(swSelectType_e.swSelANNOTATIONTABLES, "ANNOTATIONTABLES");

        /// <summary>
        /// Old block definition selection. Deprecated.
        /// </summary>
        public static readonly SelectionType BlockDefinition = new SelectionType(swSelectType_e.swSelBLOCKDEF, "BLOCKDEF");

        /// <summary>
        /// Center mark symbol selection.
        /// </summary>
        public static readonly SelectionType CenterMarkSymbol = new SelectionType(swSelectType_e.swSelCENTERMARKSYMS, "CENTERMARKSYMS");

        /// <summary>
        /// Simulation selection.
        /// </summary>
        public static readonly SelectionType Simulation = new SelectionType(swSelectType_e.swSelSIMULATION, "SIMULATION");

        /// <summary>
        /// Simulation element selection.
        /// </summary>
        public static readonly SelectionType SimulationElement = new SelectionType(swSelectType_e.swSelSIMELEMENT, "SIMULATION_ELEMENT");

        /// <summary>
        /// Center line selection.
        /// </summary>
        public static readonly SelectionType CenterLine = new SelectionType(swSelectType_e.swSelCENTERLINES, "CENTERLINE");

        /// <summary>
        /// Hole table feature selection.
        /// </summary>
        public static readonly SelectionType HoleTableFeature = new SelectionType(swSelectType_e.swSelHOLETABLEFEATS, "HOLETABLE");

        /// <summary>
        /// Hole table axis selection.
        /// </summary>
        public static readonly SelectionType HoleTableAxis = new SelectionType(swSelectType_e.swSelHOLETABLEAXES, "HOLETABLEAXIS");

        /// <summary>
        /// Weldment selection.
        /// </summary>
        public static readonly SelectionType Weldment = new SelectionType(swSelectType_e.swSelWELDMENT, "WELDMENT");

        /// <summary>
        /// Sub weld folder selection.
        /// </summary>
        public static readonly SelectionType SubWeldFolder = new SelectionType(swSelectType_e.swSelSUBWELDFOLDER, "SUBWELDMENT");

        /// <summary>
        /// Exclude manipulator selection.
        /// </summary>
        public static readonly SelectionType ExcludeManipulator = new SelectionType(swSelectType_e.swSelEXCLUDEMANIPULATORS, string.Empty);

        /// <summary>
        /// Revision table selection.
        /// </summary>
        public static readonly SelectionType RevisionTable = new SelectionType(swSelectType_e.swSelREVISIONTABLE, "REVISIONTABLE");

        /// <summary>
        /// Sub-sketch instance selection.
        /// </summary>
        public static readonly SelectionType SubSketchInstance = new SelectionType(swSelectType_e.swSelSUBSKETCHINST, "SUBSKETCHINST");

        /// <summary>
        /// Weldment table feature selection.
        /// </summary>
        public static readonly SelectionType WeldmentTableFeature = new SelectionType(swSelectType_e.swSelWELDMENTTABLEFEATS, "WELDMENTTABLE");

        /// <summary>
        /// Body folder selection.
        /// </summary>
        public static readonly SelectionType BodyFolder = new SelectionType(swSelectType_e.swSelBODYFOLDER, "BDYFOLDER");

        /// <summary>
        /// Revision table feature selection.
        /// </summary>
        public static readonly SelectionType RevisionTableFeature = new SelectionType(swSelectType_e.swSelREVISIONTABLEFEAT, "REVISIONTABLEFEAT");

        /// <summary>
        /// Sub-atom folder selection.
        /// </summary>
        public static readonly SelectionType SubAtomFolder = new SelectionType(swSelectType_e.swSelSUBATOMFOLDER, string.Empty);

        /// <summary>
        /// Weld bead selection.
        /// </summary>
        public static readonly SelectionType WeldBead = new SelectionType(swSelectType_e.swSelWELDBEADS, "WELDBEADS");

        /// <summary>
        /// Embed link document selection.
        /// </summary>
        public static readonly SelectionType EmbedLinkDoc = new SelectionType(swSelectType_e.swSelEMBEDLINKDOC, "EMBEDLINKDOC");

        /// <summary>
        /// Journal selection.
        /// </summary>
        public static readonly SelectionType Journal = new SelectionType(swSelectType_e.swSelJOURNAL, "JOURNAL");

        /// <summary>
        /// Documents folder selection.
        /// </summary>
        public static readonly SelectionType DocsFolder = new SelectionType(swSelectType_e.swSelDOCSFOLDER, "DOCSFOLDER");

        /// <summary>
        /// Comments folder selection.
        /// </summary>
        public static readonly SelectionType CommentsFolder = new SelectionType(swSelectType_e.swSelCOMMENTSFOLDER, "COMMENTSFOLDER");

        /// <summary>
        /// Comment selection.
        /// </summary>
        public static readonly SelectionType Comment = new SelectionType(swSelectType_e.swSelCOMMENT, "COMMENT");

        /// <summary>
        /// Swift / DimXpert annotation selection.
        /// </summary>
        public static readonly SelectionType SwiftAnnotation = new SelectionType(swSelectType_e.swSelSWIFTANNOTATIONS, "SWIFTANN");

        /// <summary>
        /// Swift / DimXpert feature selection.
        /// </summary>
        public static readonly SelectionType SwiftFeature = new SelectionType(swSelectType_e.swSelSWIFTFEATURES, "SWIFTFEATURE");

        /// <summary>
        /// Camera selection.
        /// </summary>
        public static readonly SelectionType Camera = new SelectionType(swSelectType_e.swSelCAMERAS, "CAMERAS");

        /// <summary>
        /// Mate supplement selection.
        /// </summary>
        public static readonly SelectionType MateSupplement = new SelectionType(swSelectType_e.swSelMATESUPPLEMENT, "MATESUPPLEMENT");

        /// <summary>
        /// Annotation view selection.
        /// </summary>
        public static readonly SelectionType AnnotationView = new SelectionType(swSelectType_e.swSelANNOTATIONVIEW, "ANNVIEW");

        /// <summary>
        /// General table feature selection.
        /// </summary>
        public static readonly SelectionType GeneralTableFeature = new SelectionType(swSelectType_e.swSelGENERALTABLEFEAT, "GENERALTABLEFEAT");

        /// <summary>
        /// Display state selection.
        /// </summary>
        public static readonly SelectionType DisplayState = new SelectionType(swSelectType_e.swSelDISPLAYSTATE, "VISUALSTATES");

        /// <summary>
        /// Belt chain feature selection.
        /// </summary>
        public static readonly SelectionType BeltChainFeature = new SelectionType(swSelectType_e.swSelBELTCHAINFEATS, "SKETCHBELT");

        /// <summary>
        /// Sub-sketch definition selection.
        /// </summary>
        public static readonly SelectionType SubSketchDefinition = new SelectionType(swSelectType_e.swSelSUBSKETCHDEF, "SUBSKETCHDEF");

        /// <summary>
        /// Swift / DimXpert schema selection.
        /// </summary>
        public static readonly SelectionType SwiftSchema = new SelectionType(swSelectType_e.swSelSWIFTSCHEMA, "SWIFTSCHEMA");

        /// <summary>
        /// Title block selection.
        /// </summary>
        public static readonly SelectionType TitleBlock = new SelectionType(swSelectType_e.swSelTITLEBLOCK, "TITLEBLOCK");

        /// <summary>
        /// Title block table feature selection.
        /// </summary>
        public static readonly SelectionType TitleBlockTableFeature = new SelectionType(swSelectType_e.swSelTITLEBLOCKTABLEFEAT, "TITLEBLOCKTABLEFEAT");

        /// <summary>
        /// Object group selection.
        /// Warning: TODO: this value is marked as 155 in the documentation, but the enum value is 207.
        /// </summary>
        public static readonly SelectionType ObjectGroup = new SelectionType(swSelectType_e.swSelOBJGROUP, "OBJGROUP");

        /// <summary>
        /// Cosmetic weld selection.
        /// </summary>
        public static readonly SelectionType CosmeticWeld = new SelectionType(swSelectType_e.swSelCOSMETICWELDS, "COSMETICWELDS");

        /// <summary>
        /// Magnetic line selection.
        /// </summary>
        public static readonly SelectionType MagneticLine = new SelectionType(swSelectType_e.SwSelMAGNETICLINES, "MAGNETICLINES");

        /// <summary>
        /// Punch table feature selection.
        /// </summary>
        public static readonly SelectionType PunchTableFeature = new SelectionType(swSelectType_e.swSelPUNCHTABLEFEATS, "PUNCHTABLE");

        /// <summary>
        /// Revision cloud selection.
        /// </summary>
        public static readonly SelectionType RevisionCloud = new SelectionType(swSelectType_e.swSelREVISIONCLOUDS, string.Empty);

        /// <summary>
        /// Selection set folder selection.
        /// </summary>
        public static readonly SelectionType SelectionSetFolder = new SelectionType(swSelectType_e.swSelSELECTIONSETFOLDER, "SELECTIONSETFOLDER");

        /// <summary>
        /// Selection set node selection.
        /// </summary>
        public static readonly SelectionType SelectionSetNode = new SelectionType(swSelectType_e.swSelSELECTIONSETNODE, "SUBSELECTIONSETNODE");

        /// <summary>
        /// Graphics body selection.
        /// </summary>
        public static readonly SelectionType GraphicsBody = new SelectionType(swSelectType_e.swSelGRAPHICSBODY, "MESH BODY FEATURE");

        /// <summary>
        /// Facet selection.
        /// </summary>
        public static readonly SelectionType Facet = new SelectionType(swSelectType_e.swSelFACETS, "MESHFACETREF");

        /// <summary>
        /// Mesh facet edge selection.
        /// </summary>
        public static readonly SelectionType MeshFacetEdge = new SelectionType(swSelectType_e.swSelMESHFACETEDGES, "MESHFINREF");

        /// <summary>
        /// Mesh facet vertex selection.
        /// </summary>
        public static readonly SelectionType MeshFacetVertex = new SelectionType(swSelectType_e.swSelMESHFACETVERTICES, "MESHVERTEXREF");

        /// <summary>
        /// Mesh solid body selection.
        /// </summary>
        public static readonly SelectionType MeshSolidBody = new SelectionType(swSelectType_e.swSelMESHSOLIDBODIES, "MSOLIDBODY");

        /// <summary>
        /// Advanced structure member selection.
        /// </summary>
        public static readonly SelectionType AdvStructMember = new SelectionType(swSelectType_e.swSelADVSTRUCTMEMBER, "ADVSTRUCTMEMBER");

        #endregion

        #region Private members

        /// <summary>
        /// The underlying enum value. Private because you should not need to access it directly.
        /// Returned by, for example, <see cref="ISelectionMgr.GetSelectedObjectType3"/>.
        /// </summary>
        private readonly swSelectType_e _enumValue;

        /// <summary>
        /// The underlying string value. Private because you should not need to access it directly.
        /// Used when calling <see cref="IModelDocExtension.SelectByID2"/>.
        /// </summary>
        private readonly string _stringValue;

        /// <summary>
        /// List of custom feature names for this selection type.
        /// </summary>
        private readonly string[] _customFeatureNames;

        #endregion

        #region Constructor

        /// <summary>
        /// Internal constructor for predefined selection types.
        /// Initializes a new <see cref="SelectionType"/> with the specified integer and string value.
        /// </summary>
        /// <param name="enumValue">The numeric identifier for the selection type.</param>
        /// <param name="stringValue">The string representation used in the SOLIDWORKS API.</param>
        /// <remarks>
        /// Custom feature types should use <see cref="CreateCustomFeatureType(SelectionType, string)"/> instead.
        /// </remarks>
        internal SelectionType(swSelectType_e enumValue, string stringValue)
        {
            _enumValue = enumValue;
            _stringValue = stringValue;
            _customFeatureNames = null;
        }

        /// <summary>
        /// Internal constructor for custom feature selection types.
        /// Initializes a new <see cref="SelectionType"/> with custom feature names.
        /// </summary>
        /// <param name="baseType"></param>
        /// <param name="customFeatureNames">An array of custom feature names.</param>
        /// <remarks>
        /// This constructor is used for user-defined selection types not covered by the standard enum.
        /// The integer value is set to <see cref="Attribute"/> as a default for custom types.
        /// </remarks>
        internal SelectionType(SelectionType baseType, string[] customFeatureNames)
        {
            _enumValue = baseType._enumValue;
            _stringValue = baseType;
            _customFeatureNames = customFeatureNames;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Create a new <see cref="SelectionType"/> for a single custom feature.
        /// </summary>
        /// <param name="baseType"></param>
        /// <param name="featureName">The name of the custom feature.</param>
        /// <returns>A new <see cref="SelectionType"/> instance.</returns>
        /// <remarks>
        /// Can be used with <see cref="SelectionType.Attribute"/>
        /// </remarks>
        public static SelectionType CreateCustomFeatureType(SelectionType baseType, string featureName) => new SelectionType(baseType, new[] { featureName });

        /// <summary>
        /// Create a new <see cref="SelectionType"/> for multiple custom features.
        /// </summary>
        /// <param name="baseType"></param>
        /// <param name="featureNames">An array of custom feature names.</param>
        /// <returns>A new <see cref="SelectionType"/> instance.</returns>
        /// <remarks>
        /// Can be used with <see cref="SelectionType.Attribute"/>
        /// </remarks>
        public static SelectionType CreateCustomFeatureType(SelectionType baseType, string[] featureNames) => new SelectionType(baseType, featureNames);

        /// <summary>
        /// Get a semicolon-separated string of custom feature names (if applicable).
        /// </summary>
        /// <returns>
        /// A combined string of custom feature names, or <see cref="string.Empty"/> if this is not a custom type.
        /// </returns>
        /// <remarks>
        /// Used internally for Command Manager API calls that require multiple selection identifiers.
        /// </remarks>
        internal string GetCustomFeatureNames() => _customFeatureNames is null ? string.Empty : string.Join(";", _customFeatureNames);

        #endregion

        #region Operators

        /// <summary>
        /// Implicitly convert a <see cref="SelectionType"/> to its integer value.
        /// </summary>
        /// <param name="selectionType">The <see cref="SelectionType"/> to convert.</param>
        /// <returns>The underlying integer value.</returns>
        /// <remarks>
        /// Allows seamless use of <see cref="SelectionType"/> where an integer is expected in API calls.
        /// </remarks>
        public static implicit operator int(SelectionType selectionType)
        {
            return (int)selectionType._enumValue;
        }

        /// <summary>
        /// Implicitly convert a <see cref="SelectionType"/> to its string representation.
        /// </summary>
        /// <param name="selectionType">The <see cref="SelectionType"/> to convert.</param>
        /// <returns>
        /// The underlying string value, or null for custom types.
        /// </returns>
        /// <remarks>
        /// Allows seamless use of <see cref="SelectionType"/> where a string is expected in API calls.
        /// </remarks>
        public static implicit operator string(SelectionType selectionType)
        {
            return selectionType._stringValue;
        }

        #endregion

        #region Equality

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            // Null and type check
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = (SelectionType)obj;

            // Compare primitive values first for early exit
            if (_enumValue != other._enumValue)
                return false;

            // String comparison for _stringValue not used it depends on _intValue

            // Handle null cases for custom feature names
            if (_customFeatureNames == null || other._customFeatureNames == null)
                return _customFeatureNames == null && other._customFeatureNames == null;

            // Compare array lengths first for quick mismatch detection
            if (_customFeatureNames.Length != other._customFeatureNames.Length)
                return false;

            // Element-by-element comparison
            for (var i = 0; i < _customFeatureNames.Length; i++)
            {
                if (!string.Equals(_customFeatureNames[i], other._customFeatureNames[i], StringComparison.Ordinal))
                    return false;
            }

            return true;
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                if (_customFeatureNames == null)
                {
                    // Handles only _intValue if no custom features, _stringValue check is not necessary
                    return _enumValue.GetHashCode();
                }

                // Handles _intValue and custom features array if we have custom features. _stringValue check is not necessary.
                var hashCode = _customFeatureNames.Sum(featureName => featureName.GetHashCode() * 17);
                hashCode += _enumValue.GetHashCode();
                return hashCode;
            }
        }

        #endregion
    }
}
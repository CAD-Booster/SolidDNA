using System;
using System.Linq;

namespace CADBooster.SolidDna
{
    public class SelectionType
    {
        #region Static values

        /// <summary>
        /// Nothing selected.
        /// </summary>
        public static readonly SelectionType Nothing = new SelectionType(0, string.Empty);

        /// <summary>
        /// Edge selection.
        /// </summary>
        public static readonly SelectionType Edge = new SelectionType(1, "EDGE");

        /// <summary>
        /// Face selection.
        /// </summary>
        public static readonly SelectionType Face = new SelectionType(2, "FACE");

        /// <summary>
        /// Vertex selection.
        /// </summary>
        public static readonly SelectionType Vertex = new SelectionType(3, "VERTEX");

        /// <summary>
        /// Datum plane selection.
        /// </summary>
        public static readonly SelectionType DatumPlane = new SelectionType(4, "PLANE");

        /// <summary>
        /// Datum axis selection.
        /// </summary>
        public static readonly SelectionType DatumAxis = new SelectionType(5, "AXIS");

        /// <summary>
        /// Datum point selection.
        /// </summary>
        public static readonly SelectionType DatumPoint = new SelectionType(6, "DATUMPOINT");

        /// <summary>
        /// OLE item selection.
        /// </summary>
        public static readonly SelectionType OleItem = new SelectionType(7, "OLEITEM");

        /// <summary>
        /// Attribute selection.
        /// </summary>
        public static readonly SelectionType Attribute = new SelectionType(8, "ATTRIBUTE");

        /// <summary>
        /// Sketch selection.
        /// </summary>
        public static readonly SelectionType Sketch = new SelectionType(9, "SKETCH");

        /// <summary>
        /// Sketch segment selection.
        /// </summary>
        public static readonly SelectionType SketchSegment = new SelectionType(10, "SKETCHSEGMENT");

        /// <summary>
        /// Sketch point selection.
        /// </summary>
        public static readonly SelectionType SketchPoint = new SelectionType(11, "SKETCHPOINT");

        /// <summary>
        /// Drawing view selection.
        /// </summary>
        public static readonly SelectionType DrawingView = new SelectionType(12, "DRAWINGVIEW");

        /// <summary>
        /// Geometric tolerance selection.
        /// </summary>
        public static readonly SelectionType GeometricTolerance = new SelectionType(13, "GTOL");

        /// <summary>
        /// Dimension selection.
        /// </summary>
        public static readonly SelectionType Dimension = new SelectionType(14, "DIMENSION");

        /// <summary>
        /// Note selection.
        /// </summary>
        public static readonly SelectionType Note = new SelectionType(15, "NOTE");

        /// <summary>
        /// Section line selection.
        /// </summary>
        public static readonly SelectionType SectionLine = new SelectionType(16, "SECTIONLINE");

        /// <summary>
        /// Detail circle selection.
        /// </summary>
        public static readonly SelectionType DetailCircle = new SelectionType(17, "DETAILCIRCLE");

        /// <summary>
        /// Section text selection.
        /// </summary>
        public static readonly SelectionType SectionText = new SelectionType(18, "SECTIONTEXT");

        /// <summary>
        /// Sheet selection.
        /// </summary>
        public static readonly SelectionType Sheet = new SelectionType(19, "SHEET");

        /// <summary>
        /// Component selection.
        /// </summary>
        public static readonly SelectionType Component = new SelectionType(20, "COMPONENT");

        /// <summary>
        /// Mate selection.
        /// </summary>
        public static readonly SelectionType Mate = new SelectionType(21, "MATE");

        /// <summary>
        /// Body feature selection.
        /// </summary>
        public static readonly SelectionType BodyFeature = new SelectionType(22, "BODYFEATURE");

        /// <summary>
        /// Reference curve selection.
        /// </summary>
        public static readonly SelectionType ReferenceCurve = new SelectionType(23, "REFCURVE");

        /// <summary>
        /// External sketch segment selection.
        /// </summary>
        public static readonly SelectionType ExternalSketchSegment = new SelectionType(24, "EXTSKETCHSEGMENT");

        /// <summary>
        /// External sketch point selection.
        /// </summary>
        public static readonly SelectionType ExternalSketchPoint = new SelectionType(25, "EXTSKETCHPOINT");

        /// <summary>
        /// Helix selection.
        /// </summary>
        public static readonly SelectionType Helix = new SelectionType(26, "HELIX");

        /// <summary>
        /// Reference surface selection.
        /// </summary>
        public static readonly SelectionType ReferenceSurface = new SelectionType(27, "REFSURFACE");

        /// <summary>
        /// Center mark selection.
        /// </summary>
        public static readonly SelectionType CenterMark = new SelectionType(28, "CENTERMARKS");

        /// <summary>
        /// In-context feature selection.
        /// </summary>
        public static readonly SelectionType InContextFeature = new SelectionType(29, "INCONTEXTFEAT");

        /// <summary>
        /// Mate group selection.
        /// </summary>
        public static readonly SelectionType MateGroup = new SelectionType(30, "MATEGROUP");

        /// <summary>
        /// Break line selection.
        /// </summary>
        public static readonly SelectionType BreakLine = new SelectionType(31, "BREAKLINE");

        /// <summary>
        /// In-context features selection.
        /// </summary>
        public static readonly SelectionType InContextFeatures = new SelectionType(32, "INCONTEXTFEATS");

        /// <summary>
        /// Mate groups selection.
        /// </summary>
        public static readonly SelectionType MateGroups = new SelectionType(33, "MATEGROUPS");

        /// <summary>
        /// Sketch text selection.
        /// </summary>
        public static readonly SelectionType SketchText = new SelectionType(34, "SKETCHTEXT");

        /// <summary>
        /// SF symbol selection.
        /// </summary>
        public static readonly SelectionType SfSymbol = new SelectionType(35, "SFSYMBOL");

        /// <summary>
        /// Datum tag selection.
        /// </summary>
        public static readonly SelectionType DatumTag = new SelectionType(36, "DATUMTAG");

        /// <summary>
        /// Component pattern selection.
        /// </summary>
        public static readonly SelectionType ComponentPattern = new SelectionType(37, "COMPPATTERN");

        /// <summary>
        /// Weld selection.
        /// </summary>
        public static readonly SelectionType Weld = new SelectionType(38, "WELD");

        /// <summary>
        /// Cosmetic thread selection.
        /// </summary>
        public static readonly SelectionType CosmeticThread = new SelectionType(39, "CTHREAD");

        /// <summary>
        /// Datum target selection.
        /// </summary>
        public static readonly SelectionType DatumTarget = new SelectionType(40, "DTMTARG");

        /// <summary>
        /// Point reference selection.
        /// </summary>
        public static readonly SelectionType PointReference = new SelectionType(41, "POINTREF");

        /// <summary>
        /// Cabinet selection.
        /// </summary>
        public static readonly SelectionType Cabinet = new SelectionType(42, "DCABINET");

        /// <summary>
        /// Exploded view selection.
        /// </summary>
        public static readonly SelectionType ExplodedView = new SelectionType(43, "EXPLODEDVIEWS");

        /// <summary>
        /// Explode step selection.
        /// </summary>
        public static readonly SelectionType ExplodeStep = new SelectionType(44, "EXPLODESTEPS");

        /// <summary>
        /// Explode line selection.
        /// </summary>
        public static readonly SelectionType ExplodeLine = new SelectionType(45, "EXPLODELINES");

        /// <summary>
        /// Silhouette selection.
        /// </summary>
        public static readonly SelectionType Silhouette = new SelectionType(46, "SILHOUETTE");

        /// <summary>
        /// Configuration selection.
        /// </summary>
        public static readonly SelectionType Configuration = new SelectionType(47, "CONFIGURATIONS");

        /// <summary>
        /// Object handle selection.
        /// </summary>
        public static readonly SelectionType ObjectHandle = new SelectionType(48, string.Empty);

        /// <summary>
        /// Arrow selection.
        /// </summary>
        public static readonly SelectionType Arrow = new SelectionType(49, "VIEWARROW");

        /// <summary>
        /// Zone selection.
        /// </summary>
        public static readonly SelectionType Zone = new SelectionType(50, "ZONES");

        /// <summary>
        /// Reference edge selection.
        /// </summary>
        public static readonly SelectionType ReferenceEdge = new SelectionType(51, "REFERENCE-EDGE");

        /// <summary>
        /// Reference face selection.
        /// </summary>
        public static readonly SelectionType ReferenceFace = new SelectionType(52, string.Empty);

        /// <summary>
        /// Reference silhouette selection.
        /// </summary>
        public static readonly SelectionType ReferenceSilhouette = new SelectionType(53, string.Empty);

        /// <summary>
        /// BOM selection.
        /// </summary>
        public static readonly SelectionType Bom = new SelectionType(54, "BOM");

        /// <summary>
        /// Equation folder selection.
        /// </summary>
        public static readonly SelectionType EquationFolder = new SelectionType(55, "EQNFOLDER");

        /// <summary>
        /// Sketch hatch selection.
        /// </summary>
        public static readonly SelectionType SketchHatch = new SelectionType(56, "SKETCHHATCH");

        /// <summary>
        /// Import folder selection.
        /// </summary>
        public static readonly SelectionType ImportFolder = new SelectionType(57, "IMPORTFOLDER");

        /// <summary>
        /// Viewer hyperlink selection.
        /// </summary>
        public static readonly SelectionType ViewerHyperlink = new SelectionType(58, "HYPERLINK");

        /// <summary>
        /// Midpoint selection.
        /// </summary>
        public static readonly SelectionType Midpoint = new SelectionType(59, string.Empty);

        /// <summary>
        /// Custom symbol selection.
        /// </summary>
        public static readonly SelectionType CustomSymbol = new SelectionType(60, "CUSTOMSYMBOL");

        /// <summary>
        /// Coordinate system selection.
        /// </summary>
        public static readonly SelectionType CoordinateSystem = new SelectionType(61, "COORDSYS");

        /// <summary>
        /// Datum line selection.
        /// </summary>
        public static readonly SelectionType DatumLine = new SelectionType(62, "REFLINE");

        /// <summary>
        /// Route curve selection.
        /// </summary>
        public static readonly SelectionType RouteCurve = new SelectionType(63, string.Empty);

        /// <summary>
        /// BOM template selection.
        /// </summary>
        public static readonly SelectionType BomTemplate = new SelectionType(64, "BOMTEMP");

        /// <summary>
        /// Route point selection.
        /// </summary>
        public static readonly SelectionType RoutePoint = new SelectionType(65, "ROUTEPOINT");

        /// <summary>
        /// Connection point selection.
        /// </summary>
        public static readonly SelectionType ConnectionPoint = new SelectionType(66, "CONNECTIONPOINT");

        /// <summary>
        /// Route sweep selection.
        /// </summary>
        public static readonly SelectionType RouteSweep = new SelectionType(67, string.Empty);

        /// <summary>
        /// Position group selection.
        /// </summary>
        public static readonly SelectionType PositionGroup = new SelectionType(68, "POSGROUP");

        /// <summary>
        /// Browser item selection.
        /// </summary>
        public static readonly SelectionType BrowserItem = new SelectionType(69, "BROWSERITEM");

        /// <summary>
        /// Fabricated route selection.
        /// </summary>
        public static readonly SelectionType FabricatedRoute = new SelectionType(70, "ROUTEFABRICATED");

        /// <summary>
        /// Sketch point feature selection.
        /// </summary>
        public static readonly SelectionType SketchPointFeature = new SelectionType(71, "SKETCHPOINTFEAT");

        /// <summary>
        /// Component don't override selection.
        /// </summary>
        public static readonly SelectionType ComponentDontOverride = new SelectionType(72, string.Empty);

        /// <summary>
        /// Light selection.
        /// </summary>
        public static readonly SelectionType Light = new SelectionType(73, "LIGHTS");

        /// <summary>
        /// Wire body selection.
        /// </summary>
        public static readonly SelectionType WireBody = new SelectionType(74, string.Empty);

        /// <summary>
        /// Surface body selection.
        /// </summary>
        public static readonly SelectionType SurfaceBody = new SelectionType(75, "SURFACEBODY");

        /// <summary>
        /// Solid body selection.
        /// </summary>
        public static readonly SelectionType SolidBody = new SelectionType(76, "SOLIDBODY");

        /// <summary>
        /// Frame point selection.
        /// </summary>
        public static readonly SelectionType FramePoint = new SelectionType(77, "FRAMEPOINT");

        /// <summary>
        /// Surface body first selection.
        /// </summary>
        public static readonly SelectionType SurfaceBodyFirst = new SelectionType(78, string.Empty);

        /// <summary>
        /// Manipulator selection.
        /// </summary>
        public static readonly SelectionType Manipulator = new SelectionType(79, "MANIPULATOR");

        /// <summary>
        /// Picture body selection.
        /// </summary>
        public static readonly SelectionType PictureBody = new SelectionType(80, "PICTURE BODY");

        /// <summary>
        /// Solid body first selection.
        /// </summary>
        public static readonly SelectionType SolidBodyFirst = new SelectionType(81, string.Empty);

        /// <summary>
        /// Hole series selection.
        /// </summary>
        public static readonly SelectionType HoleSeries = new SelectionType(83, "HOLESERIES");

        /// <summary>
        /// Leader selection.
        /// </summary>
        public static readonly SelectionType Leader = new SelectionType(84, "LEADER");

        /// <summary>
        /// Sketch bitmap selection.
        /// </summary>
        public static readonly SelectionType SketchBitmap = new SelectionType(85, "SKETCHBITMAP");

        /// <summary>
        /// Dowel symbol selection.
        /// </summary>
        public static readonly SelectionType DowelSymbol = new SelectionType(86, "DOWLELSYM");

        /// <summary>
        /// External sketch text selection.
        /// </summary>
        public static readonly SelectionType ExternalSketchText = new SelectionType(88, "EXTSKETCHTEXT");

        /// <summary>
        /// Block instance selection.
        /// </summary>
        public static readonly SelectionType BlockInstance = new SelectionType(93, "BLOCKINST");

        /// <summary>
        /// Feature folder selection.
        /// </summary>
        public static readonly SelectionType FeatureFolder = new SelectionType(94, "FTRFOLDER");

        /// <summary>
        /// Sketch region selection.
        /// </summary>
        public static readonly SelectionType SketchRegion = new SelectionType(95, "SKETCHREGION");

        /// <summary>
        /// Sketch contour selection.
        /// </summary>
        public static readonly SelectionType SketchContour = new SelectionType(96, "SKETCHCONTOUR");

        /// <summary>
        /// BOM feature selection.
        /// </summary>
        public static readonly SelectionType BomFeature = new SelectionType(97, "BOMFEATURE");

        /// <summary>
        /// Annotation table selection.
        /// </summary>
        public static readonly SelectionType AnnotationTable = new SelectionType(98, "ANNOTATIONTABLES");

        /// <summary>
        /// Block definition selection.
        /// </summary>
        public static readonly SelectionType BlockDefinition = new SelectionType(99, "BLOCKDEF");

        /// <summary>
        /// Center mark symbol selection.
        /// </summary>
        public static readonly SelectionType CenterMarkSymbol = new SelectionType(100, "CENTERMARKSYMS");

        /// <summary>
        /// Simulation selection.
        /// </summary>
        public static readonly SelectionType Simulation = new SelectionType(101, "SIMULATION");

        /// <summary>
        /// Simulation element selection.
        /// </summary>
        public static readonly SelectionType SimulationElement = new SelectionType(102, "SIMULATION_ELEMENT");

        /// <summary>
        /// Center line selection.
        /// </summary>
        public static readonly SelectionType CenterLine = new SelectionType(103, "CENTERLINE");

        /// <summary>
        /// Hole table feature selection.
        /// </summary>
        public static readonly SelectionType HoleTableFeature = new SelectionType(104, "HOLETABLE");

        /// <summary>
        /// Hole table axis selection.
        /// </summary>
        public static readonly SelectionType HoleTableAxis = new SelectionType(105, "HOLETABLEAXIS");

        /// <summary>
        /// Weldment selection.
        /// </summary>
        public static readonly SelectionType Weldment = new SelectionType(106, "WELDMENT");

        /// <summary>
        /// Sub weld folder selection.
        /// </summary>
        public static readonly SelectionType SubWeldFolder = new SelectionType(107, "SUBWELDMENT");

        /// <summary>
        /// Exclude manipulator selection.
        /// </summary>
        public static readonly SelectionType ExcludeManipulator = new SelectionType(111, string.Empty);

        /// <summary>
        /// Sub-sketch instance selection.
        /// </summary>
        public static readonly SelectionType SubSketchInstance = new SelectionType(114, "SUBSKETCHINST");

        /// <summary>
        /// Weldment table feature selection.
        /// </summary>
        public static readonly SelectionType WeldmentTableFeature = new SelectionType(116, "WELDMENTTABLE");

        /// <summary>
        /// Body folder selection.
        /// </summary>
        public static readonly SelectionType BodyFolder = new SelectionType(118, "BDYFOLDER");

        /// <summary>
        /// Revision table feature selection.
        /// </summary>
        public static readonly SelectionType RevisionTableFeature = new SelectionType(119, "REVISIONTABLEFEAT");

        /// <summary>
        /// Sub-atom folder selection.
        /// </summary>
        public static readonly SelectionType SubAtomFolder = new SelectionType(121, string.Empty);

        /// <summary>
        /// Weld bead selection.
        /// </summary>
        public static readonly SelectionType WeldBead = new SelectionType(122, "WELDBEADS");

        /// <summary>
        /// Embed link document selection.
        /// </summary>
        public static readonly SelectionType EmbedLinkDoc = new SelectionType(123, "EMBEDLINKDOC");

        /// <summary>
        /// Journal selection.
        /// </summary>
        public static readonly SelectionType Journal = new SelectionType(124, "JOURNAL");

        /// <summary>
        /// Documents folder selection.
        /// </summary>
        public static readonly SelectionType DocsFolder = new SelectionType(125, "DOCSFOLDER");

        /// <summary>
        /// Comments folder selection.
        /// </summary>
        public static readonly SelectionType CommentsFolder = new SelectionType(126, "COMMENTSFOLDER");

        /// <summary>
        /// Comment selection.
        /// </summary>
        public static readonly SelectionType Comment = new SelectionType(127, "COMMENT");

        /// <summary>
        /// Swift annotation selection.
        /// </summary>
        public static readonly SelectionType SwiftAnnotation = new SelectionType(130, "SWIFTANN");

        /// <summary>
        /// Swift feature selection.
        /// </summary>
        public static readonly SelectionType SwiftFeature = new SelectionType(132, "SWIFTFEATURE");

        /// <summary>
        /// Camera selection.
        /// </summary>
        public static readonly SelectionType Camera = new SelectionType(136, "CAMERAS");

        /// <summary>
        /// Mate supplement selection.
        /// </summary>
        public static readonly SelectionType MateSupplement = new SelectionType(138, "MATESUPPLEMENT");

        /// <summary>
        /// Annotation view selection.
        /// </summary>
        public static readonly SelectionType AnnotationView = new SelectionType(139, "ANNVIEW");

        /// <summary>
        /// General table feature selection.
        /// </summary>
        public static readonly SelectionType GeneralTableFeature = new SelectionType(142, "GENERALTABLEFEAT");

        /// <summary>
        /// Sub-sketch definition selection.
        /// </summary>
        public static readonly SelectionType SubSketchDefinition = new SelectionType(154, "SUBSKETCHDEF");

        /// <summary>
        /// Object group selection.
        /// </summary>
        public static readonly SelectionType ObjectGroup = new SelectionType(155, "OBJGROUP");

        /// <summary>
        /// Swift schema selection.
        /// </summary>
        public static readonly SelectionType SwiftSchema = new SelectionType(159, "SWIFTSCHEMA");

        /// <summary>
        /// Title block selection.
        /// </summary>
        public static readonly SelectionType TitleBlock = new SelectionType(192, "TITLEBLOCK");

        /// <summary>
        /// Title block table feature selection.
        /// </summary>
        public static readonly SelectionType TitleBlockTableFeature = new SelectionType(206, "TITLEBLOCKTABLEFEAT");

        /// <summary>
        /// Cosmetic weld selection.
        /// </summary>
        public static readonly SelectionType CosmeticWeld = new SelectionType(220, "COSMETICWELDS");

        /// <summary>
        /// Magnetic line selection.
        /// </summary>
        public static readonly SelectionType MagneticLine = new SelectionType(225, "MAGNETICLINES");

        /// <summary>
        /// Punch table feature selection.
        /// </summary>
        public static readonly SelectionType PunchTableFeature = new SelectionType(234, "PUNCHTABLE");

        /// <summary>
        /// Revision cloud selection.
        /// </summary>
        public static readonly SelectionType RevisionCloud = new SelectionType(240, string.Empty);

        /// <summary>
        /// Selection set folder selection.
        /// </summary>
        public static readonly SelectionType SelectionSetFolder = new SelectionType(258, "SELECTIONSETFOLDER");

        /// <summary>
        /// Selection set node selection.
        /// </summary>
        public static readonly SelectionType SelectionSetNode = new SelectionType(259, "SUBSELECTIONSETNODE");

        /// <summary>
        /// Graphics body selection.
        /// </summary>
        public static readonly SelectionType GraphicsBody = new SelectionType(262, "MESH BODY FEATURE");

        /// <summary>
        /// Facet selection.
        /// </summary>
        public static readonly SelectionType Facet = new SelectionType(268, "MESHFACETREF");

        /// <summary>
        /// Mesh facet edge selection.
        /// </summary>
        public static readonly SelectionType MeshFacetEdge = new SelectionType(269, "MESHFINREF");

        /// <summary>
        /// Mesh facet vertex selection.
        /// </summary>
        public static readonly SelectionType MeshFacetVertex = new SelectionType(270, "MESHVERTEXREF");

        /// <summary>
        /// Mesh solid body selection.
        /// </summary>
        public static readonly SelectionType MeshSolidBody = new SelectionType(274, "MSOLIDBODY");

        /// <summary>
        /// Belt chain feature selection.
        /// </summary>
        public static readonly SelectionType BeltChainFeature = new SelectionType(149, "SKETCHBELT");

        /// <summary>
        /// Advanced structure member selection.
        /// </summary>
        public static readonly SelectionType AdvStructMember = new SelectionType(295, "ADVSTRUCTMEMBER");

        /// <summary>
        /// Everything selection.
        /// </summary>
        public static readonly SelectionType Everything = new SelectionType(-3, "EVERYTHING");

        /// <summary>
        /// Location selection.
        /// </summary>
        public static readonly SelectionType Location = new SelectionType(-2, "LOCATIONS");

        /// <summary>
        /// Unsupported selection.
        /// </summary>
        public static readonly SelectionType Unsupported = new SelectionType(-1, "UNSUPPORTED");

        #endregion

        private readonly int _intValue;
        private readonly string _stringValue;
        private readonly string[] _customFeatureNames;

        /// <summary>
        /// Internal constructor for predefined selection types.
        /// Initializes a new <see cref="SelectionType"/> with the specified integer and string values.
        /// </summary>
        /// <param name="intValue">The numeric identifier for the selection type.</param>
        /// <param name="stringValue">The string representation used in the SOLIDWORKS API.</param>
        /// <remarks>
        /// Custom feature types should use <see cref="CreateCustomFeatureType(SelectionType, string)"/> instead.
        /// </remarks>
        internal SelectionType(int intValue, string stringValue)
        {
            _intValue = intValue;
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
            _intValue = baseType;
            _stringValue = baseType;
            _customFeatureNames = customFeatureNames;
        }

        /// <summary>
        /// Creates a new <see cref="SelectionType"/> for a single custom feature.
        /// </summary>
        /// <param name="baseType"></param>
        /// <param name="featureName">The name of the custom feature.</param>
        /// <returns>A new <see cref="SelectionType"/> instance.</returns>
        /// <example>
        /// var weldBeadType = SelectionType.CreateCustomFeatureType("MyAwesomeFeature");
        /// </example>
        /// <remarks>
        /// Can be used with <see cref="SelectionType.Attribute"/>
        /// </remarks>
        public static SelectionType CreateCustomFeatureType(SelectionType baseType, string featureName)
            => new SelectionType(baseType, new[] { featureName });

        /// <summary>
        /// Creates a new <see cref="SelectionType"/> for multiple custom features.
        /// </summary>
        /// <param name="baseType"></param>
        /// <param name="featureNames">An array of custom feature names.</param>
        /// <returns>A new <see cref="SelectionType"/> instance.</returns>
        /// <example>
        /// var customTypes = SelectionType.CreateCustomFeatureType(new[] { "MyAwesomeFeature1", "MyAwesomeFeature2" });
        /// </example>
        /// <remarks>
        /// Can be used with <see cref="SelectionType.Attribute"/>
        /// </remarks>
        public static SelectionType CreateCustomFeatureType(SelectionType baseType, string[] featureNames)
            => new SelectionType(baseType, featureNames);

        /// <summary>
        /// Gets a semicolon-separated string of custom feature names (if applicable).
        /// </summary>
        /// <returns>
        /// A combined string of custom feature names, or <see cref="string.Empty"/> if this is not a custom type.
        /// </returns>
        /// <remarks>
        /// Used internally for SOLIDWORKS API calls that require multiple selection identifiers.
        /// </remarks>
        internal string GetCustomFeaturesSelection()
            => _customFeatureNames is null ? string.Empty : string.Join(";", _customFeatureNames);

        /// <summary>
        /// Implicitly converts a <see cref="SelectionType"/> to its integer value.
        /// </summary>
        /// <param name="selectionType">The <see cref="SelectionType"/> to convert.</param>
        /// <returns>The underlying integer value.</returns>
        /// <remarks>
        /// Allows seamless use of <see cref="SelectionType"/> where an integer is expected (e.g., API calls).
        /// </remarks>
        public static implicit operator int(SelectionType selectionType)
        {
            return selectionType._intValue;
        }

        /// <summary>
        /// Implicitly converts a <see cref="SelectionType"/> to its string representation.
        /// </summary>
        /// <param name="selectionType">The <see cref="SelectionType"/> to convert.</param>
        /// <returns>
        /// The underlying string value, or <see langword="null"/> for custom types.
        /// </returns>
        /// <remarks>
        /// Allows seamless use of <see cref="SelectionType"/> where a string is expected (e.g., API calls).
        /// </remarks>
        public static implicit operator string(SelectionType selectionType)
        {
            return selectionType._stringValue;
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current SelectionType.
        /// </summary>
        /// <param name="obj">The object to compare with the current object</param>
        /// <returns>true if the objects are considered equal; otherwise, false</returns>
        public override bool Equals(object obj)
        {
            // Null and type check
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = (SelectionType)obj;

            // Compare primitive values first for early exit
            if (_intValue != other._intValue)
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

        /// <summary>
        /// Serves as the default hash function for SelectionType.
        /// </summary>
        /// <returns>A hash code for the current object</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                if (_customFeatureNames == null)
                {
                    // Handles only _intValue if no custom features, _stringValue check is not necessary
                    return _intValue.GetHashCode();
                }

                // Handles _intValue and custom features array if we have custom features. _stringValue check is not necessary.
                var hashCode = _customFeatureNames.Sum(featureName => featureName.GetHashCode() * 17);
                hashCode += _intValue.GetHashCode();
                return hashCode;
            }
        }
    }
}